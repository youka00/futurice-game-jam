using Godot;
using System;
using System.Collections.Generic;

namespace Transparency
{
    public partial class Player : Area2D, Occupier
    {
        [Export] private float _speed = 20;

        [Export] private CharacterBody2D characterBody2D; // Red Ghost
        [Export] private CharacterBody2D blue; // Blue Ghost
        [Export] private CharacterBody2D green; // Green Ghost

        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition { get { return _gridPosition; } set { _gridPosition = value; } }
        public CellOccupierType Type => CellOccupierType.Player;
        public Color AffectedByColor => Color.None;

        private string _redScenePath = "res://red_light.tscn";
        private string _greenScenePath = "res://green_light.tscn";
        private string _blueScenePath = "res://blue_light.tscn";

        private PackedScene _redScene = null;
        private PackedScene _greenScene = null;
        private PackedScene _blueScene = null;
        private Light _light = null;
        private Vector2I _lookingAt = Vector2I.Zero;
        private HitUnbreakable _hitUnbreakable = null;
        private BreakStone _breakStone = null;
        private Footsteps _footsteps = null;
        public enum Direction
        {
            None = 0,
            Up,
            Down,
            Left,
            Right,
        }

        public override void _Ready()
        {
            _gridPosition = new Vector2I(4,4);
            GlobalPosition = new Vector2(_gridPosition.X * 32, _gridPosition.Y * 32);
            _redScene = ResourceLoader.Load<PackedScene>(_redScenePath);
            _blueScene = ResourceLoader.Load<PackedScene>(_blueScenePath);
            _greenScene = ResourceLoader.Load<PackedScene>(_greenScenePath);
            _hitUnbreakable = GetNode<HitUnbreakable>("Hit Unbreakable");
            _breakStone = GetNode<BreakStone>("Break Stone");
            _footsteps = GetNode<Footsteps>("Footsteps");
            _lookingAt = _gridPosition + Vector2I.Up;
        }

        public override void _Process(double delta)
        {
            UpdateGhostVisibility();
            ReadInput();
        }

        private bool IsInRange()
        {
           return characterBody2D.GlobalPosition.DistanceTo(GlobalPosition) <= 0 ||
                   blue.GlobalPosition.DistanceTo(GlobalPosition) <= 0 ||
                   green.GlobalPosition.DistanceTo(GlobalPosition) <= 0;

        }

        private void UpdateGhostVisibility()
        {
            if (IsInRange())
            {
                blue.Visible = true;
                green.Visible = true;
                characterBody2D.Visible = true;

            }
            else
            {
                if (_light is BlueLight blueLight)
                {
                    blue.Visible = blueLight.IsInLightArea(blue.GlobalPosition);
                    green.Visible = false;
                    characterBody2D.Visible = false;
                }
                else if (_light is GreenLight greenLight)
                {
                    green.Visible = greenLight.IsInLightArea(green.GlobalPosition);
                    blue.Visible = false;
                    characterBody2D.Visible = false;
                }
                else if (_light is RedLight redLight)
                {
                    characterBody2D.Visible = redLight.IsInLightArea(characterBody2D.GlobalPosition);
                    blue.Visible = false;
                    green.Visible = false;
                }
                else
                {
                    blue.Visible = false;
                    green.Visible = false;
                    characterBody2D.Visible = false;
                }
            }
        }

        private void ReadInput()
        {
             if (Input.IsActionJustPressed("MoveUp"))
            {
                Move(Vector2I.Up);
                _footsteps.Play();
                _lookingAt = _gridPosition + Vector2I.Up;
            }
            if (Input.IsActionJustPressed("MoveDown"))
            {
                Move(Vector2I.Down);
                _footsteps.Play();
                _lookingAt = _gridPosition + Vector2I.Down;
            }
            if (Input.IsActionJustPressed("MoveLeft"))
            {
                Move(Vector2I.Left);
                _footsteps.Play();
                _lookingAt = _gridPosition + Vector2I.Left;
            }
            if (Input.IsActionJustPressed("MoveRight"))
            {
                Move(Vector2I.Right);
                _footsteps.Play();
                _lookingAt = _gridPosition + Vector2I.Right;
            }


            if (Input.IsActionJustPressed("Red")) ChangeLight(_redScene, typeof(RedLight));
            if (Input.IsActionJustPressed("Green")) ChangeLight(_greenScene, typeof(GreenLight));
            if (Input.IsActionJustPressed("Blue")) ChangeLight(_blueScene, typeof(BlueLight));
            if (Input.IsActionJustPressed("Mine"))
            {
                Mine(_lookingAt);
            }


        }

        private void ChangeLight(PackedScene newLightScene, Type lightType)
        {
            if (_light != null)
            {
                _light.QueueFree();
                _light = null;
            }

            _light = newLightScene.Instantiate() as Light;

            if (_light != null)
            {
                AddChild(_light);
                _light.Position = Vector2.Zero;
                _light.SetAffectedArea(_gridPosition);

                // Connect signals for RedLight
                if (_light is RedLight redLight)
                {
                    redLight.Connect("GhostEntered", Callable.From<Node2D>(OnGhostEnteredRedLight));
                    redLight.Connect("GhostExited", Callable.From<Node2D>(OnGhostExitedRedLight));
                }
                // Connect signals for GreenLight
                else if (_light is GreenLight greenLight)
                {
                    greenLight.Connect("GhostEntered", Callable.From<Node2D>(OnGhostEnteredGreenLight));
                    greenLight.Connect("GhostExited", Callable.From<Node2D>(OnGhostExitedGreenLight));
                }
                // Connect signals for BlueLight
                else if (_light is BlueLight blueLight)
                {
                    blueLight.Connect("GhostEntered", Callable.From<Node2D>(OnGhostEnteredBlueLight));
                    blueLight.Connect("GhostExited", Callable.From<Node2D>(OnGhostExitedBlueLight));
                }
            }

            UpdateGhostVisibility(); // Immediately update ghost visibility

        }

        private void Move(Vector2I direction)
        {
            if (!CheckCollision(_gridPosition + direction))
            {
                GlobalPosition += direction * Level.Current.CurrentGrid.CellHeight;
                _gridPosition += direction;
                foreach (Occupier i in Level.Current.CurrentGrid.Cells[_gridPosition.X, _gridPosition.Y].Occupiers)
                {
                    if (i.Type == CellOccupierType.Collectable)
                    {
                        i.Collect();
                    }
                }
            }
        }

        private bool CheckCollision(Vector2I gridPosition)
        {
            if (gridPosition.X < 0 || gridPosition.Y < 0 ||
                gridPosition.X > Level.Current.CurrentGrid.Width - 1 ||
                gridPosition.Y > Level.Current.CurrentGrid.Height - 1)
            {
                return true;
            }
            return Level.Current.CurrentGrid.Cells[gridPosition.X, gridPosition.Y].Collidable;
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
        public void Mine(Vector2I breakThis)
        {
            if (breakThis.X < 0 || breakThis.Y < 0 || breakThis.X > Level.Current.CurrentGrid.Width - 1 || breakThis.Y > Level.Current.CurrentGrid.Height - 1)
            {
                return;
            }
            Cell cell = Level.Current.CurrentGrid.Cells[breakThis.X, breakThis.Y];
            foreach (Occupier i in cell.Occupiers)
            {
                if (i.Type == CellOccupierType.Mineable)
                {
                    cell.Collidable = false;
                    i.Delete();
                    _breakStone.Play();
                    break;
                }
                else if (i.Type == CellOccupierType.Obstacle)
                {
                    _hitUnbreakable.Play();
                    break;
                }
            }
        }
        // Ensure Signal Methods Exist
        private void OnGhostEnteredRedLight(Node2D haamu)
        {
            GD.Print("Red Ghost entered red light!");
            haamu.Visible = true;
        }

        private void OnGhostExitedRedLight(Node2D haamu)
        {
            GD.Print("Red Ghost exited red light!");
            haamu.Visible = false;
        }

        private void OnGhostEnteredGreenLight(Node2D haamu)
        {
            GD.Print("Green Ghost entered green light!");
            haamu.Visible = true;
        }

        private void OnGhostExitedGreenLight(Node2D haamu)
        {
            GD.Print("Green Ghost exited green light!");
            haamu.Visible = false;
        }

        private void OnGhostEnteredBlueLight(Node2D haamu)
        {
            GD.Print("Blue Ghost entered blue light!");
            haamu.Visible = true;
        }

        private void OnGhostExitedBlueLight(Node2D haamu)
        {
            GD.Print("Blue Ghost exited blue light!");
            haamu.Visible = false;
        }
    }
}