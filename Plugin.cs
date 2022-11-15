using Rocket.API.Collections;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK_Falldown
{
    internal class Plugin : RocketPlugin<Config>
    {
        public static Plugin Instance;
        public static Config cfg => Instance.Configuration.Instance;
        protected override void Load()
        {
            Instance = this;
            PlayerLife.onPlayerDied += PlayerLife_onPlayerDied;

            PlayerLife.OnTellHealth_Global += onTellHealthGlobal;
            base.Load();
        }

        private void onTellHealthGlobal(PlayerLife life)
        {
            if (life.health == 0) return;

            if (life.health <= Configuration.Instance.FalldownMinHealth)
            {
                if (!life.player.TryGetComponent(out PlayerComponent pc) || pc == null || pc.isFalledDown) return;
                pc.FallDown(Configuration.Instance.FalldownDuration);
            }
            else
            {
                if (!life.player.TryGetComponent(out PlayerComponent pc) || pc == null || !pc.isFalledDown) return;
                pc.StopFallDown();
            }
            

        }

        private void PlayerLife_onPlayerDied(PlayerLife sender, EDeathCause cause, ELimb limb, Steamworks.CSteamID instigator)
        {
            if (!sender.player.TryGetComponent(out PlayerComponent pc) || pc == null || !pc.isFalledDown) return;
            pc.StopFallDown();
        }

        protected override void Unload()
        {
            Instance = null;
            PlayerLife.onPlayerDied -= PlayerLife_onPlayerDied;
            PlayerLife.OnTellHealth_Global -= onTellHealthGlobal;
            base.Unload();
        }

        public override TranslationList DefaultTranslations => new TranslationList() 
        {
            { "falldown_invalid_syntax", "Неверные аргументы команды." },
            { "falldown_player_not_found", "Игрок не найден." },
            { "falldown_error", "Во время выполнения комады произошла ошибка. Попробуйте ещё раз." },
            { "falldown_falled_down", "Игрок уже без сознания." },
            { "falldown_completed", "Игрок потрял сознание." },
            { "stopfalldown_invalid_syntax", "Неверные аргументы команды." },
            { "stopfalldown_player_not_found", "Игрок не найден." },
            { "stopfalldown_error", "Во время выполнения комады произошла ошибка. Попробуйте ещё раз." },
            { "stopfalldown_not_falled_down", "Игрок не без сознания." },
            { "stopfalldown_completed", "Игрок пришел в сознание." }
        };
    }
}
