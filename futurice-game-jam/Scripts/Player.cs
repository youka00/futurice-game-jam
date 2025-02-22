using Godot;
using System;

namespace Transparency
{
    public partial class Player : Area2D, Occupier
    {
        [Export] private float _speed = 20;
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}

        public CellOccupierType Type => CellOccupierType.Player;

        public Color AffectedByColor => Color.None;
        private string _redScenePath = "res://red_light.tscn";
        private string _greenScenePath = "res://green_light.tscn";
        private string _blueScenePath = "res://blue_light.tscn";
        private PackedScene _redScene = null;
        private PackedScene _greenScene = null;
        private PackedScene _blueScene = null;
        private Light _light = null;

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
            _redScene = ResourceLoader.Load<PackedScene>(_redScenePath);
            _blueScene = ResourceLoader.Load<PackedScene>(_blueScenePath);
            _greenScene = ResourceLoader.Load<PackedScene>(_greenScenePath);
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
            if (Input.IsActionJustPressed("Red"))
            {
                if (_light != null)
                {
                    _light.RemoveLightCollision();
                    _light.QueueFree();
                    _light = null;
                    _light = _redScene.Instantiate<RedLight>();
                    AddChild(_light);
                    _light.Position = Vector2.Zero;
                    _light.SetAffectedArea(_gridPosition);
                }
                else if (_light is RedLight)
                {}
                else
                {
                    _light = _redScene.Instantiate<RedLight>();
                    AddChild(_light);
                    _light.Position = Vector2.Zero;
                    _light.SetAffectedArea(_gridPosition);
                }
            }
            if (Input.IsActionJustPressed("Green"))
            {
                if (_light != null)
                {
                    _light.RemoveLightCollision();
                    _light.QueueFree();
                    _light = null;
                    _light = _greenScene.Instantiate<GreenLight>();
                    AddChild(_light);
                    _light.Position = Vector2.Zero;
                    _light.SetAffectedArea(_gridPosition);
                }
                else if (_light is GreenLight)
                {}
                else
                {
                    _light = _greenScene.Instantiate<GreenLight>();
                    AddChild(_light);
                    _light.Position = Vector2.Zero;
                    _light.SetAffectedArea(_gridPosition);
                }
            }
            if (Input.IsActionJustPressed("Blue"))
            {
                if (_light != null)
                {
                    _light.RemoveLightCollision();
                    _light.QueueFree();
                    _light = null;
                    _light = _blueScene.Instantiate<BlueLight>();
                    AddChild(_light);
                    _light.SetAffectedArea(_gridPosition);
                }
                else if (_light is BlueLight)
                {}
                else
                {
                    _light = _blueScene.Instantiate<BlueLight>();
                    AddChild(_light);
                    _light.SetAffectedArea(_gridPosition);
                }
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
        public void CollideWith(Occupier occupier)
        {
            switch (occupier.Type)
            {
                case CellOccupierType.Collectable: break;
                case CellOccupierType.Ghost: break;
                case CellOccupierType.Mineable: break;
                case CellOccupierType.Obstacle: break;
            }
        }
    }
}
