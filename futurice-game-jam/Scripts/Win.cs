using Godot;
using System;

public partial class Win : AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Finished += Winning;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    private void Winning()
    {
        GetTree().ChangeSceneToFile("res://mainmenu.tscn");
    }
}
