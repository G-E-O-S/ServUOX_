namespace Server.Items
{
    public class NoxCrystal : Item, ICommodity
    {

        [Constructable]
        public NoxCrystal()
            : base(0xF8E)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public NoxCrystal(Serial serial)
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
