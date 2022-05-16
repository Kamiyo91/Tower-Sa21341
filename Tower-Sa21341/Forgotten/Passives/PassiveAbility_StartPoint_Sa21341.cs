using System.Linq;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.Passives
{
    public class PassiveAbility_StartPoint_Sa21341 : PassiveAbilityBase
    {
        public override void OnRoundEndTheLast()
        {
            BattleObjectManager.instance.UnregisterUnit(owner);
            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 13,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 13))).Value,
                Pos = 0,
                EmotionLevel = 1,
                OnWaveStart = true
            }, VortexModParameters.PackageId);
            UnitUtil.RefreshCombatUI();
        }

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StartPoint_Sa21341());
        }
    }
}