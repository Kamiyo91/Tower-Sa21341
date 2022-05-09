﻿using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension.Zero;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Passives
{
    public class PassiveAbility_Zero_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_BlueFlame_Sa21341 _buff;
        private MechUtil_Zero _util;

        public override void OnWaveStart()
        {
            _buff = new BattleUnitBuf_BlueFlame_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            _util = new MechUtil_Zero(new MechUtilBaseModel
            {
                Owner = owner,
                Survive = true,
                Hp = 0,
                SetHp = 10,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_BlueFlameEgo_Sa21341),
                EgoCardId = new LorId(VortexModParameters.PackageId, 28),
                HasEgoAbDialog = true,
                EgoAbColorColor = AbColorType.Positive,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Zero",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ZeroEgoActive1_Sa21341"))
                            .Value.Desc
                    }
                }
            }, _buff);
            UnitUtil.CheckSkinProjection(owner);
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(VortexModParameters.PackageId, 20),
                    typeof(BattleUnitBuf_BlueFlameEgo_Sa21341))) _util.ForcedEgo();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            if (buf.bufType != KeywordBuf.Burn) return true;
            if (_buff.stack < 25) _buff.stack++;
            return false;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (_buff.stack < 25) _buff.stack++;
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }

        public override void OnRoundEnd()
        {
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(VortexModParameters.PackageId, 20),
                    typeof(BattleUnitBuf_BlueFlameEgo_Sa21341))) _util.ForcedEgo();
        }
    }
}