using VortexTower.Miyu.BluePetal.Dice;

namespace VortexTower.Miyu.BluePetal.Cards
{
    public class DiceCardSelfAbility_PetalDraw_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_BluePetal_Sa21341());
            owner.allyCardDetail.DrawCards(1);
        }
    }
}