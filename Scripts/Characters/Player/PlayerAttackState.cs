using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int comboCounterMax = 2;

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter
        );

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, comboCounterMax + 1);
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

}
