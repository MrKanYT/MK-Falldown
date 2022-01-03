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
            PlayerLife.onPlayerDied += PlayerLife_onPlayerDied;
            base.Load();
        }

        private void PlayerLife_onPlayerDied(PlayerLife sender, EDeathCause cause, ELimb limb, Steamworks.CSteamID instigator)
        {
            if (!sender.player.TryGetComponent(out PlayerComponent target)) return;
            target.StopFallDown();
        }

        protected override void Unload()
        {
            PlayerLife.onPlayerDied -= PlayerLife_onPlayerDied;
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
