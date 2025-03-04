using Godot;
using System;

namespace Transparency
{
    public partial class RedDiamond : CharacterBody2D, Occupier
    {
        public CellOccupierType Type => CellOccupierType.Collectable;

        public Color AffectedByColor => Color.Red;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Visible = false;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
        public void Collect()
        {
            Level.Current.Score++;
            Cell parent = GetParent() as Cell;
            parent.Occupiers.Remove(this);
            if (Level.Current.Score > 2)
            {
                Level.Current.Win();
            }
            QueueFree();
        }
    }
}

