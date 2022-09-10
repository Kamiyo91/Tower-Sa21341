using System.Linq;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_SummonMiyu_Sa21341 : PassiveAbilityBase
    {
        public BattleUnitModel Unit;
        public override void OnWaveStart()
        {
            if (BattleObjectManager.instance.GetList(owner.faction).Any(x => x.passiveDetail.HasPassive<PassiveAbility_Miyu_Sa21341>())) return;
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId,70));
        }
        public override void OnBattleEnd()
        {
            if (Unit != null) Unit.Book.owner = null;
        }
    }
}
