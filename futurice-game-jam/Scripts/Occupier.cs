namespace Transparency
{
    public interface Occupier
    {
        CellOccupierType Type { get; }
        Color AffectedByColor { get; }
        public void CollideWith(Occupier occupier){}
        public void LightCollision(Color color){}
        public void RemoveLight(){}
    }

    public enum CellOccupierType
    {
        None,
        Player,
        Collectable,
        Mineable,
        Obstacle
    }
        public enum Color
    {
        None = 0,
        Red,
        Green,
        Blue
    }
}