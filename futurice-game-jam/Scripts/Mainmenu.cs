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
    [Export] public Button MusicButton;
    [Export] public AudioStreamPlayer MusicPlayer;
    [Export] public Sprite2D volume_max;
    [Export] public Sprite2D volume_out;

    public override void _Ready()
    {
        PlayButton.Pressed += OnPlayButtonPressed;
        QuitButton.Pressed += OnQuitButtonPressed;
        HowplayButton.Pressed += OnHowplayButtonPressed;
        CreditsButton.Pressed += OnCreditsButtonPressed;
        MusicButton.Pressed += OnMusicButtonPressed;
        CloseCreditsButton.Pressed += () => CreditsPopup.Visible = false;
        CloseHowtoplayButton.Pressed += () => HowtoplayPopup.Visible = false;

        // Ensure MusicPlayer is assigned
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
}