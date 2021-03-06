﻿using SOC.Classes.Fox2;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using SOC.Forms.Pages;
using SOC.Classes.Lua;
using SOC.Classes.Assets;
using SOC.Classes.Common;

namespace SOC.QuestObjects.WalkerGear
{
    class WalkerManager : LocationalManager
    {
        static LocationalDataStub walkerStub = new LocationalDataStub("Walker Gear Locations");

        static WalkerControl walkerControl = new WalkerControl();

        static WalkerVisualizer walkerVisualizer = new WalkerVisualizer(walkerStub, walkerControl);
        
        public WalkerManager(WalkerDetail detail) : base(detail, walkerVisualizer) { }

        public override void AddToFox2Entities(DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            WalkerFox2.AddQuestEntities((WalkerDetail)base.detail, dataSet, entityList);
        }

        public override void AddToMainLua(MainLua mainLua)
        {
            WalkerLua.GetMain((WalkerDetail)base.detail, mainLua);
        }

        public override void AddToDefinitionLua(DefinitionLua definitionLua)
        {
            WalkerLua.GetDefinition((WalkerDetail)base.detail, definitionLua);
        }
    }
}
