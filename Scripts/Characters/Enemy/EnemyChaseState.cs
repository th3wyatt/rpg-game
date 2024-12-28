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

        target = CharacterNode.ChaseAreaNode
        .GetOverlappingBodies()
        .First() as CharacterBody3D;

        EnemyChaseTimer.Timeout += HandleChaseTimerTimeout;
        CharacterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        EnemyChaseTimer.Timeout -= HandleChaseTimerTimeout;
        CharacterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        CharacterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;        
    }

    private void HandleChaseTimerTimeout()
    {
        destination = target.GlobalPosition;
        CharacterNode.AgentNode.TargetPosition = destination;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

}
