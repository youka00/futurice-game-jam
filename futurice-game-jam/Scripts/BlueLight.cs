using Godot;
using System;

namespace Transparency
{
    public partial class BlueLight : Light
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            LightColor = Color.Blue;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
