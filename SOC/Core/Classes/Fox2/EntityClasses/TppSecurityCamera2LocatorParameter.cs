﻿namespace SOC.Classes.Fox2
{
    class TppSecurityCamera2LocatorParameter : Fox2EntityClass
    {
        private Fox2EntityClass owner;

        public TppSecurityCamera2LocatorParameter(Fox2EntityClass _owner)
        {
            owner = _owner;
        }

        public override string GetFox2Format()
        {
            return string.Format($@"
        <entity class=""TppSecurityCamera2LocatorParameter"" classVersion=""0"" addr=""{GetHexAddress()}"" unknown1=""32"" unknown2=""107"">
          <staticProperties>
	        <property name=""owner"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
	          <value>{owner.GetHexAddress()}</value>
	        </property>
	        <property name=""identifier"" type=""String"" container=""StaticArray"" arraySize=""1"">
	          <value></value>
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
