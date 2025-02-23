using Godot;
using System;

public partial class MainMenuMusic : AudioStreamPlayer
{
    public override void _Ready()
    {
        Finished += PlayAgain;
    }

    private void PlayAgain()
    {
        Play(); // Loops the music automatically
    }
}
