namespace Server.Items
{
    [FlipableAttribute(0x494E, 0x494D)]
    public class StatueGargoyleEast : Item
    {
        [Constructable]
        public StatueGargoyleEast()
            : base(0x494E)
        {
            Weight = 1.0;
        }

        public StatueGargoyleEast(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x494D, 0x494E)]
    public class StatueGargoyleSouth : Item
    {
        [Constructable]
        public StatueGargoyleSouth()
            : base(0x494D)
        {
            Weight = 1.0;
        }

        public StatueGargoyleSouth(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
