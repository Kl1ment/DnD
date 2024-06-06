
using UnityEngine;

public class PlayerStateWalk : StateBase
{
    const string Walk = "Walk";

    public PlayerStateWalk(FSM fsm, Animator animator, PlayerMovement playerMovement) : base(fsm, animator, playerMovement)
    {
    }

    public override void Enter()
    {
        _animator.SetBool(Walk, true);
    }

    public override void Exit()
    {
        _animator.SetBool(Walk, false);
    }

    public override void Update()
    {
        _playerMovement.Move();

        if (!_playerMovement.IsMove)
        {
            _fsm.SetState<PlayerStateIdle>();
        }
    }
}
