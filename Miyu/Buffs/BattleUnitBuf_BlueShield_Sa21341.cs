﻿using System.Linq;
using BigDLL4221.Models;
using LOR_DiceSystem;
using Sound;

namespace VortexTower.Miyu.Buffs
{
    public class BattleUnitBuf_BlueShield_Sa21341 : BattleUnitBuf
    {
        private bool _protectBp;

        public BattleUnitBuf_BlueShield_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "BlueShield_Sa21341";
        protected override string keywordIconId => "BlueShield_Sa21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _protectBp = false;
        }

        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            if (!ModParameters.CardOptions.TryGetValue(behavior.card.card.GetID().packageId, out var cardOptions))
                return base.GetDamageReduction(behavior);
            var card = cardOptions.FirstOrDefault(
                x => x.CardId == behavior.card.card.GetID().id && x.OnlyAllyTargetCard);
            if (card != null) return base.GetDamageReduction(behavior);
            _protectBp = true;
            if (_owner.battleCardResultLog == null) return 9999;
            SingletonBehavior<DiceEffectManager>.Instance.CreateBehaviourEffect("BlueShield_Sa21341", 1f,
                _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
            return 9999;
        }

        public override int GetBreakDamageReduction(BehaviourDetail behaviourDetail)
        {
            if (!_protectBp) return base.GetBreakDamageReduction(behaviourDetail);
            _protectBp = false;
            _owner.bufListDetail.RemoveBuf(this);
            return 9999;
        }

        public override void OnRoundEnd()
        {
            _owner.bufListDetail.RemoveBuf(this);
        }
    }
}