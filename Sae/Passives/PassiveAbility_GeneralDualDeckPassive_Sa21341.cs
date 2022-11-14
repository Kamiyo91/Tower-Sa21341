using VortexTower.Sae.Cards;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_GeneralDualDeckPassive_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            DiceCardSelfAbility_GeneralAtkStance_Sa21341.Activate(owner);
        }

        public override void OnRoundStart()
        {
            StanceUtil.AddGeneralCards(owner);
        }
    }
}