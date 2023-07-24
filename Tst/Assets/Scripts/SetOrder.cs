using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetOrder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Dish"))
        {
            collider.gameObject.tag = "Order";
            collider.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }
}
