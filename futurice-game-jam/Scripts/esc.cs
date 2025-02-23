using Godot;
using System;

public partial class esc : Node
{
    [Export] public Button ResumeButton;
    [Export] public Button RestartButton;
    [Export] public Button MainmenuButton;
    [Export] public Button CloseButton;
    [Export] public ColorRect EscPopup;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Ready method called");
        ResumeButton.Pressed += OnResumeButtonPressed;
        RestartButton.Pressed += OnRestartButtonPressed;
        MainmenuButton.Pressed += OnMainmenuButtonPressed;
        CloseButton.Pressed += () => EscPopup.Visible = false;
    }

    private void OnResumeButtonPressed()
    {
        GD.Print("Resume button pressed");
        GetTree().Paused = false;
        EscPopup.Visible = false;
    }

    private void OnRestartButtonPressed()
    {
        GD.Print("Restart button pressed");
        GetTree().ChangeSceneToFile("res://main.tscn");
    }

    private void OnMainmenuButtonPressed()
    {
        GD.Print("Mainmenu button pressed");
        GetTree().ChangeSceneToFile("res://mainmenu.tscn");
    }

    private void OnCloseButtonPressed()
    {
        GD.Print("Close button pressed");
        EscPopup.Visible = false;
    }

    private void TogglePauseMenu()
    {
        GD.Print("Toggling pause menu");
        EscPopup.Visible = !EscPopup.Visible;
        GetTree().Paused = EscPopup.Visible;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GD.Print("ESC key pressed");
            TogglePauseMenu();
        }
    }
}