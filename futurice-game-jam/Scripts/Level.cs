using Godot;
using System;

namespace Transparency
{
    public partial class Level : Node2D
    {
        public static Level Current = null;
        private Grid _grid = null;
        public Grid CurrentGrid {get {return _grid;}}
        [Export] private Ambience _ambience = null;
        private Win _win = null;
        private Loss _loss = null;
        public Level()
        {
            Current = this;
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _grid = GetNode<Grid>("Grid");
            _ambience = GetNode<Ambience>("Ambience");
            _ambience.Play();
            _loss = GetNode<Loss>("Loss");
            _win = GetNode<Win>("Win");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
        public void Win()
        {
            _ambience.Stop();
            _win.Play();
        }
        public void Lose()
        {
            _loss.Play();
            _ambience.Stop();
        }
    }
}
