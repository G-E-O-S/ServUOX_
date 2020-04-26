using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x2307, 0x2308)]
    public class FurBoots : BaseShoes
    {
        [Constructable]
        public FurBoots()
            : this(0)
        {
        }

        [Constructable]
        public FurBoots(int hue)
            : base(0x2307, hue)
        {
            this.Weight = 3.0;
        }

        public FurBoots(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
