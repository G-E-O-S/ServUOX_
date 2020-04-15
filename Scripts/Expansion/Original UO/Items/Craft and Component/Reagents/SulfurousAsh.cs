namespace Server.Items
{
    public class SulfurousAsh : Item, ICommodity
    {

        [Constructable]
        public SulfurousAsh()
            : base(0xF8C)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public SulfurousAsh(Serial serial)
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
