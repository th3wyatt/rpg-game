using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DEATH);

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        CharacterNode.QueueFree(); //bye bye
    }

}
