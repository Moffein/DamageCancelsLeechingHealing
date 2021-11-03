using BepInEx;
using UnityEngine.Networking;

namespace DamageCancelsLeechingHealing
{
    [BepInDependency("com.swuff.LostInTransit")]
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Moffein.DamageCancelsLeechingHealing", "Damage Cancels Leeching Healing", "1.0.0")]
    public class DamageCancelsLeechingHealing : BaseUnityPlugin
    {
        public void Awake()
        {
            On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
            {
                orig(self, damageInfo);
                if (NetworkServer.active && !damageInfo.rejected)
                {
                    if (self.body && self.body.HasBuff(LostInTransit.Buffs.LeechingRegen.buff))
                    {
                        self.body.ClearTimedBuffs(LostInTransit.Buffs.LeechingRegen.buff);
                    }
                }
            };
        }
    }
}
