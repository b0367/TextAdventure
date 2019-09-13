namespace TextAdventure
{
    internal class Player : Entity
    {
        public Player(int x, int y, Room room) : base(x, y, "Player", room, null, 'P') { }
    }
}