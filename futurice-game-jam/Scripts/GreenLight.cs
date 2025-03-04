using Godot;
using System;

namespace Transparency
{
    public partial class GreenLight : Light
    {
        [Signal]
        public delegate void GhostEnteredEventHandler(CharacterBody2D characterBody2D);
        [Signal]
        public delegate void GhostExitedEventHandler(CharacterBody2D characterBody2D);

        [Signal] public delegate void GreenDiamondEnteredEventHandler(CharacterBody2D Greendiamond);

        private Area2D _area; // Reference to Area2D

        public override void _Ready()
        {
            LightColor = Color.Green;
            _area = GetNode<Area2D>("Area2D"); // Ensure the path is correct!

            if (_area != null)
            {
                _area.BodyEntered += OnBodyEntered;
                _area.BodyExited += OnBodyExited;
            }
            else
            {
                GD.PrintErr("Area2D not found in GreenLight!");
            }
        }

        private void OnBodyEntered(Node2D body)
        {
            if (body is CharacterBody2D) // Check if it's a ghost
            {
                EmitSignal(nameof(GhostEntered), body);
            }
        }

        private void OnBodyExited(Node2D body)
        {
            if (body is CharacterBody2D) // Check if it's a ghost
            {
                EmitSignal(nameof(GhostExited), body);
            }
        }


    }
}