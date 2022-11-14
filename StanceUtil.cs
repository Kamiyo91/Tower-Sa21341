using System;
using System.Linq;
using BigDLL4221.Utils;
using Sound;
using VortexTower.Sae.Buffs;

namespace VortexTower
{
    public static class StanceUtil
    {
        public static void RemoveStanceBuffs(BattleUnitModel owner)
        {
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_AtkStance_Sa21341));
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_DefStance_Sa21341));
        }

        public static void RemoveCards(BattleUnitModel owner)
        {
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 1));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 2));
        }

        public static void AddCards(BattleUnitModel owner)
        {
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 1));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 2));
        }

        public static void AddGeneralCards(BattleUnitModel owner)
        {
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 10));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 11));
        }

        public static void RemoveGeneralCards(BattleUnitModel owner)
        {
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 10));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 11));
        }

        public static void RemoveGeneralStanceBuffs(BattleUnitModel owner)
        {
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_GeneralAtkStance_Sa21341));
            owner.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_GeneralDefStance_Sa21341));
        }

        public static void ChangeStance(BattleUnitModel owner, Type buf, string stanceType, KeywordBuf removeBuf,
            KeywordBuf addBuf, int deckIndex)
        {
            ChangeAnimation(owner);
            owner.bufListDetail.AddBufWithoutDuplication((BattleUnitBuf)Activator.CreateInstance(buf));
            if (stanceType.Equals("Def"))
            {
                owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.Guard);
                owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.Guard);
            }
            else
            {
                owner.view.charAppearance.RemoveAltMotion(ActionDetail.Standing);
                owner.view.charAppearance.RemoveAltMotion(ActionDetail.Default);
            }

            if (addBuf != KeywordBuf.None && removeBuf != KeywordBuf.None &&
                owner.bufListDetail.GetActivatedBufList().Exists(x => x.bufType == removeBuf) &&
                UnitUtil.SupportCharCheck(owner) == 1)
            {
                DecreaseStacksBufType(owner, removeBuf, 3);
                owner.bufListDetail.AddKeywordBufThisRoundByEtc(addBuf, 3, owner);
            }

            if (owner.faction != Faction.Player) return;
            owner.view.speedDiceSetterUI.DeselectAll();
            if (VortexModParameters.SaeKeypageIds.Contains(owner.Book.BookId)) return;
            var count = owner.allyCardDetail.GetHand().Count;
            var deckForBattle = owner.UnitData.unitData.GetDeckForBattle(deckIndex);
            owner.ChangeBaseDeck(deckForBattle, count);
        }

        private static void ChangeAnimation(BattleUnitModel owner)
        {
            owner.view.StartEgoSkinChangeEffect("Character");
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Purple_Change");
        }

        private static void DecreaseStacksBufType(BattleUnitModel owner, KeywordBuf bufType, int stacks)
        {
            var buf = owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x.bufType == bufType);
            if (buf != null) buf.stack -= stacks;
            if (buf != null && buf.stack < 1) owner.bufListDetail.RemoveBuf(buf);
        }
    }
}