using Godot;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.ENTER_STATE_CHANNEL);
    }

    public void SwitchState<T>()
    {
        //LINQ Query to get matching state
        //see Character.cs for description
        //T represents the type of the state
        CharacterState newState = states.Where((element) => element is T).FirstOrDefault();
        
        if (newState == null) {return;}

        if (currentState is T) {return;}

        if (!newState.CanTransition()) { return; }

        currentState.Notification(GameConstants.EXIT_STATE_CHANNEL);
        currentState = newState;
        currentState.Notification(GameConstants.ENTER_STATE_CHANNEL);
    }
}
