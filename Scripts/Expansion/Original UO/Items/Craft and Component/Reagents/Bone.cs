namespace Server.Items
{
    public class Bone : Item, ICommodity
    {

        [Constructable]
        public Bone()
            : base(0xf7e)
        {
            Stackable = true;
            Weight = 1.0;
        }

        public Bone(Serial serial)
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
