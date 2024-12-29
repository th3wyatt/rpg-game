using Godot;

public abstract partial class PlayerState : CharacterState // this class isn't meant to be implemented directly
{
    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}