using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x0FBF, 0x0FC0)]
    public class MapmakersPen : BaseTool
    {
        [Constructable]
        public MapmakersPen()
            : base(0x0FBF)
        {
            Weight = 1.0;
        }

        [Constructable]
        public MapmakersPen(int uses)
            : base(uses, 0x0FBF)
        {
            Weight = 1.0;
        }

        public MapmakersPen(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem => DefCartography.CraftSystem;
        public override int LabelNumber => 1044167;// mapmaker's pen
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
