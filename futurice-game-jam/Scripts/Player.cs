using Godot;
using System;

namespace Transparency
{
    public partial class Player : Area2D
    {
        [Export] private float _speed = 20;
        public enum Direction
        {
            None = 0,
            Up,
            Down,
            Left,
            Right,
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            Vector2 direction = ReadInput();
            Move(direction);
        }
        private Vector2 ReadInput()
        {
            Vector2 direction = Vector2.Zero;
            if (Input.IsActionJustPressed("MoveUp"))
            {
                return Vector2.Up;
            }
            if (Input.IsActionJustPressed("MoveDown"))
            {
                return Vector2.Down;
            }
            if (Input.IsActionJustPressed("MoveLeft"))
            {
                return Vector2.Left;
            }
            if (Input.IsActionJustPressed("MoveRight"))
            {
                return Vector2.Right;
            }
            return direction;
        }
        private void Move(Vector2 direction)
        {
            GlobalPosition += direction * Level.Current.CurrentGrid.CellHeight;
        }
    }
}
