

using UnityEngine;

public abstract class StateBase
{
    protected readonly FSM _fsm;
    protected readonly Animator _animator;
    protected readonly PlayerMovement _playerMovement;

    public StateBase(FSM fsm, Animator animator, PlayerMovement playerMovement)
    {
        _fsm = fsm;
        _animator = animator;
        _playerMovement = playerMovement;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();

}
