using LOR_DiceSystem;

namespace VortexLabyrinth_Sa21341.Sae.Buffs
{
    public class BattleUnitBuf_DefStance_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_DefStance_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "DefStance_Sa21341";
        protected override string keywordIconId => "DefStance_Sa21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Evasion || behavior.Detail == BehaviourDetail.Guard)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
            if (behavior.Detail == BehaviourDetail.Slash || behavior.Detail == BehaviourDetail.Penetrate ||
                behavior.Detail == BehaviourDetail.Hit)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = -1
                    });
        }

        public override bool IsImmune(BufPositiveType posType)
        {
            return posType == BufPositiveType.Negative;
        }

        public override void OnRoundEndTheLast()
        {
            if (_owner.hp >= _owner.MaxHp * 0.25f) return;
            _owner.RecoverHP(4);
            if (_owner.hp > _owner.MaxHp * 0.25f) _owner.SetHp((int)(_owner.MaxHp * 0.25f));
        }
    }
}