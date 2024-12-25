using Godot;

public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayerNode {get; private set;}
    [Export] public Sprite3D Sprite3DNode {get; private set;}
    [Export] public StateMachine StateMachineNode {get; private set;}
    
    public Vector2 direction = new();

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        
        if (isNotMovingHorizontally) {return;}
        bool isMovingLeft = Velocity.X < 0;
        Sprite3DNode.FlipH = isMovingLeft;
    }
}