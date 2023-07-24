using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Cooking : MonoBehaviour
{
    private string _name;
    [SerializeField]List<string> _avilable = new List<string>()
    {
        "bread",
        "Raw Egg",
        "Raw meat",
        "Raw Chicken Breast",
        "Raw Bacon",
        "Raw Chicken",
        "Onion Slice",
    };
    Dictionary<string, string> RawToCooked = new Dictionary<string, string>() 
    {
        { "bread" , "Toast" },
        { "Raw meat" , "Cooked Meat" },
        { "Raw Chicken" , "Cooked Chicken" },
        { "Raw Egg" , "Fried Egg" },
        { "Raw Chicken Breast" , "Cooked Chicken Breast" },
        { "Raw Bacon" , "Cooked Bacon" },
        //{ "" , "" },
    };
    Dictionary<string, string> CookedToBurnt = new Dictionary<string, string>()
    {
         { "Toast" , "Burnt Toast" },
        { "Cooked Meat" , "Burnt Meat" },
        { "Cooked Chicken" , "Burnt Chicken" },
        { "Fried Egg" , "Burnt Egg" },
        { "Cooked Chicken Breast" , "Burnt Chicken Breast" },
        { "Cooked Bacon" , "Burnt Bacon" },
        //{ "" , "" },
    };

    private GameObject _dish;
    public static float _cookSpeed = 0.5f;
    public bool _isCooking = false;
    public bool _isEmpty = true;
    [SerializeField]private float CookTime = 25f;
    [SerializeField]private float BurnTime = 5f;
    private bool _isCooked = false;
    private bool _isBurnt = false;
    public static bool _isBurnUp = false;
    public static bool _isCookUp = false;

    GameObject Cooktxt;
    GameObject Readytxt;
    GameObject GameOvPanel;


    public void IsBurnUp()
    {
        _isBurnUp=true;
    }

    public void IsCookUp()
    {
        Debug.Log(_isCookUp);
        _isCookUp = true;
        Debug.Log(_isCookUp);
    }

    public void CookSpeedUp()
    {
        
        _cookSpeed = 1.5f;
    }

    private void Awake()
    {
        GameOvPanel = GameObject.Find("GameOverPanelParent");
        Cooktxt = GameObject.Find("CookingText");
        Readytxt = GameObject.Find("ReadyText");

        Debug.Log(Readytxt);

        Cooktxt.SetActive(false);
        Readytxt.SetActive(false);

        Debug.Log(_cookSpeed);
        Debug.Log(_isCookUp);
        if (_isBurnUp)
        {
            BurnTime = 15f;
        }
        if (_isCookUp)
        {
            CookTime = 15f;
        }
        GameOvPanel.SetActive(false);
    }

    //Вариант, когда предмет входит в сковороду и у него все отключается (объект делается дочерним)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && _isEmpty)
        {
            foreach (string s in _avilable)
            {
                if (s == other.name)
                {
                    _dish = GameObject.Find(s);
                    SetDish(_dish);
                    _dish.GetComponent<BoxCollider>().enabled = false;
                    _dish.GetComponent<XRGrabInteractable>().enabled = false;
                }
            }
        }
        if (other.name == "Stove" && !_isEmpty)
        {
            Cooktxt.SetActive(true);
            Debug.Log("12345");
        }
    }
    //Вариант, где считывается, что объект находится внутри (объект не делается дочерним)
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Stove")
        {
            if (!_isEmpty && !_isBurnt)
            {
                CookTime -= Time.deltaTime * _cookSpeed;
                Debug.Log(CookTime);
                Cooktxt.SetActive(true);
                Debug.Log(_cookSpeed);
            }
            if (CookTime < 0 && CookTime > -BurnTime)
            {
                if (!_isCooked)
                {
                    string name = _dish.name;
                    Destroy(_dish);
                    _dish = GameObject.Find(RawToCooked[name]);
                    SetDish(_dish);
                    gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                    _dish.GetComponent<Rigidbody>().isKinematic = false;
                }
                _isCooked = true;
                Cooktxt.SetActive(false);
                Readytxt.SetActive(true);

            }
            if (CookTime < -BurnTime)
            {
                _isCooking = false;
                if (!_isBurnt)
                {
                    name = _dish.name;
                    Destroy(_dish);
                    _dish = GameObject.Find(CookedToBurnt[name]);
                    SetDish(_dish);
                    Readytxt.SetActive(false);
                    //надпись "сгорело..." (сет актив тру)

                    GameObject.Find("XR Origin").transform.position = Vector3.zero;
                    GameObject.Find("XR Origin").GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = 0;
                    GameOvPanel.SetActive(true); 

                    _isBurnt = true;
                    Cooktxt.SetActive(false);
                }
                
                // дописать, чтобы удалялись/отключались все объекты для готовки + перемещать игрока в позицию (0, 0, 0)

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Stove")
        {
            _isCooking = false;
            Readytxt.SetActive(false);
        }    
            

        if (other.CompareTag("Interactable"))
        {
            _isCooking = false;
            _isEmpty = true;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.SetParent(null);
        }
    }
    public void SetDish(GameObject dish)
    {
        dish.GetComponent<Rigidbody>().isKinematic = true;
        dish.transform.SetParent(transform);
        dish.transform.localPosition = new Vector3(0, 0.15f, 0);
        dish.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        _isEmpty = false;
    }
    
}
