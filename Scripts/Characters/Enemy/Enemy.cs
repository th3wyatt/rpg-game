using Godot;
using System;

public partial class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();

        HurtboxNode.AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area3D area)
    {
        if (area is not IHitBox hitbox) { return; }
        if (hitbox.CanStun() && GetStatResource(Stat.Health).StatValue !=0)
        {
            StateMachineNode.SwitchState<EnemyStunState>();
        }
    }

}
