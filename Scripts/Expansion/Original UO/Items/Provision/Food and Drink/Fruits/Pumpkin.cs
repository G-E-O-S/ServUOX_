
namespace Server.Items
{
    [Flipable(0xC6A, 0xC6B)]
    public class Pumpkin : Food
    {
        [Constructable]
        public Pumpkin()
            : this(1)
        {
        }

        [Constructable]
        public Pumpkin(int amount)
            : base(amount, 0xC6A)
        {
            Weight = 1.0;
            FillFactor = 8;
        }

        public Pumpkin(Serial serial)
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
