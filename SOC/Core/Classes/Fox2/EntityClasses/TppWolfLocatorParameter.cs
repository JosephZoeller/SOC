﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Fox2
{
    class TppWolfLocatorParameter : Fox2EntityClass
    {
        private Fox2EntityClass owner;
        private string animalCount;

        public TppWolfLocatorParameter(Fox2EntityClass _owner, string count)
        {
            owner = _owner; animalCount = count;
        }

        public override string GetFox2Format()
        {
            return string.Format($@"
        <entity class=""TppWolfLocatorParameter"" classVersion=""0"" addr=""{GetHexAddress()}"" unknown1=""40"" unknown2=""4763161"">
          <staticProperties>
            <property name=""owner"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
              <value>{owner.GetHexAddress()}</value>
            </property>
            <property name=""count"" type=""uint32"" container=""StaticArray"" arraySize=""1"">
              <value>{animalCount}</value>
            </property>
            <property name=""radius"" type=""uint8"" container=""StaticArray"" arraySize=""1"">
              <value>40</value>
            </property>
          </staticProperties>
          <dynamicProperties />
        </entity>
                                ");
        }

        public override string GetName()
        {
            return "";
        }

        public override Fox2EntityClass GetOwner()
        {
            return owner;
        }
    }
}
