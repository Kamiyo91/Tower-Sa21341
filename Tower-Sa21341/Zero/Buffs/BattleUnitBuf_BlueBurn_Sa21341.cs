using Sound;

namespace VortexLabyrinth_Sa21341.Zero.Buffs
{
    public class BattleUnitBuf_BlueBurn_Sa21341 : BattleUnitBuf
    {
        public override KeywordBuf bufType => KeywordBuf.Burn;
        public override BufPositiveType positiveType => BufPositiveType.Negative;
        protected override string keywordId => "Burn";
        protected override string keywordIconId => "BlueFlame_Sa21341";

        public override void OnAddBuf(int addedStack)
        {
            stack += addedStack;
            if (stack == 0) _owner.bufListDetail.RemoveBuf(this);
        }

        public override void OnRoundEnd()
        {
            if (!_owner.IsImmune(bufType))
            {
                _owner.TakeDamage(stack * 2, DamageType.Buf, null, bufType);
                PrintEffect();
                if (_owner.bufListDetail.GetActivatedBuf(KeywordBuf.BurnBreak) != null)
                    _owner.TakeBreakDamage(stack, DamageType.Buf, null, AtkResist.Normal, bufType);
                if (_owner.faction == Faction.Enemy && _owner.IsDead())
                    Singleton<StageController>.Instance.GetStageModel().AddBurnKillCount();
            }

            stack = stack * 2 / 3;
            if (stack <= 0)
                _owner.bufListDetail.RemoveBuf(this);
        }

        private void PrintEffect()
        {
            SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect("6/BigBadWolf_Emotion_Aura", 1f,
                _owner.view, _owner.view, 2);
            SoundEffectPlayer.PlaySound("Buf/Effect_Burn");
        }
    }
}