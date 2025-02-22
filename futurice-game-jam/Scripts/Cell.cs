using Godot;
using System;
namespace Transparency
{
    public partial class Cell : Sprite2D
    {
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}
        private bool _collidable = false;
        public bool Collidable {get {return _collidable;}}
    }
}

