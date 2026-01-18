using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorPivot;
    public float openAngle = 90f;
    public float openSpeed = 4f;

    [Header("Audio")]
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    void Start()
    {
        closedRotation = doorPivot.localRotation;
        targetRotation = closedRotation;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; 
    }

    void Update()
    {
        doorPivot.localRotation = Quaternion.Lerp(
            doorPivot.localRotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }

    public void ToggleDoor(Transform player)
    {
        if (isOpen)
        {
            targetRotation = closedRotation;
            PlaySound(closeSound);
            isOpen = false;
            return;
        }

        Vector3 doorForward = transform.forward;
        Vector3 toPlayer = (player.position - transform.position).normalized;

        float dot = Vector3.Dot(doorForward, toPlayer);
        float direction = (dot >= 0f) ? -1f : 1f;

        targetRotation =
            closedRotation *
            Quaternion.AngleAxis(openAngle * direction, Vector3.up);

        PlaySound(openSound);
        isOpen = true;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip);
        }
    }
}
