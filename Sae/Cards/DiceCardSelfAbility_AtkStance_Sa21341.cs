using System.Linq;
using BigDLL4221.Utils;
using VortexTower.Sae.Buffs;
using VortexTower.Sae.Passives;

namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_AtkStance_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        public static void Activate(BattleUnitModel unit)
        {
            if (unit.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>()) return;
            if (unit.faction == Faction.Enemy || VortexModParameters.SaeKeypageIds.Contains(unit.Book.BookId))
                ChangeDeckEnemy(unit);
            StanceUtil.RemoveStanceBuffs(unit);
            StanceUtil.RemoveCards(unit);
            StanceUtil.ChangeStance(unit, typeof(BattleUnitBuf_AtkStance_Sa21341), "Atk", KeywordBuf.Endurance,
                KeywordBuf.Strength, 0);
            if (!unit.passiveDetail.HasPassive<PassiveAbility_Warrior_Sa21341>() ||
                UnitUtil.SupportCharCheck(unit) <= 1) return;
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 3, unit);
            DecreaseStacksBufType(unit, KeywordBuf.Endurance, 3);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>();
        }

        private static void DecreaseStacksBufType(BattleUnitModel owner, KeywordBuf bufType, int stacks)
        {
            var buf = owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x.bufType == bufType);
            if (buf != null) buf.stack -= stacks;
            if (buf != null && buf.stack < 1) owner.bufListDetail.RemoveBuf(buf);
        }

        private static void ChangeDeckEnemy(BattleUnitModel unit)
        {
            var cardInHand = unit.allyCardDetail.GetHand().Count;
            unit.allyCardDetail.ExhaustAllCards();
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_RedAura_Sa21341>() && unit.faction == Faction.Enemy)
                foreach (var cardId in VortexModParameters.DarkSaeAttackDeck)
                    unit.allyCardDetail.AddNewCardToDeck(cardId);
            else
                foreach (var cardId in VortexModParameters.SaeAttackDeck)
                    unit.allyCardDetail.AddNewCardToDeck(cardId);
            unit.allyCardDetail.DrawCards(cardInHand);
            if (unit.faction == Faction.Enemy) unit.allyCardDetail.DrawCards(5);
        }
    }
}