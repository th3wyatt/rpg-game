using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_STUN);

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (CharacterNode.AttackAreaNode.HasOverlappingBodies())
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        } else if (CharacterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        } else
        {
            CharacterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;

    }

}
