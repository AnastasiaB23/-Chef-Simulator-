using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RobotAI : MonoBehaviour
{
    private Transform _idlePath;
    private Transform _waitPath;
    private Transform _workPath;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rotationSpeed = 2;
    private Transform target;
    private Transform _path;
    [SerializeField] private RobotStates _currentState;
    private Transform[] _points;
    private int _currentPoint;
    private GameObject order;
    [SerializeField] private BotAnimatorController _controller;
    [SerializeField]private SceneTransitionManager _fadeAction;
    [SerializeField] private FadeScreen _fadeScreen;
    private Transform _endOfTheDayCanvas;
    public static bool _isEnd = false;
    private float _enteringSpeed = 0.8f;
    

    private void SetPoints()
    {
        _points = new Transform[_path.childCount];
        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
        _currentPoint = 0;
    }
    private void PathMovement(Transform target, bool await)
    {
        var direction = target.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        if (!await) transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed);
        else transform.Rotate(new Vector3(30 * Time.deltaTime, 30 * Time.deltaTime, _speed * Time.deltaTime * 35));
        if (transform.position == target.position) _currentPoint = GenerateNewPoint();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Interactable") || collider.CompareTag("InteractableTool") || collider.CompareTag("Dish"))
        {
            _path = _waitPath;
            SetPoints();
            _currentState = RobotStates.Wait;
        }

    }
    private void BotIdle()
    {
        target = _points[_currentPoint];
        PathMovement(target, false);
        TryFindTarget();
        _controller.IsHitted(false);
        _controller.IsWorking(false);
    }
    private void BotWait()
    {
        target = _points[_currentPoint];
        PathMovement(target, true);
        TryFindTarget();
        _controller.IsHitted(true);
    }
    private void BotFollow()
    {
        var direction = order.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, order.transform.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed);
        if (transform.position == order.transform.position)
        {
            _path = _workPath;
            SetPoints();
            _currentState = RobotStates.TakeAway;
            order.GetComponent<Rigidbody>().isKinematic = true;
            order.transform.SetParent(transform);
            order.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    private void BotTake()
    {
        target = _points[_currentPoint];
        PathMovement(target, false);
        if (_currentPoint == _points.Length - 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Order"));
            _currentState = RobotStates.Idle;
            _speed = 1;
            _path = _idlePath;
            SetPoints();
            _fadeScreen.gameObject.SetActive(true);
            

            _fadeAction.GoToSceneAsync(SceneManager.GetActiveScene().buildIndex);
            _isEnd = true;

            //_endOfTheDayCanvas.position= Vector3.MoveTowards(_endOfTheDayCanvas.position,new Vector3 (0.201f, 2.222f, -0.05f), _speed*Time.deltaTime);
            
            // переместить игрока в положение (0, 0, 0)
            
           
            
        }
        _controller.IsWorking(true);

    }
    private void Start()
    {
        _endOfTheDayCanvas = GameObject.Find("EndOfTheDayMenu").transform;
        _idlePath = GameObject.Find("IdlePath").transform;
        _waitPath = GameObject.Find("WaitPath").transform;
        _workPath = GameObject.Find("WorkPath").transform;
        _currentState = RobotStates.Idle;
        _path = _idlePath;
        SetPoints();
    }
    private void Update()
    {
        switch (_currentState)
        {
            case RobotStates.Idle:
                BotIdle();

                break;
            case RobotStates.Wait:
                BotWait();

                break;
            case RobotStates.FollowOrder:
                BotFollow();

                break;
            case RobotStates.TakeAway:
                BotTake();

                break;

        }
        if(_isEnd)
        {
            _endOfTheDayCanvas.position = Vector3.MoveTowards(_endOfTheDayCanvas.position, new Vector3(0.201f, 2.222f, -0.05f), _enteringSpeed * Time.deltaTime);
            if(_endOfTheDayCanvas.position == new Vector3(0.201f, 2.222f, -0.05f))
            {
                _isEnd= false;
            }
        }
    }
    private void TryFindTarget()
    {
        order = GameObject.FindGameObjectWithTag("Order");
        if (order != null)
        {
            _currentState = RobotStates.FollowOrder;
            _speed = 2;
        }
    }
    private int GenerateNewPoint()
    {
        /*int index = Random.Range(0, _path.childCount); ;
        while ((index - prev) <= 2 || (prev - index) <= 2)
            index = Random.Range(0, _path.childCount);*/ //endless
        int index = (_currentPoint + 1) % _path.childCount;
        return index;
    }
    public enum RobotStates
    {
        Idle,
        Wait,
        FollowOrder,
        TakeAway,
    }
}
