
namespace Server.Mobiles
{
    [CorpseName("a dragon corpse")]
    public class AncientWyrm : BaseCreature
    {
        [Constructable]
        public AncientWyrm()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ancient wyrm";
            Body = 46;
            BaseSoundID = 362;

            SetStr(1096, 1185);
            SetDex(86, 175);
            SetInt(686, 775);

            SetHits(658, 711);

            SetDamage(29, 35);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 80, 90);
            SetResistance(ResistanceType.Cold, 70, 80);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.Meditation, 52.5, 75.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 70;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public AncientWyrm(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement { get { return !Controlled; } }
        public override bool AutoDispel { get { return !Controlled; } }
        public override int TreasureMapLevel { get { return 5; } }
        public override int Meat { get { return 19; } }
        public override int DragonBlood { get { return 8; } }
        public override int Hides { get { return 40; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override int Scales { get { return 12; } }
        public override ScaleType ScaleType { get { return (ScaleType)Utility.Random(4); } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }
        public override bool CanFly { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        // public override Poison HitPoison { get { return Utility.RandomBool() ? Poison.Lesser : Poison.Regular; } }

        public override int GetIdleSound() { return 0x2D3; }
        public override int GetHurtSound() { return 0x2D1; }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 5);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    [CorpseName("a dragon corpse")]
    public class Dragon : BaseCreature
    {
        [Constructable]
        public Dragon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a dragon";
            Body = Utility.RandomList(12, 59);
            BaseSoundID = 362;

            SetStr(796, 825);
            SetDex(86, 105);
            SetInt(436, 475);

            SetHits(478, 495);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 70);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 25, 35);
            SetResistance(ResistanceType.Energy, 35, 45);

            SetSkill(SkillName.EvalInt, 30.1, 40.0);
            SetSkill(SkillName.Magery, 30.1, 40.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 92.5);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 3;
            MinTameSkill = 93.9;

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Dragon(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement { get { return !Controlled; } }
        public override bool AutoDispel { get { return !Controlled; } }
        public override int TreasureMapLevel { get { return 4; } }
        public override int Meat { get { return 19; } }
        public override int DragonBlood { get { return 8; } }
        public override int Hides { get { return 20; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override int Scales { get { return 7; } }
        public override ScaleType ScaleType { get { return (Body == 12 ? ScaleType.Yellow : ScaleType.Red); } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }
        public override bool CanFly { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.Gems, 8);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    [CorpseName("a drake corpse")]
    public class Drake : BaseCreature
    {
        [Constructable]
        public Drake()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a drake";
            Body = Utility.RandomList(60, 61);
            BaseSoundID = 362;

            SetStr(401, 430);
            SetDex(133, 152);
            SetInt(101, 140);

            SetHits(241, 258);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Fire, 20);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 65.1, 80.0);
            SetSkill(SkillName.Tactics, 65.1, 90.0);
            SetSkill(SkillName.Wrestling, 65.1, 80.0);

            Fame = 5500;
            Karma = -5500;

            VirtualArmor = 46;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 84.3;

            PackReg(3);

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Drake(Serial serial)
            : base(serial)
        {
        }

        public override bool ReacquireOnMovement { get { return !Controlled; } }
        public override bool AutoDispel { get { return !Controlled; } }
        public override int TreasureMapLevel { get { return 2; } }
        public override int Meat { get { return 10; } }
        public override int DragonBlood { get { return 8; } }
        public override int Hides { get { return 20; } }
        public override HideType HideType { get { return HideType.Horned; } }
        public override int Scales { get { return 2; } }
        public override ScaleType ScaleType { get { return (Body == 60 ? ScaleType.Yellow : ScaleType.Red); } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }
        public override bool CanFly { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
