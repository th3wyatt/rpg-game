using Godot;


public partial class Ability : Node3D
{
    [Export] protected AnimationPlayer animPlayerNode;
    [Export] public float Damage { get; protected set; } = 10;

}