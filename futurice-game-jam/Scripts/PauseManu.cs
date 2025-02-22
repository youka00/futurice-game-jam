using Godot;
using System;


public partial class PauseManu : Node
{
	[Export] public Button ResumeButton;
	[Export] public Button RestartButton;
	[Export] public Button MainmenuButton;
	[Export] public Button CloseButton;
	[Export] public Button EscButton;
	[Export] public ColorRect EscPopup;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResumeButton.Pressed += OnResumeButtonPressed;
		RestartButton.Pressed += OnRestartButtonPressed;
		MainmenuButton.Pressed += OnMainmenuButtonPressed;
		EscButton.Pressed += () => EscPopup.Visible = true;
		CloseButton.Pressed += () => EscPopup.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnResumeButtonPressed()
	{
		GetTree().Paused = false;
		EscPopup.Visible = false;
	}
	private void OnRestartButtonPressed()
	{
		GetTree().ReloadCurrentScene();
	}
	private void OnMainmenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://mainmenu.tscn");
	}
	private void OnCloseButtonPressed()
	{
		EscPopup.Visible = false;
	}



}
