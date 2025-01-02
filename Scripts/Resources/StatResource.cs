using System;
using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZero;
    [Export] public Stat StatType { get; private set; }

    private float _statValue;
    [Export] public float StatValue 
    { 
        get => _statValue; 
        set 
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            if (_statValue == 0)
            {
                //the ? says to ignore if the variable doesn't contain a method
                OnZero?.Invoke();
            }
        } 
    }

}