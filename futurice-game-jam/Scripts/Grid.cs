using Godot;
using System;

namespace Transparency
{
    public partial class Grid : Node2D
    {
        private Cell[,] _cells = null;
        [Export] private int _cellWidth = 32;
        [Export] private int _cellHeight = 32;
        [Export] private int _width = 16;
        [Export] private int _height = 16;
        public int Height {get {return _height;}}
        public int Width {get {return _width;}}

        public int CellHeight {get {return _cellHeight;}}
        public int CellWidth {get {return _cellWidth;}}
        public override void _Ready()
        {
            _cells = new Cell[_width,_height];
            int l = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _cells[i, j] = GetChild(l, false) as Cell;
                    _cells[i, j].GridPosition = new Vector2I(i, j);
                    _cells[i, j].GlobalPosition = new Vector2I(i * _cellWidth, j * _cellHeight);
                    l++;
                }
            }
        }
    }
}
