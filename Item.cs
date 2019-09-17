namespace TextAdventure
{

    public enum Slots
    {
        Head, Body, Feet, MainHand, OffHand, Misc1, Misc2, None
    }

    public class Item
    {
        public string Name;

        public Slots Slot;

        public char Representation;

        public Player Owner;

        public Item(string name, Player owner, Slots slot = Slots.None, char representation = '░')
        {
            Name = name;
            Slot = slot;
            Representation = representation;
            Owner = owner;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}