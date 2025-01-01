using System;
using Godot;

public abstract partial class PlayerState : CharacterState // this class isn't meant to be implemented directly
{
    public override void _Ready()
    {
        base._Ready();
        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
    private void HandleZeroHealth()
    {
        CharacterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }
}