using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Sae.Passives
{
    public class PassiveAbility_Desc_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId, 10000901));
        }
    }
}