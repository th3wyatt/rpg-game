using System;
using Godot;

public abstract partial class EnemyState: CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();

        CharacterNode.GetStatResource(Stat.Health).OnZero += HandleZerohealth;
    }


    protected void Move()
    {
        CharacterNode.AgentNode.GetNextPathPosition();
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(destination);
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected Vector3 GetPointGlobalPosition(int pathPointIndex)
    {
        Vector3 localPosition = CharacterNode.PathNode.Curve.GetPointPosition(pathPointIndex);
        Vector3 globalPosition = CharacterNode.PathNode.GlobalPosition;
        return localPosition + globalPosition;

    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }
    private void HandleZerohealth()
    {
        CharacterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }

}