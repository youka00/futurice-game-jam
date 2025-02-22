using Godot;
using System;
using System.Collections.Generic;
namespace Transparency
{
    public enum OccupierScene
    {
        Rock,
        Steel,
        Diamond
    }
    public partial class Cell : Node2D
    {
        private string _rockScene = "res://rock.tscn";
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}
        private bool _collidable = false;
        public bool Collidable {get {return _collidable;}}
        private List<Occupier> _occupiers = new List<Occupier>();
        public List<Occupier> Occupiers {get {return _occupiers;}}
        public void CreateOccupier(OccupierScene occupierScene)
        {
            switch (occupierScene)
            {
                case OccupierScene.Rock:
                PackedScene scene = ResourceLoader.Load<PackedScene>(_rockScene);
                Rock rock = scene.Instantiate<Rock>();
                GD.Print(GridPosition);
                AddChild(rock);
                GD.Print(rock.GlobalPosition);
                _occupiers.Add(rock);
                _collidable = true;
                break;
            }
        }
    }
}

