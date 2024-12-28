using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{

        public override void _PhysicsProcess(double delta)
    {

        CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();


    }
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
