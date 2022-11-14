namespace VortexTower.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_LostHostSoul_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            InitDialog();
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000011));
                    break;
            }
        }
    }
}