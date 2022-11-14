using System.Linq;
using VortexTower.Miyu.BluePetal.Dice;

namespace VortexTower.Miyu.Cards
{
    public class DiceCardSelfAbility_MiyuLaser_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (owner.faction == Faction.Enemy)
                card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_BluePetal_Sa21341());
            owner.allyCardDetail.DrawCards(1);
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var allyUnits = BattleObjectManager.instance.GetAliveList(owner.faction);
            if (allyUnits.Any()) RandomUtil.SelectOne(allyUnits).breakDetail.RecoverBreak(3);
        }
    }
}