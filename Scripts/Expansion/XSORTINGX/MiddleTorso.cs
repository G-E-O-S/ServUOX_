using System;
using Server.Engines.Craft;

namespace Server.Items
{

    [Flipable(0x27A1, 0x27EC)]
    public class JinBaori : BaseMiddleTorso
    {
        [Constructable]
        public JinBaori()
            : this(0)
        {
        }

        [Constructable]
        public JinBaori(int hue)
            : base(0x27A1, hue)
        {
            this.Weight = 3.0;
        }

        public JinBaori(Serial serial)
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

    [Flipable(0x46B4, 0x46B5)]
    public class GargishSash : BaseClothing
    {
        [Constructable]
        public GargishSash()
            : this(0)
        {
        }

        [Constructable]
        public GargishSash(int hue)
            : base(0x46B4, Layer.MiddleTorso, hue)
        {
            this.Weight = 1.0;
        }

        public GargishSash(Serial serial)
            : base(serial)
        {
        }

        public override Race RequiredRace
        {
            get
            {
                return Race.Gargoyle;
            }
        }
        public override bool CanBeWornByGargoyles
        {
            get
            {
                return true;
            }
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
