using Server.Engines.Craft;
using System;

namespace Server.Items
{
    [Alterable(typeof(DefTailoring), typeof(LeatherTalons), true)]
    [Flipable]
    public class ThighBoots : BaseShoes, IArcaneEquip
    {
        #region Arcane Impl
        private int m_MaxArcaneCharges, m_CurArcaneCharges;

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxArcaneCharges
        {
            get
            {
                return this.m_MaxArcaneCharges;
            }
            set
            {
                this.m_MaxArcaneCharges = value;
                this.InvalidateProperties();
                this.Update();
            }
        }
        public int TempHue { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int CurArcaneCharges
        {
            get
            {
                return this.m_CurArcaneCharges;
            }
            set
            {
                this.m_CurArcaneCharges = value;
                this.InvalidateProperties();
                this.Update();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsArcane
        {
            get
            {
                return (this.m_MaxArcaneCharges > 0 && this.m_CurArcaneCharges >= 0);
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            if (this.IsArcane)
                this.LabelTo(from, 1061837, String.Format("{0}\t{1}", this.m_CurArcaneCharges, this.m_MaxArcaneCharges));
        }

        public void Update()
        {
            if (this.IsArcane)
                this.ItemID = 0x26AF;
            else if (this.ItemID == 0x26AF)
                this.ItemID = 0x1711;
            if (IsArcane && CurArcaneCharges == 0)
            {
                TempHue = Hue;
                Hue = 0;
            }
        }

        public override void AddCraftedProperties(ObjectPropertyList list)
        {
            base.AddCraftedProperties(list);

            if (IsArcane)
                list.Add(1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges); // arcane charges: ~1_val~ / ~2_val~
        }

        public void Flip()
        {
            if (this.ItemID == 0x1711)
                this.ItemID = 0x1712;
            else if (this.ItemID == 0x1712)
                this.ItemID = 0x1711;
        }

        #endregion

        public override CraftResource DefaultResource
        {
            get
            {
                return CraftResource.RegularLeather;
            }
        }

        [Constructable]
        public ThighBoots()
            : this(0)
        {
        }

        [Constructable]
        public ThighBoots(int hue)
            : base(0x1711, hue)
        {
            this.Weight = 4.0;
        }

        public ThighBoots(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            if (this.IsArcane)
            {
                writer.Write(true);
                writer.Write((int)this.m_CurArcaneCharges);
                writer.Write((int)this.m_MaxArcaneCharges);
            }
            else
            {
                writer.Write(false);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        if (reader.ReadBool())
                        {
                            this.m_CurArcaneCharges = reader.ReadInt();
                            this.m_MaxArcaneCharges = reader.ReadInt();

                            if (this.Hue == 2118)
                                this.Hue = ArcaneGem.DefaultArcaneHue;
                        }

                        break;
                    }
            }
        }
    }
}
