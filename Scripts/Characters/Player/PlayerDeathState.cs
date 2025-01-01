using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DEATH);

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        CharacterNode.QueueFree();
    }

}
