using System.Collections.Generic;
using System.Linq;
using KamiyoStaticUtil.Utils;
using Sound;
using UnityEngine;
using VortexLabyrinth_Sa21341.Miyu.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Dices
{
    public class DiceCardAbility_FingersnapSound_Sa21341 : DiceCardAbilityBase
    {
        public override void OnAfterAreaAtk(List<BattleUnitModel> damagedList, List<BattleUnitModel> defensedList)
        {
            var audioClip = Resources.Load<AudioClip>("StoryResource/SoundEffects/Story/ch1_FingerSnap");
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip(audioClip);
            foreach (var unit in BattleObjectManager.instance
                         .GetAliveList(UnitUtil.ReturnOtherSideFaction(owner.faction))
                         .Where(x => !x.bufListDetail.HasBuf<BattleUnitBuf_BlueShield_Sa21341>())) unit.TakeDamage(999);
        }
    }
}