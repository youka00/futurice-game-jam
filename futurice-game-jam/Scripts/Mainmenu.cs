using Godot;
using System;

public partial class Mainmenu : Node
{

	[Export] public Button PlayButton;
	[Export] public Button CreditsButton;
	[Export] public Button QuitButton;
	[Export] public Button HowplayButton;
	[Export] public ColorRect CreditsPopup;
	[Export] public ColorRect HowtoplayPopup;
	[Export] public Button CloseCreditsButton;
	[Export] public Button CloseHowtoplayButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayButton.Pressed += OnPlayButtonPressed;
		QuitButton.Pressed += OnQuitButtonPressed;
		HowplayButton.Pressed += OnHowplayButtonPressed;
		CreditsButton.Pressed += OnCreditsButtonPressed;
		CloseCreditsButton.Pressed += () => CreditsPopup.Visible = false;
		CloseHowtoplayButton.Pressed += () => HowtoplayPopup.Visible = false;

	}


	private void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	private void OnHowplayButtonPressed()
	{
		HowtoplayPopup.Visible = true;
	}

	private void OnCreditsButtonPressed()
	{
		CreditsPopup.Visible = true;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
