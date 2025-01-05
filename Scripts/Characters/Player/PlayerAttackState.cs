using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int comboCounterMax = 2;
    [Export] private Timer comboTimerNode;
    [Export] private PackedScene lightningScene;

    public override void _Ready()
    {
        base._Ready();
        //lamda to execute a simple action rather than setup a standard method
        comboTimerNode.Timeout += () => comboCounter = 1; 
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f
        );

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
        CharacterNode.HitboxNode.BodyEntered += HandleBodyEntered;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        CharacterNode.HitboxNode.BodyEntered -= HandleBodyEntered;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, comboCounterMax + 1);
        
        CharacterNode.ToggleHitbox(true);
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();

    }

    private void HandleBodyEntered(Node3D body)
    {
        if (comboCounter != comboCounterMax) { return ; }

        Node3D lightning  = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;

    }

    private void PerformHit()
    {
        Vector3 newPosition = CharacterNode.Sprite3DNode.FlipH ?
            Vector3.Left:
            Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;
        
        CharacterNode.HitboxNode.Position = newPosition;
        GD.Print("Perform Hit!");

        CharacterNode.ToggleHitbox(false);
    }
}
