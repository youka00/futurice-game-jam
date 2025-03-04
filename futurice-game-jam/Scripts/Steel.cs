using Godot;
using System;

namespace Transparency
{
    public partial class Steel : Sprite2D, Occupier
    {
        public CellOccupierType Type => CellOccupierType.Obstacle;

        public Color AffectedByColor => Color.None;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
        public void CollideWith(Occupier occupier)
        {
            if (occupier.Type is CellOccupierType.Player)
            {

            }
        }
    }
}

