using System;
using Server.Engines.Craft;

namespace Server.Items
{
    #region Reward Clothing
    public class ZooMemberThighBoots : ThighBoots
    {
        public override int LabelNumber
        {
            get
            {
                return 1073221;
            }
        }// Britannia Royal Zoo Member

        [Constructable]
        public ZooMemberThighBoots()
            : this(0)
        {
        }

        [Constructable]
        public ZooMemberThighBoots(int hue)
            : base(hue)
        {
        }

        public ZooMemberThighBoots(Serial serial)
            : base(serial)
        {
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
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

    #endregion

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

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x2797, 0x27E2)]
    public class NinjaTabi : BaseShoes
    {
        [Constructable]
        public NinjaTabi()
            : this(0)
        {
        }

        [Constructable]
        public NinjaTabi(int hue)
            : base(0x2797, hue)
        {
            this.Weight = 2.0;
        }

        public NinjaTabi(Serial serial)
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

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x2796, 0x27E1)]
    public class SamuraiTabi : BaseShoes
    {
        [Constructable]
        public SamuraiTabi()
            : this(0)
        {
        }

        [Constructable]
        public SamuraiTabi(int hue)
            : base(0x2796, hue)
        {
            this.Weight = 2.0;
        }

        public SamuraiTabi(Serial serial)
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

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable(0x2796, 0x27E1)]
    public class Waraji : BaseShoes
    {
        [Constructable]
        public Waraji()
            : this(0)
        {
        }

        [Constructable]
        public Waraji(int hue)
            : base(0x2796, hue)
        {
            this.Weight = 2.0;
        }

        public Waraji(Serial serial)
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

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [FlipableAttribute(0x2FC4, 0x317A)]
    public class ElvenBoots : BaseShoes
    {
        public override CraftResource DefaultResource
        {
            get
            {
                return CraftResource.RegularLeather;
            }
        }

        public override Race RequiredRace
        {
            get
            {
                return Race.Elf;
            }
        }

        [Constructable]
        public ElvenBoots()
            : this(0)
        {
        }

        [Constructable]
        public ElvenBoots(int hue)
            : base(0x2FC4, hue)
        {
            this.Weight = 2.0;
        }

        public ElvenBoots(Serial serial)
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

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    public class JesterShoes : BaseShoes
    {
        public override int LabelNumber { get { return 1109617; } } // Jester Shoes

        [Constructable]
        public JesterShoes()
            : this(0)
        {
        }

        [Constructable]
        public JesterShoes(int hue)
            : base(0x7819, hue)
        {
        }

        public JesterShoes(Serial serial)
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
