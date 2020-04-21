using System;

namespace Server.Items
{
    public class DeathRobe : Robe
    {
        private Timer m_DecayTimer;
        private DateTime m_DecayTime;

        private static readonly TimeSpan m_DefaultDecayTime = TimeSpan.FromMinutes(1.0);

        public override bool DisplayLootType
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public DeathRobe()
        {
            this.LootType = LootType.Newbied;
            this.Hue = 2301;
            this.BeginDecay(m_DefaultDecayTime);
        }

        public new bool Scissor(Mobile from, Scissors scissors)
        {
            from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
            return false;
        }

        public void BeginDecay()
        {
            this.BeginDecay(m_DefaultDecayTime);
        }

        private void BeginDecay(TimeSpan delay)
        {
            if (this.m_DecayTimer != null)
                this.m_DecayTimer.Stop();

            this.m_DecayTime = DateTime.UtcNow + delay;

            this.m_DecayTimer = new InternalTimer(this, delay);
            this.m_DecayTimer.Start();
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            this.BeginDecay(m_DefaultDecayTime);

            return true;
        }

        public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            if (this.m_DecayTimer != null)
            {
                this.m_DecayTimer.Stop();
                this.m_DecayTimer = null;
            }

            return true;
        }

        public override void OnAfterDelete()
        {
            if (this.m_DecayTimer != null)
                this.m_DecayTimer.Stop();

            this.m_DecayTimer = null;
        }

        private class InternalTimer : Timer
        {
            private readonly DeathRobe m_Robe;

            public InternalTimer(DeathRobe c, TimeSpan delay)
                : base(delay)
            {
                this.m_Robe = c;
                this.Priority = TimerPriority.FiveSeconds;
            }

            protected override void OnTick()
            {
                if (this.m_Robe.Parent != null || this.m_Robe.IsLockedDown)
                    this.Stop();
                else
                    this.m_Robe.Delete();
            }
        }

        public DeathRobe(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write(this.m_DecayTimer != null);

            if (this.m_DecayTimer != null)
                writer.WriteDeltaTime(this.m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
                        if (reader.ReadBool())
                        {
                            this.m_DecayTime = reader.ReadDeltaTime();
                            this.BeginDecay(this.m_DecayTime - DateTime.UtcNow);
                        }
                        break;
                    }
                case 1:
                case 0:
                    {
                        if (this.Parent == null)
                            this.BeginDecay(m_DefaultDecayTime);
                        break;
                    }
            }

            if (version < 1 && this.Hue == 0)
                this.Hue = 2301;
        }
    }
}