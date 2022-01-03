using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;


namespace MK_Falldown
{
    public class PlayerComponent : UnturnedPlayerComponent
    {
        public int bleedings = 0;
        public float blood = 100;
        public float sleep = 100;
        public Coroutine bleeding_coroutine = null;

        public System.Collections.IEnumerator sleep_coroutine;
        public Coroutine falldownCoroutine;

        public float OldHealth;
        public float OldEat;
        public float OldWater;
        public float OldVirus;
        public float OldEnergy;
        public float OldBlood;
        public float OldSleep;

        public bool isSleeping;
        public bool isFalledDown;

        public double bleedingStopModifier = 1;

        private System.Collections.IEnumerator FallDownWaitEffectCoroutine(int seconds)
        {
            yield return new WaitForSeconds(Plugin.Instance.Configuration.Instance.FallDownStartEffectTime);
            try
            {
                EffectManager.sendUIEffect(Plugin.Instance.Configuration.Instance.FallDownEffectID, 20, Player.Player.channel.owner.transportConnection, true);
            }
            catch { }
            yield return new WaitForSeconds(seconds);
            StopFallDown();
            yield break;
        }

        public void FallDown(int seconds)
        {
            isFalledDown = true;
            EffectManager.sendUIEffect(Plugin.Instance.Configuration.Instance.FallDownStartEffectID, 20, Player.Player.channel.owner.transportConnection, true);
            falldownCoroutine = StartCoroutine(FallDownWaitEffectCoroutine(seconds));
            Player.Player.stance.checkStance(EPlayerStance.PRONE, true);
            Player.Player.equipment.dequip();
            Player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);

        }

        public void StopFallDown()
        {
            if (!isFalledDown)
            {
                return;
            }
            try
            {
                EffectManager.sendUIEffect(Plugin.Instance.Configuration.Instance.FallDownEndEffectID, 20, Player.Player.channel.owner.transportConnection, true);
            }
            catch { }
            isFalledDown = false;
            StopCoroutine(falldownCoroutine);
            Player.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
            EffectManager.askEffectClearByID(Plugin.Instance.Configuration.Instance.FallDownStartEffectID, Player.Player.channel.owner.transportConnection);
            EffectManager.askEffectClearByID(Plugin.Instance.Configuration.Instance.FallDownEffectID, Player.Player.channel.owner.transportConnection);
        }

    }

}

