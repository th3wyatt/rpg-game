using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.direction != Vector2.Zero)
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
       CheckForAttackInput();

       if (Input.IsActionJustPressed(GameConstants.ANIM_DASH))
       {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
       }
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
