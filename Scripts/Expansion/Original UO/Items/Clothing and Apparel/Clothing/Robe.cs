using System;

namespace Server.Items
{
    [Flipable]
    public class Robe : BaseOuterTorso, IArcaneEquip
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


        public int TempHue { get; set; }
        public void Update()
        {
            if (this.IsArcane)
                this.ItemID = 0x26AE;
            else if (this.ItemID == 0x26AE)
                this.ItemID = 0x1F04;

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

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            if (this.IsArcane)
                this.LabelTo(from, 1061837, String.Format("{0}\t{1}", this.m_CurArcaneCharges, this.m_MaxArcaneCharges));
        }

        public void Flip()
        {
            if (this.ItemID == 0x1F03)
                this.ItemID = 0x1F04;
            else if (this.ItemID == 0x1F04)
                this.ItemID = 0x1F03;

            if (IsArcane && CurArcaneCharges == 0)
            {
                TempHue = Hue;
                Hue = 0;
            }
        }

        #endregion

        [Constructable]
        public Robe()
            : this(0)
        {
        }

        [Constructable]
        public Robe(int hue)
            : base(0x1F03, hue)
        {
            this.Weight = 3.0;
        }

        public Robe(Serial serial)
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
