using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{   
    [Export(PropertyHint.Range, "0,20,.1")] private int speed = 5;

    public override void _PhysicsProcess(double delta)
    {
        
        if (CharacterNode.direction == Vector2.Zero)
        {
            CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }
        
        CharacterNode.Velocity = new(CharacterNode.direction.X, 0, CharacterNode.direction.Y);
        CharacterNode.Velocity *= speed;

        CharacterNode.MoveAndSlide();

        CharacterNode.Flip();

    }

    public override void _Input(InputEvent @event)
    {
       if (Input.IsActionJustPressed(GameConstants.ANIM_DASH))
       {
            CharacterNode.StateMachineNode.SwitchState<PlayerDashState>();
       }
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

    }

}