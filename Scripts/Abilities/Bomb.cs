using Godot;
using System;

public partial class Bomb : Ability
{
    public override void _Ready()
    {
        animPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIM_EXPAND)
        {
            animPlayerNode.Play(GameConstants.ANIM_EXPLOSION);
        } else 
        {
            QueueFree(); //bye bye
        }
    }

}
