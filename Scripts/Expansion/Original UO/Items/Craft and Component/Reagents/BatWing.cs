namespace Server.Items
{
    public class BatWing : Item, ICommodity
    {
        [Constructable]
        public BatWing()
            : base(0xF78)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public BatWing(Serial serial)
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
