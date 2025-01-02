using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;
    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
    }

    private void HandleStartGame()
    {
        Reparent(target);

        Position = positionFromTarget;
    }

}
