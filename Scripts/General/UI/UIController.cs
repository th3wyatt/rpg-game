using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((element) => element is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary((element) => element.container);

        containers[ContainerType.Start].Visible = true;

        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

        GameEvents.OnEndGame += HandleOnEndGame;
        GameEvents.OnVictory += HandleOnVictory;
        GameEvents.OnReward += HandleOnReward;

        
    }

    public override void _Input(InputEvent @event)
    {
        if (!canPause) {return;}
        
        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }

        containers[ContainerType.Pause].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;

    }

    private void HandleOnVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleOnEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }


    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

    
    private void HandlePausePressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
    }

    private void HandleOnReward(RewardResource resource)
    {
        canPause = false;
        GetTree().Paused = true;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;
        containers[ContainerType.Reward].TextureNode.Texture = resource.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = resource.Description;
    }
    
    private void HandleRewardPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }


}
