using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Plate : MonoBehaviour
{
    private bool _isEmpty = true;
    private List<string> _list = new List<string>() 
    {
        "Cooked Meat",
        "Cooked Chicken Breast",
        "tomatoSlice",
        "paprikaSlice"

    };
    private void OnTriggerEnter(Collider other)
    {
        if(_isEmpty)
        {
            if (_list.Contains(other.name))
            {
                SetDish(other.gameObject);
                gameObject.tag = "Dish";
            }
        }
        
    }
    public void SetDish(GameObject dish)
    {
        if (dish.name == "Cooked Meat" || dish.name == "Cooked Chicken Breast")
            dish.transform.rotation = new Quaternion(0, 90, 90, 0);
        else
            dish.transform.rotation = new Quaternion(0, 0, 0, 0);
        Debug.Log(dish.name);
        dish.GetComponent<BoxCollider>().enabled = false;
        dish.GetComponent<Rigidbody>().isKinematic = true;
        dish.GetComponent<XRGrabInteractable>().enabled = false;
        dish.transform.SetParent(transform);
        dish.transform.localPosition = new Vector3(-0.132f, 0.19f, -0.033f);
        _isEmpty = false;
    }
}
