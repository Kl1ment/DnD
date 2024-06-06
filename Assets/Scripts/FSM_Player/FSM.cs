

using System;
using System.Collections.Generic;

public class FSM
{
    private Dictionary<Type, StateBase> _states = new Dictionary<Type, StateBase>();
    private StateBase _carrentState;

    public void AddState(StateBase state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : StateBase
    {
        var type = typeof(T);

        if (_carrentState != null && _carrentState.GetType() == type) { return; }

        if (_states.TryGetValue(type, out var newState))
        {
            _carrentState?.Exit();
            _carrentState = newState;
            _carrentState.Enter();
        }
    }

    public void Update()
    {
        _carrentState.Update();
    }
}
