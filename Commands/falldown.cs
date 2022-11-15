using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections.Generic;
using UnityEngine;

namespace MK_Falldown
{
    public class FalldownCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "falldown";

        public string Help => "Вырубить игрока";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "son" };

        public List<string> Permissions => new List<string> { "falldown" };
        public UnturnedPlayer uplayer;

        public void Execute(IRocketPlayer caller, string[] command)
        {
            uplayer = (UnturnedPlayer)caller;
            
            if (command.Length != 2)
            {
                UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_invalid_syntax"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            UnturnedPlayer utarget;

            try
            {
                utarget = UnturnedPlayer.FromName(command[0]);
            }
            catch
            {
                UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_player_not_found"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }
            if (!utarget.Player.TryGetComponent(out PlayerComponent target))
            {
                UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_error"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            if (target.isFalledDown)
            {
                UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_falled_down"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            try
            {
                Convert.ToInt32(command[1]);
            }
            catch
            {
                UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_invalid_syntax"), Color.red);
                throw new WrongUsageOfCommandException(caller, this);
            }

            target.FallDown(Convert.ToInt32(command[1]));
            UnturnedChat.Say(uplayer, Plugin.Instance.Translate("falldown_completed"), Color.green);

        }
    }
}


