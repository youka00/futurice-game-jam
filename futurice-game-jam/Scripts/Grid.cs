using Godot;
using System;
using System.Collections.Generic;

namespace Transparency
{
    public enum OccupierScene
    {
        Rock,
        Steel,
        Diamond
    }
    public partial class Grid : Node2D
    {
        private string _cellScenePath = "res://cell.tscn";
        private string _rockScene = "res://rock.tscn";
        private string _steelScene = "res://steel.tscn";
        private string _redDiamondScene = "res://red_diamond.tscn";
        private Cell[,] _cells = null;
        public Cell[,] Cells {get {return _cells;}}
        [Export] private int _cellWidth = 32;
        [Export] private int _cellHeight = 32;
        [Export] private int _width = 9;
        [Export] private int _height = 9;
        public int Height {get {return _height;}}
        public int Width {get {return _width;}}

        public int CellHeight {get {return _cellHeight;}}
        public int CellWidth {get {return _cellWidth;}}
        private List<Cell> _freeCells = new List<Cell>();
        public override void _Ready()
        {
            PackedScene cellScene = ResourceLoader.Load<PackedScene>(_cellScenePath);
            _cells = new Cell[_width,_height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Cell cell = cellScene.Instantiate<Cell>();
                    AddChild(cell);
                    _cells[i, j] = cell;
                    cell.GridPosition = new Vector2I(i, j);
                    cell.GlobalPosition = new Vector2I(i * _cellWidth, j * _cellHeight);
                    _freeCells.Add(cell);
                }
            }
            MakePath();
            for (int i = 0; i < 5; i++)
            {
                CreateOccupier(OccupierScene.Steel, GetRandomFreeCell());
            }
            for (int i = 0; i < _freeCells.Count; i++)
            {
                CreateOccupier(OccupierScene.Diamond, GetRandomFreeCell());
            }
            int amount = _freeCells.Count;
            for (int i = 0; i < amount; i++)
            {
                CreateOccupier(OccupierScene.Rock, _freeCells[0]);
            }
        }
        public Cell GetRandomFreeCell()
        {
            int randomIndex = GD.RandRange(0, _freeCells.Count - 1);
            return _freeCells[randomIndex];
        }
        public void CreateOccupier(OccupierScene occupierScene, Cell cell)
        {
            PackedScene scene = null;
            switch (occupierScene)
            {
                case OccupierScene.Rock:
                scene = ResourceLoader.Load<PackedScene>(_rockScene);
                Rock rock = scene.Instantiate<Rock>();
                cell.AddChild(rock);
                cell.Occupiers.Add(rock);
                cell.Collidable = true;
                _freeCells.Remove(cell);
                break;
                case OccupierScene.Steel:
                scene = ResourceLoader.Load<PackedScene>(_steelScene);
                Steel steel = scene.Instantiate<Steel>();
                cell.AddChild(steel);
                cell.Occupiers.Add(steel);
                cell.Collidable = true;
                _freeCells.Remove(cell);
                break;
                case OccupierScene.Diamond:
                scene = ResourceLoader.Load<PackedScene>(_redDiamondScene);
                RedDiamond redDiamond = scene.Instantiate<RedDiamond>();
                cell.AddChild(redDiamond);
                cell.Occupiers.Add(redDiamond);
                break;
            }
        }
        // ÄLÄ KATO TÄNNE TÄÄL ON PURKKAA SILMÄNKANTAMATTOMIIN
        private void MakePath()
        {
            _freeCells.Remove(_cells[0,4]);
            _freeCells.Remove(_cells[0,5]);
            _freeCells.Remove(_cells[1,4]);
            _freeCells.Remove(_cells[1,5]);
            _freeCells.Remove(_cells[2,5]);
            _freeCells.Remove(_cells[2,4]);
            _freeCells.Remove(_cells[3,4]);
            _freeCells.Remove(_cells[3,5]);
            _freeCells.Remove(_cells[4,4]);
            _freeCells.Remove(_cells[5,4]);
            _freeCells.Remove(_cells[6,4]);
            _freeCells.Remove(_cells[7,4]);
            _freeCells.Remove(_cells[8,4]);
            _freeCells.Remove(_cells[4,5]);
            _freeCells.Remove(_cells[5,5]);
            _freeCells.Remove(_cells[6,5]);
            _freeCells.Remove(_cells[7,5]);
            _freeCells.Remove(_cells[8,5]);
        }
    }
}
