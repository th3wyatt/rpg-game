using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer EnemyChaseTimer;
    private CharacterBody3D target;
    
    public override void _PhysicsProcess(double delta)
    {
        Move();
    }
    
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        GD.Print("Chase state");

        EnemyChaseTimer.Timeout += HandleChaseTimerTimeout;

        target = CharacterNode.ChaseAreaNode
        .GetOverlappingBodies()
        .First() as CharacterBody3D;
    }

    protected override void ExitState()
    {
        EnemyChaseTimer.Timeout -= HandleChaseTimerTimeout;        
    }

    private void HandleChaseTimerTimeout()
    {
        destination = target.GlobalPosition;
        CharacterNode.AgentNode.TargetPosition = destination;
    }

}
