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
    [Export] public AudioStreamPlayer MusicPlayer; // Reference to the music player

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

        // Initialize MusicButton text
        int musicBus = AudioServer.GetBusIndex("Music");
        if (AudioServer.IsBusMute(musicBus))
        {
            MusicButton.Text = "Music: Off";
        }
        else
        {
            MusicButton.Text = "Music: On";
        }
    }

    private void OnMusicButtonPressed()
    {
        int musicBus = AudioServer.GetBusIndex("Music");

        if (AudioServer.IsBusMute(musicBus))
        {
            // Unmute and resume playing
            AudioServer.SetBusMute(musicBus, false);
            MusicButton.Text = "Music: On";

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
            MusicButton.Text = "Music: Off";
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
