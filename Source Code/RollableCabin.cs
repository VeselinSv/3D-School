using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RollableCabin : MonoBehaviour
{
    [Header("Door")]
    public Transform slidingDoor;
    public float openDistance = 0.4f;
    public float speed = 2f;

    [Header("Sound")]
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen;
    private bool isMoving;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void Start()
    {
        closedPos = slidingDoor.localPosition;
        openPos = closedPos + slidingDoor.up * openDistance;
    }

    public void ToggleCabin(Transform player)
    {
        if (isMoving) return;

        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? closedPos : openPos));
        PlaySound();
        isOpen = !isOpen;   
    }

    IEnumerator MoveDoor(Vector3 target)
    {
        isMoving = true;

        while (Vector3.Distance(slidingDoor.localPosition, target) > 0.01f)
        {
            slidingDoor.localPosition = Vector3.MoveTowards(
                slidingDoor.localPosition,
                target,
                speed * Time.deltaTime
            );
            yield return null;
        }

        slidingDoor.localPosition = target;
        isMoving = false;
    }

    void PlaySound()
    {
        if (audioSource == null) return;

        audioSource.clip = isOpen ? closeSound : openSound;
        audioSource.Play();
    }
}
