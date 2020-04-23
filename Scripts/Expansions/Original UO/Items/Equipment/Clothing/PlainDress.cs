namespace Server.Items
{
    [Flipable(0x1f01, 0x1f02)]
    public class PlainDress : BaseOuterTorso
    {
        [Constructable]
        public PlainDress()
            : this(0)
        {
        }

        [Constructable]
        public PlainDress(int hue)
            : base(0x1F01, hue)
        {
            Weight = 2.0;
        }

        public PlainDress(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
