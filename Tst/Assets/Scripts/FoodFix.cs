using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFix : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
