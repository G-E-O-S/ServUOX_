using System;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a glowing orc corpse")]
    public class OrcishMage : BaseCreature
    {
        [Constructable]
        public OrcishMage()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an orcish mage";
            Body = 140;
            BaseSoundID = 0x45A;

            SetStr(116, 150);
            SetDex(91, 115);
            SetInt(161, 185);

            SetHits(70, 90);

            SetDamage(4, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.EvalInt, 60.1, 72.5);
            SetSkill(SkillName.Magery, 60.1, 72.5);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 50.1, 65.0);
            SetSkill(SkillName.Wrestling, 40.1, 50.0);

            Fame = 3000;
            Karma = -3000;

            VirtualArmor = 30;

            PackReg(6);
        }

        public OrcishMage(Serial serial)
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
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
        }

        public override void OnDeath(Container CorpseLoot)
        {
            if (Core.ML)
            {
                switch (Utility.Random(8))
                {
                    case 0: CorpseLoot.DropItem(new CorpseSkinScroll()); break;
                }
            }

            if (Core.UOTD && Utility.RandomDouble() > 0.05)
                CorpseLoot.DropItem(new OrcishKinMask());

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
