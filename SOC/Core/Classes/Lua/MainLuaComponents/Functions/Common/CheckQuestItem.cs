﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class CheckQuestItem : CheckQuestMethodsPair
    {
        static readonly LuaFunction IsTargetSetMessageIdForItem = new LuaFunction("IsTargetSetMessageIdForItem",
    @"
function this.IsTargetSetMessageIdForItem(gameId, messageId, checkAnimalId)
  if messageId == ""PickUpDormant"" then
    for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == false then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  elseif messageId == ""PickUpActive"" or messageId == ""Activate"" then
    for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == true then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  end
  return false, false
end");

        static readonly LuaFunction TallyItemTargets = new LuaFunction("TallyItemTargets",
            @"
function this.TallyItemTargets(totalTargets, objectiveCompleteCount, objectiveFailedCount)
  for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
    local dynamicQuestType = RECOVERED
    for _, ObjectiveTypeInfo in ipairs(ObjectiveTypeList.itemTargets) do
      if ObjectiveTypeInfo.Check(targetInfo) then
        dynamicQuestType = ObjectiveTypeInfo.Type
        break
      end
    end
    local targetMessageId = targetInfo.messageId

    if targetMessageId ~= ""None"" then
        if dynamicQuestType == RECOVERED then
          if (targetMessageId == ""PickUpDormant"" or targetMessageId == ""PickUpActive"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""Activate"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ELIMINATE then
          if (targetMessageId == ""PickUpActive"" or targetMessageId == ""Activate"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == KILLREQUIRED then
          if (targetMessageId == ""Activate"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""PickUpActive"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end
      end
  	end
    totalTargets = totalTargets + 1
  end
  return totalTargets, objectiveCompleteCount, objectiveFailedCount
end");
        
        public CheckQuestItem(MainLua mainLua, LuaFunction checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForItem, TallyItemTargets, "itemTargets", checkFunction, objectiveType) { }
    }
}
