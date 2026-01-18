using UnityEngine;

public class CabinOpen : MonoBehaviour
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

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void Update()
    {
        doorPivot.localRotation = Quaternion.RotateTowards(
            doorPivot.localRotation,
            targetRotation,
            openSpeed * Time.deltaTime * 100f
        );
    }

    public void ToggleDoor(Transform player)
    {
        if (!isOpen)
            OpenDoor(player);
        else
            CloseDoor();
    }

    private void OpenDoor(Transform player)
    {
        if (isOpen) return;

        Vector3 toPlayer = (player.position - transform.position).normalized;

        Vector3 pivotForward = doorPivot.forward;

        float side = Vector3.Cross(pivotForward, toPlayer).y;

        float direction = (side >= 0f) ? 1f : -1f;

        targetRotation = closedRotation * Quaternion.AngleAxis(openAngle * direction, doorPivot.up);

        if (openSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(openSound);
        }

        isOpen = true;
    }

    private void CloseDoor()
    {
        targetRotation = closedRotation;

        if (closeSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(closeSound);
        }

        isOpen = false;
    }
}
