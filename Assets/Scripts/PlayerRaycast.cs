using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float interactDistance = 5f;
    public Transform player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (!hit.collider.CompareTag("Openable"))
                    return;

                RollableCabin rollableCabin =
                    hit.collider.GetComponentInParent<RollableCabin>();

                if (rollableCabin != null)
                {
                    rollableCabin.ToggleCabin(player);
                    return;
                }

                CabinOpen cabinDoor =
                    hit.collider.GetComponentInParent<CabinOpen>();

                if (cabinDoor != null)
                {
                    cabinDoor.ToggleDoor(player);
                    return;
                }

                DoorInteraction genericDoor =
                    hit.collider.GetComponentInParent<DoorInteraction>();

                if (genericDoor != null)
                {
                    genericDoor.ToggleDoor(player);
                    return;
                }

                WindowOpen window =
                    hit.collider.GetComponentInParent<WindowOpen>();

                if (window != null)
                {
                    window.ToggleWindow();
                    return;
                }
            }
        }
    }
}
