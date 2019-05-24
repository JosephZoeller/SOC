﻿using SOC.Classes.Fox2;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.Hostage
{
    class HostageManager : LocationalQuestObjectManager
    {
        public HostageManager() : base(new LocationalDataStub("Prisoner Locations"))
        {
        }

        public override void AddFox2Entities(ref List<Fox2EntityClass> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
