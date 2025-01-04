using Godot;
using System;

public partial class StatLabel : Label
{
    [Export] private StatResource statResource;

    public override void _Ready()
    {
        statResource.OnUpdate += HandleStatUpdated;

        Text = statResource.StatValue.ToString();
    }

    private void HandleStatUpdated()
    {
        Text = statResource.StatValue.ToString();
    }

}
