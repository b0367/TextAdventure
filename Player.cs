namespace TextAdventure
{
    internal class Player : Entity
    {
        public Player(int x, int y, Room room) : base(x, y, "Player", room, null, 'P') { } //Create a Player at (x,y) named Player in the current room that's not navigable and seen as "P"
    }
}