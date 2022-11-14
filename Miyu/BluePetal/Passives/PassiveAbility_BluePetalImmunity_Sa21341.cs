namespace VortexTower.Miyu.BluePetal.Passives
{
    public class PassiveAbility_BluePetalImmunity_Sa21341 : PassiveAbilityBase
    {
        public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
        {
            if (keyword == KeywordBuf.Burn || keyword == KeywordBuf.Bleeding) return 0;
            return base.DmgFactor(dmg, type, keyword);
        }
    }
}