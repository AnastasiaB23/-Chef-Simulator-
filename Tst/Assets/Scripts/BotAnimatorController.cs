using UnityEngine;

public class BotAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int Work = Animator.StringToHash("IsWorking");
    private static readonly int Wait = Animator.StringToHash("IsHitted");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void IsHitted(bool cond)
    {
        _animator.SetBool(Wait, cond);
    }
    public void IsWorking(bool cond)
    {
        _animator.SetBool(Work, cond);
    }
}
