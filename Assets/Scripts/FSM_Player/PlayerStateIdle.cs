

using UnityEngine;

public class PlayerStateIdle : StateBase
{
    const string Idle = "Idle";
    const string Vertical = "Vertical";
    const string Horizontal = "Horizontal";

    public PlayerStateIdle(FSM fsm, Animator animator, PlayerMovement playerMovement) : base(fsm, animator, playerMovement)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Idle");
        _animator.SetBool(Idle, true);
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
        _animator.SetBool(Idle, false);
    }

    public override void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.z = Input.GetAxis(Horizontal);
        direction.x = -Input.GetAxis(Vertical);

        if (direction.magnitude != 0 )
        {
            _fsm.SetState<PlayerStateWalk>();
        }
    }
}
