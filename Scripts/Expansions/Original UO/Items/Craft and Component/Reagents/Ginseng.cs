namespace Server.Items
{
    public class Ginseng : Item, ICommodity
    {
        [Constructable]
        public Ginseng()
            : base(0xF85)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public Ginseng(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => true;
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
