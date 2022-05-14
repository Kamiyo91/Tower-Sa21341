namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Cards
{
    public class DiceCardSelfAbility_Rush_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            var speedDiceResultValue = card.speedDiceResultValue;
            var target = card.target;
            var targetSlotOrder = card.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            var targetDiceBroken = target.speedDiceResult[targetSlotOrder].breaked;
            if (speedDiceResultValue - speedDice.value <= Check && !targetDiceBroken) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
        }
    }
}