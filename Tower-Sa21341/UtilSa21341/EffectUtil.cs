using Sound;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.UtilSa21341
{
    public static class EffectUtil
    {
        public static void BurnEffect(BattleUnitModel owner)
        {
            var gameObject = Util.LoadPrefab("Battle/DiceAttackEffects/New/FX/DamageDebuff/FX_DamageDebuff_Fire");
            if (gameObject != null && owner?.view != null)
            {
                var pss = gameObject.GetComponentsInChildren<ParticleSystem>();
                var count = 0;
                foreach (var ps in pss)
                {
                    if(count != 2 && count != 0) ps.gameObject.SetActive(false); 
                    var main = ps.main;
                    main.startColor = new Color(0, 0, 0, 1);
                    count++;
                }
                gameObject.transform.parent = owner.view.camRotationFollower;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localScale = Vector3.one;
                gameObject.transform.localRotation = Quaternion.identity;
            }
            SoundEffectPlayer.PlaySound("Buf/Effect_Burn");
        }
    }
}
