using Server.Targeting;

namespace Server.Spells.Second
{
    public class HarmSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Harm", "An Mani",
            212,
            Core.AOS ? 9001 : 9041,
            Reagent.Nightshade,
            Reagent.SpidersSilk);
        public HarmSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle => SpellCircle.Second;
        public override bool DelayedDamage => false;
        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override double GetSlayerDamageScalar(Mobile target)
        {
            return 1.0; //This spell isn't affected by slayer spellbooks
        }

        public void Target(IDamageable m)
        {
            Mobile mob = m as Mobile;

            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);
                Mobile source = Caster;

                SpellHelper.CheckReflect((int)Circle, ref source, ref m);

                double damage = 0;

                if (Core.AOS)
                {
                    damage = GetNewAosDamage(17, 1, 5, m);
                }
                else if (mob != null)
                {
                    damage = Utility.Random(1, 15);

                    if (CheckResisted(mob))
                    {
                        damage *= 0.75;

                        mob.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                    }

                    damage *= GetDamageScalar(mob);
                }

                if (!Caster.InRange(m, 2))
                {
                    damage *= 0.25; // 1/4 damage at > 2 tile range
                }
                else if (!Caster.InRange(m, 1))
                {
                    damage *= 0.50; // 1/2 damage at 2 tile range
                }

                if (Core.AOS)
                {
                    if (mob != null)
                    {
                        mob.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);
                        mob.PlaySound(0x0FC);
                    }
                    else
                    {
                        Effects.SendLocationParticles(m, 0x374A, 10, 30, 1153, 2, 5013, 0);
                        Effects.PlaySound(m.Location, m.Map, 0x0FC);
                    }
                }
                else if (mob != null)
                {
                    mob.FixedParticles(0x374A, 10, 15, 5013, EffectLayer.Waist);
                    mob.PlaySound(0x1F1);
                }

                if (damage > 0)
                {
                    SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly HarmSpell m_Owner;
            public InternalTarget(HarmSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IDamageable)
                {
                    m_Owner.Target((IDamageable)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}