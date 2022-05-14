using System.Linq;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow
{
    public class PassiveAbility_HayateShadow_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_ShadowEntertainMe_Sa21341 _buff;
        private PassiveAbility_ForgottenEgo_Sa_21341 _passive;
        private bool _singleUse;

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StartPoint_Sa21341());
            _passive = BattleObjectManager.instance.GetAliveList()
                    .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgo_Sa_21341>())?
                    .passiveDetail.PassiveList
                    .FirstOrDefault(x => x is PassiveAbility_ForgottenEgo_Sa_21341) as
                PassiveAbility_ForgottenEgo_Sa_21341;
            if (owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_ShadowEntertainMe_Sa21341) is
                BattleUnitBuf_ShadowEntertainMe_Sa21341 buff)
            {
                _buff = buff;
                if (_passive?.GetPhase() == 3) _buff.AddStacks(50);
            }
            else
            {
                _buff = new BattleUnitBuf_ShadowEntertainMe_Sa21341();
                if (_passive?.GetPhase() == 3) _buff.AddStacks(50);
                owner.bufListDetail.AddBuf(_buff);
            }
        }

        public override int SpeedDiceNumAdder()
        {
            return 4;
        }

        public override void OnRoundStart()
        {
            if (_passive.GetPhase() < 4) return;
            if (_passive.GetCount() == 2) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(2);
        }

        public override void AfterTakeDamage(BattleUnitModel attacker, int dmg)
        {
            if (attacker != null) _buff.AddStacks(-2);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (_singleUse || _buff.stack < 40) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            _singleUse = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(new LorId(VortexModParameters.PackageId, 32)));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() != new LorId(VortexModParameters.PackageId, 32)) return;
            _buff.stack = 0;
            owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
        }

        public override void OnRoundEnd()
        {
            var cards = owner.allyCardDetail.GetAllDeck()
                .Where(x => x.GetID() == new LorId(VortexModParameters.PackageId, 32));
            foreach (var card in cards) owner.allyCardDetail.ExhaustACardAnywhere(card);
            _singleUse = false;
        }
    }
}