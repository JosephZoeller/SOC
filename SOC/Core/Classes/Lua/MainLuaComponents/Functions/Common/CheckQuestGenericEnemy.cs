﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class CheckQuestGenericEnemy : CheckQuestMethodsPair
    {

        static readonly LuaFunction IsTargetSetMessageIdForGenericEnemy = new LuaFunction("IsTargetSetMessageIdForGenericEnemy",
            @"
function this.IsTargetSetMessageIdForGenericEnemy(gameId, messageId, checkAnimalId)
  if mvars.ene_questTargetList[gameId] then
	local targetInfo = mvars.ene_questTargetList[gameId]
	local intended = true
	if targetInfo.messageId ~= ""None"" and targetInfo.isTarget == true then
	  intended = false
	elseif targetInfo.isTarget == false then
	  intended = false
	end
	targetInfo.messageId = messageId or ""None""
	return true, intended
  end
  return false, false
end");

        static readonly LuaFunction TallyGenericTargets = new LuaFunction("TallyGenericTargets",
            @"
function this.TallyGenericTargets(totalTargets, objectiveCompleteCount, objectiveFailedCount)
  for targetGameId, targetInfo in pairs(mvars.ene_questTargetList) do
    local dynamicQuestType = ELIMINATE
    local isTarget = targetInfo.isTarget or false
    local targetMessageId = targetInfo.messageId

    if isTarget == true then
      if ObjectiveTypeList.genericTargets ~= nil then
        for _, ObjectiveTypeInfo in ipairs(ObjectiveTypeList.genericTargets) do
          if ObjectiveTypeInfo.Check(targetGameId) then
            dynamicQuestType = ObjectiveTypeInfo.Type
            break
          end
        end
      end

      if targetMessageId ~= ""None"" then
        if dynamicQuestType == RECOVERED then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ELIMINATE then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") or (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == KILLREQUIRED then
          if (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"")  then
            objectiveFailedCount = objectiveFailedCount + 1
          end
        end
      end
      totalTargets = totalTargets + 1
    end
  end
  return totalTargets, objectiveCompleteCount, objectiveFailedCount
end");

        public CheckQuestGenericEnemy(MainLua mainLua, LuaFunction checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets, "genericTargets", checkFunction, objectiveType) { }

        public CheckQuestGenericEnemy(MainLua mainLua) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets) { }
    }
}
