using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections.Generic;
using UnityEngine;

namespace MK_Falldown
{
    public class StopFalldownCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "stopfalldown";

        public string Help => "Поднять вырубленого игрока";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "unson" };

        public List<string> Permissions => new List<string> { "stopfalldown" };
        public UnturnedPlayer uplayer;

        public void Execute(IRocketPlayer caller, string[] command)
        {
            
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("stopfalldown_invalid_syntax"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            UnturnedPlayer utarget;

            try
            {
                utarget = UnturnedPlayer.FromName(command[0]);
            }
            catch
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("stopfalldown_player_not_found"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            if (utarget.Player.TryGetComponent(out PlayerComponent target))
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("stopfalldown_error"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            if (!target.isFalledDown)
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("stopfalldown_not_falled_down"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

           
            target.StopFallDown();
            UnturnedChat.Say(caller, Plugin.Instance.Translate("stopfalldown_completed"), Color.green);

        }
    }
}


