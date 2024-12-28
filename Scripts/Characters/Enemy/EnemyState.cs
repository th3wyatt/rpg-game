using Godot;

public abstract partial class EnemyState: CharacterState
{
    protected Vector3 destination;

    protected void Move()
    {
        CharacterNode.AgentNode.GetNextPathPosition();
        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(destination);
        CharacterNode.MoveAndSlide();
    }

    protected Vector3 GetPointGlobalPosition(int pathPointIndex)
    {
        Vector3 localPosition = CharacterNode.PathNode.Curve.GetPointPosition(pathPointIndex);
        Vector3 globalPosition = CharacterNode.PathNode.GlobalPosition;
        return localPosition + globalPosition;

    }

}