using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, int> _available = new Dictionary<string, int>()
    {
        {"Avocado", 4},
        {"cheese", 6},
        {"loaf", 4},
        {"Onion", 4},
        {"paprika", 12},
        {"tomato", 12},
    };
    [SerializeField]
    private Dictionary<string, string> _toCut = new Dictionary<string, string>()
    {
        {"Avocado", "Avocado Slice"},
        {"cheese", "cheeseCut"},
        {"loaf", "bread"},
        {"Onion", "Onion Slice"},
        {"paprika", "paprikaSlice"},
        {"tomato", "tomatoSlice"},
    };
    [SerializeField] private int _cutCount;
    private GameObject _dish;
    private bool _isEmpty = true;
    private bool _isCutted = false;
    public static bool _isUpgraded = false;

    public void IsUpgraded()
    {
        _isUpgraded = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        foreach (var kvp in _available)
        {
            if (_isEmpty && other.name == kvp.Key)
            {
                _dish = other.gameObject;
                SetDish(_dish);
                _dish.GetComponent<XRGrabInteractable>().enabled = false;
                _cutCount = _available[_dish.name];
                if(_isUpgraded)
                    _cutCount = _available[_dish.name] / 2;
                /*other.enabled = false;*/
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.name == "knife" && !_isCutted)
        {
            _cutCount--;
            if (_cutCount == 0)
            {
                _isCutted = true;
                _dish.GetComponent<XRGrabInteractable>().enabled = true;
                _cutCount--;
            }
        }
        if (other.CompareTag("Interactable"))
        {
            _isEmpty = true;
            other.transform.SetParent(null);
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    private void Update()
    {
        if (_isCutted)
        {
            string name = _dish.name;
            Destroy(_dish);
            _dish = GameObject.Find(_toCut[name]);
            SetDish(_dish);
            _dish.GetComponent<Rigidbody>().isKinematic = false;
            _isCutted = false;
        }
    }
    public void SetDish(GameObject dish)
    {
        dish.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        dish.transform.SetParent(transform);
        dish.transform.localPosition = new Vector3(0, 0.05f, 0);

        _isEmpty = false;
    }
}
