using System;

public class GameEvents
{
    public static event Action OnStartGame;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
}