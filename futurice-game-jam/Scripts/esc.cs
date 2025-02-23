using Godot;
using System;

public partial class esc : Node
{
    [Export] public Button ResumeButton;
    [Export] public Button RestartButton;
    [Export] public Button MainmenuButton;
    [Export] public Button CloseButton;
    [Export] public Button EscButton;
    [Export] public ColorRect EscPopup;
     [Export] public Button MusicButton;
    [Export] public AudioStreamPlayer MusicPlayer;
    [Export] public Sprite2D volume_max;
    [Export] public Sprite2D volume_out;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Ready method called");
        ResumeButton.Pressed += OnResumeButtonPressed;
        EscButton.Pressed += () => EscPopup.Visible = true;
        RestartButton.Pressed += OnRestartButtonPressed;
        MainmenuButton.Pressed += OnMainmenuButtonPressed;
        MusicButton.Pressed += OnMusicButtonPressed;
        CloseButton.Pressed += () => EscPopup.Visible = false;

         if (MusicPlayer == null)
        {
            GD.PrintErr("MusicPlayer is not assigned in the inspector!");
            return;
        }

        // Initialize MusicButton text and sprite
        int musicBus = AudioServer.GetBusIndex("Music");
        if (AudioServer.IsBusMute(musicBus))
        {

            volume_max.Visible = false;
            volume_out.Visible = true;
        }
        else
        {

            volume_max.Visible = true;
            volume_out.Visible = false;
        }
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

    private void OnMusicButtonPressed()
    {
        int musicBus = AudioServer.GetBusIndex("Music");

        if (AudioServer.IsBusMute(musicBus))
        {
            // Unmute and resume playing
            AudioServer.SetBusMute(musicBus, false);

            volume_max.Visible = true;
            volume_out.Visible = false;

            if (MusicPlayer.StreamPaused) // Resume music if it was paused
            {
                MusicPlayer.StreamPaused = false;
            }
            else if (!MusicPlayer.Playing) // Play only if it was stopped
            {
                MusicPlayer.Play();
            }
        }
        else
        {
            // Mute and pause music
            AudioServer.SetBusMute(musicBus, true);

            volume_max.Visible = false;
            volume_out.Visible = true;
            MusicPlayer.StreamPaused = true;
        }
    }
    }

