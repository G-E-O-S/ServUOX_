namespace Server.Items
{
    public class DeadWood : Item, ICommodity
    {

        [Constructable]
        public DeadWood()
            : base(0xF90)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public DeadWood(Serial serial)
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
