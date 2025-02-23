using Godot;
using System;
using Transparency;

public partial class HaamuMovement : CharacterBody2D
{
	[Export] public float Speed = 100f;
	[Export] public float LeftLimit = -200f;
	[Export] public float RightLimit = 200f;
    [Export] private Vector2 _destination = Vector2.Zero;

	private Vector2 _Velocity = new Vector2(1, 0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _Velocity = (_destination - GlobalPosition).Normalized();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += _Velocity * Speed * (float)delta;
		if (IsEqualApprox(_destination, GlobalPosition))
		{
			_destination = Level.Current.CurrentGrid.GetRandomCell().GlobalPosition;
            _Velocity = (_destination - GlobalPosition).Normalized();
		}
	}
    private bool IsEqualApprox(Vector2 i, Vector2 j)
    {
        if (Mathf.Abs(i.X - j.X) > 1 && Mathf.Abs(i.Y - j.Y) > 1)
        {
            return false;
        }
        return true;
    }
}