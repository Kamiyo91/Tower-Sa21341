using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using LOR_DiceSystem;
using VortexTower.Miyu.Buffs;

namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_Miyu_Sa21341 : PassiveAbilityBase
    {
        private bool _used;

        public override void OnWaveStart()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
                unit.bufListDetail.AddBuf(new BattleUnitBuf_MiyuImmunity_Sa21341());
            _used = false;
            InitDialog();
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 24));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 26));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 33));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 62));
            UnitUtil.CheckSkinProjection(owner);
        }

        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            switch (behavior.card.card.XmlData.Spec.Ranged)
            {
                case CardRange.FarArea:
                    return 9999;
                case CardRange.FarAreaEach:
                    return 9999;
                default:
                    return base.GetDamageReduction(behavior);
            }
        }

        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            switch (behavior.card.card.XmlData.Spec.Ranged)
            {
                case CardRange.FarArea:
                    return 9999;
                case CardRange.FarAreaEach:
                    return 9999;
                default:
                    return base.GetBreakDamageReduction(behavior);
            }
        }

        public override void OnRoundStart()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            //if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 44))
            //{
            //    _used = true;
            //    ChangeToMiyuEgoMap();
            //}

            if (curCard.card.GetID() != new LorId(VortexModParameters.PackageId, 26)) return;
            owner.personalEgoDetail.RemoveCard(curCard.card.GetID());
            _used = true;
            ChangeToMiyuEgoMap();
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            if (!ModParameters.CardOptions.TryGetValue(card.GetID().packageId, out var cardOptions))
                return base.ChangeAttackTarget(card, idx);
            var cardItem = cardOptions.FirstOrDefault(x => card.GetID().id == x.CardId && x.OnlyAllyTargetCard);
            if (cardItem == null) base.ChangeAttackTarget(card, idx);
            var units = BattleObjectManager.instance.GetAliveList(owner.faction).Where(x => x.IsTargetable(owner))
                .ToList();
            return units.Any() ? RandomUtil.SelectOne(units) : base.ChangeAttackTarget(card, idx);
        }

        private static void ChangeToMiyuEgoMap()
        {
            MapUtil.ChangeMap(VortexModParameters.MiyuMap);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("BlueGuardian_Sa21341",
                new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 3), new LorId(VortexModParameters.PackageId, 4) });
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 3:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000004));
                    break;
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000005));
                    break;
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000008));
                    break;
            }
        }
    }
}