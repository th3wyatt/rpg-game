using Godot;
using System;
using System.Reflection;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTime = 3;
    private int pointIndex;
    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        {
            return;
        }

        Move();
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;
        destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = destination;

        CharacterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        CharacterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        CharacterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;

    }


    private void HandleNavigationFinished()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
        
        RandomNumberGenerator rng = new();

        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimerNode.Start();

    }
    
    private void HandleTimeout()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);
        
        //prevents out of bounds for the points in the path curve array
        pointIndex = Mathf.Wrap(pointIndex +1, 0, CharacterNode.PathNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.AgentNode.TargetPosition = destination;
    }

}
