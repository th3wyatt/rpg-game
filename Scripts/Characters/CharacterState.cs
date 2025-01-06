using System;
using Godot;

public abstract partial class CharacterState : Node // this class isn't meant to be implemented directly
{
    
    protected Character CharacterNode; 
    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        CharacterNode = GetOwner<Character>();
        SetPhysicsProcess(false); // Disable physicsprocess logic unless we need it
        SetProcessInput(false); // Stop checking for input unless we need it
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == GameConstants.ENTER_STATE_CHANNEL)
        {
            EnterState();
            SetPhysicsProcess(true); // enable the physics process to check for if we need to change state
            SetProcessInput(true); // check for input
        }
        else if (what == GameConstants.EXIT_STATE_CHANNEL)
        {
            SetPhysicsProcess(false); // turn off physics process logic
            SetProcessInput(false); // turn off checking for input
            ExitState();

        }
    }

    protected virtual void EnterState() {}

    protected virtual void ExitState() {}


}

