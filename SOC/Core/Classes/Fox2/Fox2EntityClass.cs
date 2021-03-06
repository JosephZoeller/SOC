﻿namespace SOC.Classes.Fox2
{
    public abstract class Fox2EntityClass
    {
        public Fox2EntityClass() { }

        private uint address = 0;

        public void SetAddress(uint _address)
        {
            address = _address;
        }

        public string GetHexAddress()
        {
            return string.Format("0x{0:X8}", address);
        }

        public abstract Fox2EntityClass GetOwner();

        public abstract string GetFox2Format();

        public abstract string GetName();
    }
}
