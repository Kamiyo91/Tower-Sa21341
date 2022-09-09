using System;
using LOR_DiceSystem;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_CorruptedUniform_Sa21341 : BattleUnitBuf
    {
        private Random _random;
        private int _resistBreakNumber;
        private int _resistNumber;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _random = new Random();
            _resistNumber = _random.Next(1, 3);
            _resistBreakNumber = _random.Next(1, 3);
        }

        public override void OnRoundEnd()
        {
            _resistNumber = _random.Next(1, 3);
            _resistBreakNumber = _random.Next(1, 3);
        }

        public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
        {
            if (detail == BehaviourDetail.None) return base.GetResistHP(origin, detail);
            switch (_resistNumber)
            {
                case 1:
                    return detail == BehaviourDetail.Slash ? AtkResist.Endure : base.GetResistHP(origin, detail);
                case 2:
                    return detail == BehaviourDetail.Penetrate ? AtkResist.Endure : base.GetResistHP(origin, detail);
                case 3:
                    return detail == BehaviourDetail.Hit ? AtkResist.Endure : base.GetResistHP(origin, detail);
            }

            return base.GetResistHP(origin, detail);
        }

        public override AtkResist GetResistBP(AtkResist origin, BehaviourDetail detail)
        {
            if (detail == BehaviourDetail.None) return base.GetResistHP(origin, detail);
            switch (_resistBreakNumber)
            {
                case 1:
                    return detail == BehaviourDetail.Slash ? AtkResist.Endure : base.GetResistHP(origin, detail);
                case 2:
                    return detail == BehaviourDetail.Penetrate ? AtkResist.Endure : base.GetResistHP(origin, detail);
                case 3:
                    return detail == BehaviourDetail.Hit ? AtkResist.Endure : base.GetResistHP(origin, detail);
            }

            return base.GetResistHP(origin, detail);
        }
    }
}