﻿using BigDLL4221.Utils;

namespace VortexTower
{
    public class BattleUnitBuf_Vip_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Vip_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "Vip_Sa21341";
        protected override string keywordIconId => "Vip_Sa21341";

        public override void OnDie()
        {
            UnitUtil.VipDeath(_owner);
        }
    }
}