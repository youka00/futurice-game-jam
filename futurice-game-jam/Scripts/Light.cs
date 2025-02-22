using Godot;

namespace Transparency
{
    public abstract partial class Light : Sprite2D
    {
        [Export] private Color _color = Color.None;
        public Color LightColor {get {return _color;} set {_color = value;}}
        private Vector2I _gridPosition = Vector2I.Zero;
        public Vector2I GridPosition{get {return _gridPosition;} set {_gridPosition = value;}}
        private Vector2I[] _affectedArea = new Vector2I[9];
        public Light()
        {
            int l = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    _affectedArea[l] = new Vector2I(i, j);
                }
            }
        }
        public void SetAffectedArea(Vector2I gridPosition)
        {
            int l = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    _affectedArea[l] = new Vector2I(i + gridPosition.X, j + gridPosition.Y);
                    l++;
                }
            }
            CheckLightCollision();
        }
        public void LightMove(Vector2I direction)
        {
            RemoveLightCollision();
            _gridPosition += direction;
            for (int i = 0; i > _affectedArea.Length; i++)
            {
                _affectedArea[i] += direction;
            }
            CheckLightCollision();
        }
        public void CheckLightCollision()
        {
            for (int i = 0; i > _affectedArea.Length; i++)
            {
                Vector2I collision = _affectedArea[i];
                if (collision.X < 0 || collision.Y < 0 || collision.X > Level.Current.CurrentGrid.Width - 1 || collision.Y > Level.Current.CurrentGrid.Height - 1)
                {
                }
                else
                {
                    Cell cell = Level.Current.CurrentGrid.Cells[collision.X, collision.Y];
                    foreach (Occupier j in cell.Occupiers)
                    {
                        j.LightCollision(_color);
                    }
                }
            }
        }
        public void RemoveLightCollision()
        {
            for (int i = 0; i > _affectedArea.Length; i++)
            {
                Vector2I collision = _affectedArea[i];
                if (collision.X < 0 || collision.Y < 0 || collision.X > Level.Current.CurrentGrid.Width - 1 || collision.Y > Level.Current.CurrentGrid.Height - 1)
                {
                }
                else
                {
                    Cell cell = Level.Current.CurrentGrid.Cells[collision.X, collision.Y];
                    foreach (Occupier j in cell.Occupiers)
                    {
                        j.RemoveLight();
                    }
                }

            }
        }
    }
}