using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int comboCounterMax = 2;
    [Export] private Timer comboTimerNode;

    public override void _Ready()
    {
        base._Ready();
        //lamda to execute a simple action rather than setup a standard method
        comboTimerNode.Timeout += () => comboCounter = 1; 
    }

    protected override void EnterState()
    {
        CharacterNode.AnimationPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f
        );

        CharacterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        CharacterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, comboCounterMax + 1);
        CharacterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

}
