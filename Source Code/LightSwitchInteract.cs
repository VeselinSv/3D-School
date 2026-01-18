using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteract : MonoBehaviour
{
    public List<Light> lights;
    public GameObject usePanel;
    public AudioSource clickSound;

    private bool isOn = false;
    private bool playerNearby = false;

    void Start()
    {
        if (usePanel != null)
            usePanel.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLights();
        }
    }

    private void ToggleLights()
    {
        isOn = !isOn;

        foreach (Light light in lights)
            light.enabled = isOn;

        if (clickSound != null)
            clickSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (usePanel != null)
                usePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (usePanel != null)
                usePanel.SetActive(false);
        }
    }
}
