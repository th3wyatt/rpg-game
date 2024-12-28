using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{

    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);

    }

    public override void _PhysicsProcess(double delta)
    {
        if (CharacterNode.AgentNode.IsNavigationFinished())
        {
            GD.Print("Reached Destination, switching to patrol");
            CharacterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();

    }
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE);

        CharacterNode.AgentNode.TargetPosition = destination;
    }
}
