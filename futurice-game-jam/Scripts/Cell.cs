using Godot;
using System;
using System.Collections.Generic;
namespace Transparency
{
    public partial class Cell : Node2D
    {
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}
        private bool _collidable = false;
        public bool Collidable {get {return _collidable;} set {_collidable = value;}}
        private List<Occupier> _occupiers = new List<Occupier>();
        public List<Occupier> Occupiers {get {return _occupiers;}}
    }
}

