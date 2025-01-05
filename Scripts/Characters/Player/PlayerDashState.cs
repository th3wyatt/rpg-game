using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private PackedScene bombScene;

    [Export(PropertyHint.Range, "0,20,.1")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected override void EnterState()
    {
            CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DASH);
            CharacterNode.Velocity = new(
                CharacterNode.direction.X, 0, CharacterNode.direction.Y
            );
            if (CharacterNode.Velocity == Vector3.Zero)
            {
                CharacterNode.Velocity = CharacterNode.Sprite3DNode.FlipH ? //shorthand if statement
                    Vector3.Left: // this runs if the condition before the ? is true
                    Vector3.Right; // this runs if it is false
            }
            CharacterNode.Velocity *= speed;
            dashTimerNode.Start();

            Node3D bomb = bombScene.Instantiate<Node3D>();
            GetTree().CurrentScene.AddChild(bomb);
            bomb.GlobalPosition = CharacterNode.GlobalPosition;
    }

    private void HandleDashTimeout()
    {
        CharacterNode.Velocity = Vector3.Zero;
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}
