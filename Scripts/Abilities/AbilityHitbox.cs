using Godot;
using System;

public partial class AbilityHitbox : Area3D , IHitBox
{
    public float GetDamage() => GetOwner<Ability>().Damage;

}
