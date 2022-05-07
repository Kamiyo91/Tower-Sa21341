using VortexLabyrinth_Sa21341.Miyu.Passives;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_BlueShield_Sa21341 : BattleUnitBuf
    {
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
            owner.passiveDetail.AddPassive(new PassiveAbility_BlueShield_Sa21341()).Hide();
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}