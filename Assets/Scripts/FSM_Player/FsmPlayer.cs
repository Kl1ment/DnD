
using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(PlayerMovement))]
public class FsmPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private FSM _fsm;
    private PlayerMovement _movement;

    private void Start()
    {
        _fsm = new FSM();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();

        _fsm.AddState(new PlayerStateIdle(_fsm, _animator, _movement));
        _fsm.AddState(new PlayerStateWalk(_fsm, _animator, _movement));

        _fsm.SetState<PlayerStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}
