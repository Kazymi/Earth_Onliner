using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public StateMachine(State state)
    {
        SetState(state);
    }

    public void Tick()
    {
        var newindex = IsTransitionsCondition();
        if (newindex != -1) SetState(_currentState.Transitions[newindex].StateTo);
        else
            _currentState.Tick();
        Debug.Log(_currentState);
    }

    public void FixedTick()
    {
        _currentState.FixedTick();
    }

    private int IsTransitionsCondition()
    {
        var currentTransitions = _currentState.Transitions;
        for (var i = 0; i != currentTransitions.Count; i++)
        {
            var condition = currentTransitions[i].Condition;
            if (condition.Invoke())
            {
                return i;
            }
        }

        return -1;
    }

    public void SetState(State state)
    {
        _currentState?.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }
}