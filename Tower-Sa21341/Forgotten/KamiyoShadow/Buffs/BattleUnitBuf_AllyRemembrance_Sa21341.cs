using System.Linq;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_AllyRemembrance_Sa21341 : BattleUnitBuf
    {
        private BattleUnitBuf_Remembrance_Sa21341 _buff;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            var mainShadow = BattleObjectManager.instance.GetAliveList().FirstOrDefault(x =>
                x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgo_Sa_21341>() ||
                x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgoPlayer_Sa21341>());
            if (mainShadow != null)
                _buff = mainShadow.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(1);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(-1);
        }
    }
}