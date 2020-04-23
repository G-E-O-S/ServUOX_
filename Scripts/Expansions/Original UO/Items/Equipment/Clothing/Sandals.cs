using Server.Engines.Craft;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x170d, 0x170e)]
    public class Sandals : BaseShoes
    {
        public override CraftResource DefaultResource => CraftResource.RegularLeather;

        [Constructable]
        public Sandals()
            : this(0)
        {
        }

        [Constructable]
        public Sandals(int hue)
            : base(0x170D, hue)
        {
            Weight = 1.0;
        }

        public Sandals(Serial serial)
            : base(serial)
        {
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            return false;
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
