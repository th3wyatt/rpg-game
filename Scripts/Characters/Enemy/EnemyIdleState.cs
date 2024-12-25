using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
