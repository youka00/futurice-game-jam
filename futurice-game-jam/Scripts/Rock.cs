using Godot;
using System;

namespace Transparency
{
    public partial class Rock : Sprite2D, Occupier
    {
        public CellOccupierType Type => CellOccupierType.Mineable;

        public Color AffectedByColor => Color.None;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Frame = GD.RandRange(0 , 2);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
        public void Delete()
        {
            Cell parent = GetParent() as Cell;
            parent.Occupiers.Remove(this);
            QueueFree();
        }
    }
}

