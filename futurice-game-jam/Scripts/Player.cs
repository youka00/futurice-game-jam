using Godot;
using System;

namespace Transparency
{
    public partial class Player : Area2D
    {
        [Export] private float _speed = 20;
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}
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
            ReadInput();
        }
        private void ReadInput()
        {
            if (Input.IsActionJustPressed("MoveUp"))
            {
                Move(Vector2I.Up);
            }
            if (Input.IsActionJustPressed("MoveDown"))
            {
                Move(Vector2I.Down);
            }
            if (Input.IsActionJustPressed("MoveLeft"))
            {
                Move(Vector2I.Left);
            }
            if (Input.IsActionJustPressed("MoveRight"))
            {
                Move(Vector2I.Right);
            }
        }
        private void Move(Vector2I direction)
        {
            if (!CheckCollision(_gridPosition + direction))
            {
                GlobalPosition += direction * Level.Current.CurrentGrid.CellHeight;
                _gridPosition += direction;
            }
        }
        private bool CheckCollision(Vector2I gridposition)
        {
            if (gridposition.X < 0 || gridposition.Y < 0 || gridposition.X > Level.Current.CurrentGrid.Width - 1 || gridposition.Y > Level.Current.CurrentGrid.Height - 1)
            {
                return true;
            }
            return Level.Current.CurrentGrid.Cells[gridposition.X, gridposition.Y].Collidable;;
        }
    }
}
