namespace Server.Items
{
    public class BlackPearl : Item, ICommodity
    {

        [Constructable]
        public BlackPearl()
            : base(0xF7A)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public BlackPearl(Serial serial)
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
