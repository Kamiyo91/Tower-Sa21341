namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_LightingBlade_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 8;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkLand = 0;
        }

        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _atkLand += damage;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}