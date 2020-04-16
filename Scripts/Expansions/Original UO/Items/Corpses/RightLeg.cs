namespace Server.Items
{
    public class RightLeg : Item
    {
        [Constructable]
        public RightLeg()
            : base(0x1DA4)
        {
        }

        public RightLeg(Serial serial)
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