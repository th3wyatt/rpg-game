using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;
    
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayerNode {get; private set;}
    [Export] public Sprite3D Sprite3DNode {get; private set;}
    [Export] public StateMachine StateMachineNode {get; private set;}
    
    [ExportGroup("AI Nodes")] 
    [Export] public Path3D PathNode { get; private set;}
    [Export] public NavigationAgent3D AgentNode { get; private set;}
    [Export] public Area3D ChaseAreaNode { get; private set;}
    [Export] public Area3D AttackAreaNode { get; private set;}
    [Export] public Area3D HurtboxNode { get; private set;}
    public Vector2 direction = new();

    public override void _Ready()
    {
        base._Ready();
        HurtboxNode.AreaEntered += HandleHurtboxAreaEntered;
    }

    private void HandleHurtboxAreaEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        GD.Print(health.StatValue);
    }

    public StatResource GetStatResource(Stat stat)
    {
        //LINQ query to get the matching stat in our stats array
        //each item in the collection will be referred to as element
        //lamda matches the stat type in element with the stat to match from the param
        //we only want 1 result, so we get the first or default item in the collecion
        return stats.Where((element) => stat == element.StatType)
            .FirstOrDefault();
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;
        
        if (isNotMovingHorizontally) {return;}
        bool isMovingLeft = Velocity.X < 0;
        Sprite3DNode.FlipH = isMovingLeft;
    }
}