using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class OrcishLord : BaseCreature
    {
        [Constructable]
        public OrcishLord()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an orcish lord";
            Body = 138;
            BaseSoundID = 0x45A;

            SetStr(147, 215);
            SetDex(91, 115);
            SetInt(61, 85);

            SetHits(95, 123);

            SetDamage(4, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 70.1, 85.0);
            SetSkill(SkillName.Swords, 60.1, 85.0);
            SetSkill(SkillName.Tactics, 75.1, 90.0);
            SetSkill(SkillName.Wrestling, 60.1, 85.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 42;
        }

        public OrcishLord(Serial serial)
            : base(serial)
        {
        }

        public override InhumanSpeech SpeechType { get { return InhumanSpeech.Orc; } }
        public override bool CanRummageCorpses { get { return true; } }
        public override int TreasureMapLevel { get { return 1; } }
        public override int Meat { get { return 1; } }
        public override TribeType Tribe { get { return TribeType.Orc; } }
        public override OppositionGroup OppositionGroup { get { return OppositionGroup.SavagesAndOrcs; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Average);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            // TODO: evil orc helm
            switch (Utility.Random(5))
            {
                case 0:
                    CorpseLoot.DropItem(new Lockpick());
                    break;
                case 1:
                    CorpseLoot.DropItem(new MortarPestle());
                    break;
                case 2:
                    CorpseLoot.DropItem(new Bottle());
                    break;
                case 3:
                    CorpseLoot.DropItem(new RawRibs());
                    break;
                case 4:
                    CorpseLoot.DropItem(new Shovel());
                    break;
            }

            CorpseLoot.DropItem(new RingmailChest());

            if (Utility.RandomDouble() > 0.3)
                CorpseLoot.DropItem(Loot.RandomPossibleReagent());

            if (Core.UOR && Utility.RandomDouble() > .2)
                CorpseLoot.DropItem(new BolaBall());

            if (Core.HS && Utility.RandomDouble() > 0.5)
                CorpseLoot.DropItem(new Yeast());

            base.OnDeath(CorpseLoot);
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
