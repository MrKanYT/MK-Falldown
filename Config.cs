﻿using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK_Falldown
{
    public class Config : IRocketPluginConfiguration
    {
        public float FallDownStartEffectTime;
        public float FallDownTime;
        public float FallDownEndTime;

        public ushort FallDownStartEffectID;
        public ushort FallDownEffectID;
        public ushort FallDownEndEffectID;
        public void LoadDefaults()
        {
            FallDownStartEffectTime = 5;
            FallDownTime = 5;
            FallDownEndTime = 10;

            FallDownStartEffectID = 13162;
            FallDownEffectID = 13163;
            FallDownEndEffectID = 13164;
        }
    }
}
