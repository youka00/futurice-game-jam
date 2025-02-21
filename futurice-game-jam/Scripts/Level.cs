using Godot;
using System;

namespace Transparency
{
    public partial class Level : Node2D
    {
        public static Level Current = null;
        private string _gridScenePath = "res://grid.tscn";
        private Grid _grid = null;
        public Grid CurrentGrid {get {return _grid;}}
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Current = this;
            PackedScene gridScene = ResourceLoader.Load<PackedScene>(_gridScenePath);
            _grid = gridScene.Instantiate<Grid>();
            _grid.GlobalPosition = Vector2.Zero;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
