namespace Server.Items
{
    public class DragonBlood : Item, ICommodity
    {
        [Constructable]
        public DragonBlood()
            : base(0x4077)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public DragonBlood(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description => LabelNumber;
        bool ICommodity.IsDeedable => (Core.ML);
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
