using System.Linq;
using LOR_DiceSystem;
using VortexLabyrinth_Sa21341.Forgotten.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives;

namespace VortexLabyrinth_Sa21341.Forgotten.WiltonShadow.Passives
{
    public class PassiveAbility_WiltonShadow_Sa21341 : PassiveAbilityBase
    {
        private const int Stacks = 1;
        private PassiveAbility_ForgottenEgo_Sa_21341 _passive;

        public override void OnWaveStart()
        {
            owner.ignoreBloodyEffect = true;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_0CardCost_Sa21341());
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StartPoint_Sa21341());
            _passive = BattleObjectManager.instance.GetAliveList(owner.faction)
                    .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgo_Sa_21341>())?
                    .passiveDetail.PassiveList
                    .FirstOrDefault(x => x is PassiveAbility_ForgottenEgo_Sa_21341) as
                PassiveAbility_ForgottenEgo_Sa_21341;
        }

        public override int SpeedDiceNumAdder()
        {
            return _passive.GetPhase() < 4 ? 4 : 2;
        }

        public override void OnRoundStart()
        {
            if (_passive.GetPhase() < 4) return;
            if (_passive.GetCount() == 0) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, Stacks, owner);
            if (behavior.Detail == BehaviourDetail.Penetrate)
                behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, Stacks, owner);
        }
    }
}