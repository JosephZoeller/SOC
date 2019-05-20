﻿using SOC.Classes.Common;
using SOC.Classes.Fox2;
using SOC.QuestComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Hostage
{
    static class HostageFox2 // : (abstract) objectFox2?
    {
        static void AddQuestEntities(HostageDetails hDetails, ref List<Fox2EntityClass> entityList)
        {
            List<Hostage> hostages = hDetails.Hostages;
            HostageMetadata hMetadata = hDetails.hostageMetadata;
            BodyInfoEntry hostageBodies = NPCBodyInfo.GetBodyInfo(hMetadata.hostageBodyName);

            DataSet dataSet = GetQuestDataSet(entityList);

            if (hostages.Count > 0)
            {
                GameObject gameObjectTppHostageUnique = new GameObject("GameObjectTppHostageUnique", dataSet, "TppHostageUnique2", hostages.Count, hostages.Count);
                TppHostage2Parameter hostageParameter = new TppHostage2Parameter(gameObjectTppHostageUnique, hostageBodies.partsPath);

                gameObjectTppHostageUnique.SetParameter(hostageParameter);

                entityList.Add(gameObjectTppHostageUnique);
                entityList.Add(hostageParameter);

                foreach (Hostage hostage in hostages)
                {
                    GameObjectLocator hostageLocator = new GameObjectLocator(hostage.GetHostageName(), dataSet, "TppHostageUnique2");
                    Transform hostageTransform = new Transform(hostageLocator, hostage.rotation, hostage.coordinates);
                    TppHostage2LocatorParameter hostageLocatorParameter = new TppHostage2LocatorParameter(hostageLocator);

                    hostageLocator.SetTransform(hostageTransform);
                    hostageLocator.SetParameter(hostageLocatorParameter);

                    entityList.Add(hostageLocator);
                    entityList.Add(hostageTransform);
                    entityList.Add(hostageLocatorParameter);
                }
            }
        }
    }
}