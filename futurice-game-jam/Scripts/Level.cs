using Godot;
using System;

namespace Transparency
{
    public partial class Level : Node2D
    {
        public static Level Current = null;
        private Grid _grid = null;
        public Grid CurrentGrid {get {return _grid;}}
        public Level()
        {
            Current = this;
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _grid = GetNode<Grid>("Grid");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
