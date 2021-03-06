﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class QStep_Start : LuaMainComponent
    {
        List<string> OnEnterList = new List<string>();

        public void AddToOnEnter(string code)
        {
            OnEnterList.Add(code);
        }

        public override string GetComponent()
        {
            return$@"
quest_step.QStep_Start = {{
  OnEnter = function(){GetEnterListFormatted()}
    TppQuest.SetNextQuestStep(""QStep_Main"")
  end,
}}";
        }

        private string GetEnterListFormatted()
        {
            StringBuilder EnterListBuilder = new StringBuilder();
            foreach (string code in OnEnterList)
            {
                EnterListBuilder.Append($@"
    {code}");
            }
            return EnterListBuilder.ToString();
        }
    }
}
