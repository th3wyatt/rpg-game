using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.ENTER_STATE_CHANNEL);
    }

    public void SwitchState<T>()
    {
        Node newState = null;
        foreach (Node state in states)
        {
            if (state is T)
            {
                newState = state;
            }
        }

        if (newState == null) {return;}

        currentState.Notification(GameConstants.EXIT_STATE_CHANNEL);
        currentState = newState;
        currentState.Notification(GameConstants.ENTER_STATE_CHANNEL);
    }
}
