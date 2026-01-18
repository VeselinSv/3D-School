using UnityEngine;
using System.Collections;

public class WindowOpen : MonoBehaviour
{
    [Header("Window Parts")]
    public Transform windowWing;     

    [Header("Rotation Settings")]
    public float openAngle = -90f;  
    public float speed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine rotateRoutine;

    void Start()
    {
        closedRotation = windowWing.localRotation;

        openRotation = Quaternion.Euler(
            windowWing.localEulerAngles.x,
            windowWing.localEulerAngles.y + openAngle,
            windowWing.localEulerAngles.z
        );
    }

    public void ToggleWindow()
    {
        isOpen = !isOpen;

        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(
            RotateWindow(isOpen ? openRotation : closedRotation)
        );

        PlaySound();
    }

    private IEnumerator RotateWindow(Quaternion targetRotation)
    {
        while (Quaternion.Angle(windowWing.localRotation, targetRotation) > 0.1f)
        {
            windowWing.localRotation = Quaternion.Slerp(
                windowWing.localRotation,
                targetRotation,
                Time.deltaTime * speed
            );
            yield return null;
        }

        windowWing.localRotation = targetRotation;
    }

    private void PlaySound()
    {
        if (audioSource == null)
            return;

        audioSource.clip = isOpen ? openSound : closeSound;
        audioSource.Play();
    }
}
