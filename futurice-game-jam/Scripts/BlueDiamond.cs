using Godot;
using System;

namespace Transparency
{
    public partial class BlueDiamond : Sprite2D, Occupier
    {
        public CellOccupierType Type => CellOccupierType.Collectable;

        public Color AffectedByColor => Color.Blue;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Visible = false;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
        public void LightCollision(Color color)
        {
            if (color == AffectedByColor)
            {
                Visible = true;
            }
        }
        public void RemoveLight()
        {
            Visible = false;
        }
    }
}

