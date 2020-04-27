using System;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    interface IRedSolen
    {
    }

    public partial class SolenHelper
    {
        public static bool CheckRedFriendship(Mobile m)
        {
            if (m is BaseCreature bc)
            {
                if (bc.Controlled && bc.ControlMaster is PlayerMobile)
                    return CheckRedFriendship(bc.ControlMaster);
                else if (bc.Summoned && bc.SummonMaster is PlayerMobile)
                    return CheckRedFriendship(bc.SummonMaster);
            }
            return m is PlayerMobile player && player.SolenFriendship == SolenFriendship.Red;
        }

        public static void OnRedDamage(Mobile from)
        {
            if (from is BaseCreature bc)
            {
                if (bc.Controlled && bc.ControlMaster is PlayerMobile)
                    OnRedDamage(bc.ControlMaster);
                else if (bc.Summoned && bc.SummonMaster is PlayerMobile)
                    OnRedDamage(bc.SummonMaster);
            }

            if (from is PlayerMobile player && player.SolenFriendship == SolenFriendship.Red)
            {
                player.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1054103); // The solen revoke their friendship. You will now be considered an intruder.
                player.SolenFriendship = SolenFriendship.None;
            }
        }
    }

    public class RSQEggSac : Item, ICarvable
    {
        private SpawnTimer m_Timer;
        public override string DefaultName => "egg sac";

        [Constructable]
        public RSQEggSac()
            : base(4316)
        {
            Movable = false;
            Hue = 350;

            m_Timer = new SpawnTimer(this);
            m_Timer.Start();
        }

        public bool Carve(Mobile from, Item item)
        {
            Effects.PlaySound(GetWorldLocation(), Map, 0x027);
            Effects.SendLocationEffect(GetWorldLocation(), Map, 0x3728, 10, 10, 0, 0);

            from.SendMessage("You destroy the egg sac.");
            Delete();
            m_Timer.Stop();

            return true;
        }

        public RSQEggSac(Serial serial)
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

            m_Timer = new SpawnTimer(this);
            m_Timer.Start();
        }

        private class SpawnTimer : Timer
        {
            private Item m_Item;

            public SpawnTimer(Item item)
                : base(TimeSpan.FromSeconds(Utility.RandomMinMax(5, 10)))
            {
                Priority = TimerPriority.FiftyMS;
                m_Item = item;
            }

            protected override void OnTick()
            {
                if (m_Item.Deleted)
                    return;

                Mobile spawn;

                switch (Utility.Random(2))
                {
                    case 0:
                        spawn = new RedSolenWarrior();
                        spawn.MoveToWorld(m_Item.Location, m_Item.Map);
                        m_Item.Delete();
                        break;
                    case 1:
                        spawn = new RedSolenWorker();
                        spawn.MoveToWorld(m_Item.Location, m_Item.Map);
                        m_Item.Delete();
                        break;
                }
            }
        }
    }
    
    public class BaseRedSolen : BaseCreature, IRedSolen
    {
        public BaseRedSolen(AIType type, FightMode mode, int rf, int rp, double activespeed, double passivespeed)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            AI = type;
            FightMode = mode;
            RangeFight = rf;
            RangePerception = rp;
            ActiveSpeed = activespeed;
            PassiveSpeed = passivespeed;
        }

        public BaseRedSolen(Serial serial)
            : base(serial)
        {
        }

        public virtual bool Male => true;

        public override int GetAngerSound() { return Male ? 0xB5 : 0x259; }
        public override int GetIdleSound() { return Male ? 0xB5 : 0x259; }
        public override int GetAttackSound() { return Male ? 0x289 : 0x195; }
        public override int GetHurtSound() { return Male ? 0xBC : 0x250; }
        public override int GetDeathSound() { return Male ? 0xE4 : 0x25B; }

        public override void OnDeath(Container CorpseLoot)
        {
            if (1 > Utility.Random(100))
            {
                PicnicBasket basket = new PicnicBasket();

                basket.DropItem(new BeverageBottle(BeverageType.Wine));
                basket.DropItem(new CheeseWedge());

                CorpseLoot.DropItem(basket);
            }

            CorpseLoot.DropItem(new ZoogiFungus((0.05 < Utility.RandomDouble()) ? 4 : 16));

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (SolenHelper.CheckRedFriendship(m))
                return false;
            else
                return base.IsEnemy(m);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            SolenHelper.OnRedDamage(from);
            base.OnDamage(amount, from, willKill);
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
    
    public class BaseRedSolenQueen : BaseRedSolen
    {
        private static bool m_Laid;
        private DateTime m_NextAcidBreath;

        public BaseRedSolenQueen(AIType type, FightMode mode, int rf, int rp, double activespeed, double passivespeed)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            AI = type;
            FightMode = mode;
            RangeFight = rf;
            RangePerception = rp;
            ActiveSpeed = activespeed;
            PassiveSpeed = passivespeed;
        }

        public BaseRedSolenQueen(Serial serial)
            : base(serial)
        {
        }

        public bool BurstSac { get; private set; }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (attacker.Weapon is BaseRanged)
                BeginAcidBreath();

            else if (Map != null && attacker != this && m_Laid == false && 0.20 > Utility.RandomDouble())
            {
                RSQEggSac sac = new RSQEggSac();

                sac.MoveToWorld(Location, Map);
                PlaySound(0x582);
                Say(1114445); // * * The solen queen summons her workers to her aid! * *
                m_Laid = true;
                EggSacTimer e = new EggSacTimer();
                e.Start();
            }

            base.OnGotMeleeAttack(attacker);
        }

        public override bool OnBeforeDeath()
        {
            SpillAcid(4);
            return base.OnBeforeDeath();
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            SolenHelper.OnRedDamage(from);

            if (!willKill)
            {
                if (!BurstSac)
                {
                    if (Hits < 50)
                    {
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "* The solen's acid sac is burst open! *");
                        BurstSac = true;
                    }
                }
                else if (from != null && from != this && InRange(from, 1))
                {
                    SpillAcid(from, 1);
                }
            }

            base.OnDamage(amount, from, willKill);
        }

        public override void OnDamagedBySpell(Mobile attacker)
        {
            if (0.80 >= Utility.RandomDouble())
                BeginAcidBreath();

            base.OnDamagedBySpell(attacker);
        }

        public void BeginAcidBreath()
        {
            PlayerMobile m = Combatant as PlayerMobile;

            if (m == null || m.Deleted || !m.Alive || !Alive || m_NextAcidBreath > DateTime.Now || !CanBeHarmful(m))
                return;

            PlaySound(0x118);
            MovingEffect(m, 0x36D4, 1, 0, false, false, 0x3F, 0);

            TimeSpan delay = TimeSpan.FromSeconds(GetDistanceToSqrt(m) / 5.0);
            Timer.DelayCall(delay, new TimerStateCallback<Mobile>(EndAcidBreath), m);

            m_NextAcidBreath = DateTime.Now + TimeSpan.FromSeconds(5);
        }

        public void EndAcidBreath(Mobile m)
        {
            if (m == null || m.Deleted || !m.Alive || !Alive)
                return;

            if (0.2 >= Utility.RandomDouble())
                m.ApplyPoison(this, Poison.Greater);

            AOS.Damage(m, Utility.RandomMinMax(100, 120), 0, 0, 0, 100, 0);
        }

        private class EggSacTimer : Timer
        {
            public EggSacTimer()
                : base(TimeSpan.FromSeconds(10))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Laid = false;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            // if (SpawnsEggs) writer.Write(BurstSac);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();

            /*
            switch (version)
            {
                case 1:
                    {
                        BurstSac = reader.ReadBool();
                        break;
                    }
            }
            */
        }
    }

    [CorpseName("a solen infiltrator corpse")]
    public class RedSolenInfiltratorQueen : BaseRedSolen
    {
        [Constructable]
        public RedSolenInfiltratorQueen()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a red solen infiltrator queen";
            Body = 783;
            BaseSoundID = 959;

            SetStr(326, 350);
            SetDex(141, 165);
            SetInt(96, 120);

            SetHits(151, 162);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 70);
            SetDamageType(ResistanceType.Poison, 30);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 30, 35);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 35, 40);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 90.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Wrestling, 90.0);

            Fame = 6500;
            Karma = -6500;

            VirtualArmor = 50;
        }

        public RedSolenInfiltratorQueen(Serial serial)
            : base(serial)
        {
        }

        public override bool Male => false;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
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

    [CorpseName("a solen infiltrator corpse")]
    public class RedSolenInfiltratorWarrior : BaseRedSolen
    {
        [Constructable]
        public RedSolenInfiltratorWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a red solen infiltrator";
            Body = 782;
            BaseSoundID = 959;

            SetStr(206, 230);
            SetDex(121, 145);
            SetInt(66, 90);

            SetHits(96, 107);

            SetDamage(5, 15);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 20, 35);
            SetResistance(ResistanceType.Fire, 20, 35);
            SetResistance(ResistanceType.Cold, 10, 25);
            SetResistance(ResistanceType.Poison, 20, 35);
            SetResistance(ResistanceType.Energy, 10, 25);

            SetSkill(SkillName.MagicResist, 80.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 40;
        }

        public RedSolenInfiltratorWarrior(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.Gems, Utility.RandomMinMax(1, 4));
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

    [CorpseName("a solen queen corpse")]
    public class RedSolenQueen : BaseRedSolenQueen
    {
        [Constructable]
        public RedSolenQueen()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a red solen queen";
            Body = 783;
            BaseSoundID = 959;

            SetStr(296, 320);
            SetDex(121, 145);
            SetInt(76, 100);

            SetHits(151, 162);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 70);
            SetDamageType(ResistanceType.Poison, 30);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 30, 35);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 35, 40);
            SetResistance(ResistanceType.Energy, 25, 30);

            SetSkill(SkillName.MagicResist, 70.0);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Wrestling, 90.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 45;
        }

        public RedSolenQueen(Serial serial)
            : base(serial)
        {
        }

        public override bool Male => false;

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomDouble() < 0.05)
                CorpseLoot.DropItem(new BallOfSummoning());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a solen warrior corpse")]
    public class RedSolenWarrior : BaseRedSolen
    {
        [Constructable]
        public RedSolenWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a red solen warrior";
            Body = 782;
            BaseSoundID = 959;

            SetStr(196, 220);
            SetDex(101, 125);
            SetInt(36, 60);

            SetHits(96, 107);

            SetDamage(5, 15);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 20, 35);
            SetResistance(ResistanceType.Fire, 20, 35);
            SetResistance(ResistanceType.Cold, 10, 25);
            SetResistance(ResistanceType.Poison, 20, 35);
            SetResistance(ResistanceType.Energy, 10, 25);

            SetSkill(SkillName.MagicResist, 60.0);
            SetSkill(SkillName.Tactics, 80.0);
            SetSkill(SkillName.Wrestling, 80.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 35;
        }

        public RedSolenWarrior(Serial serial)
            : base(serial)
        {
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Utility.RandomDouble() < 0.05)
                CorpseLoot.DropItem(new BraceletOfBinding());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, Utility.RandomMinMax(1, 4));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a solen worker corpse")]
    public class RedSolenWorker : BaseRedSolen
    {
        [Constructable]
        public RedSolenWorker()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a red solen worker";
            Body = 781;
            BaseSoundID = 959;

            SetStr(96, 120);
            SetDex(81, 105);
            SetInt(36, 60);

            SetHits(58, 72);

            SetDamage(5, 7);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 10, 20);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.MagicResist, 60.0);
            SetSkill(SkillName.Tactics, 65.0);
            SetSkill(SkillName.Wrestling, 60.0);

            Fame = 1500;
            Karma = -1500;

            VirtualArmor = 28;
        }

        public RedSolenWorker(Serial serial)
            : base(serial)
        {
        }

        public override int GetAngerSound() { return 0x269; }
        public override int GetIdleSound() { return 0x269; }
        public override int GetAttackSound() { return 0x186; }
        public override int GetHurtSound() { return 0x1BE; }
        public override int GetDeathSound() { return 0x8E; }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Gems, Utility.RandomMinMax(1, 2));
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
