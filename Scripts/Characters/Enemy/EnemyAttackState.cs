using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;
    protected override void EnterState()
    {
        Node3D target = CharacterNode.AttackAreaNode
            .GetOverlappingBodies()
            .First();
        targetPosition = target.GlobalPosition;


        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;

        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        CharacterNode.ToggleHitbox(true);

        Node3D target = CharacterNode.AttackAreaNode
            .GetOverlappingBodies()
            .FirstOrDefault();
        
        if (target == null)
        {
            Node3D chaseTarget = CharacterNode.ChaseAreaNode
                .GetOverlappingBodies()
                .FirstOrDefault();
            
            if (chaseTarget == null)
            {
                CharacterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }

            CharacterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = CharacterNode.GlobalPosition.DirectionTo(targetPosition);

        CharacterNode.Sprite3DNode.FlipH = direction.X < 0;
    }

    private void PerformHit()
    {
        CharacterNode.ToggleHitbox(false);
        CharacterNode.HitboxNode.GlobalPosition = targetPosition;
    }
}
