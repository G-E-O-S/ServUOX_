namespace Server.Items
{
    public class DaemonBlood : Item, ICommodity
    {

        [Constructable]
        public DaemonBlood()
            : base(0xF7D)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public DaemonBlood(Serial serial)
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
