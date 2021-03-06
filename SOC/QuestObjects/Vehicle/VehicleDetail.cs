﻿using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace SOC.QuestObjects.Vehicle
{
    public class VehicleDetail : Detail
    {
        public VehicleDetail() { }

        public VehicleDetail(List<Vehicle> vehicleList, VehicleMetadata vehicleMeta)
        {
            vehicles = vehicleList; vehicleMetadata = vehicleMeta;
        }

        [XmlElement]
        public VehicleMetadata vehicleMetadata { get; set; } = new VehicleMetadata();

        [XmlArray]
        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        
        public override DetailManager GetNewManager()
        {
            return new VehicleManager(this);
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return vehicles.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            vehicles = qObjects.Cast<Vehicle>().ToList();
        }

        public override Metadata GetMetadata()
        {
            return vehicleMetadata;
        }
    }

    public class Vehicle : QuestObject
    {

        public Vehicle() { }

        public Vehicle(Position pos, int id)
        {
            position = pos; ID = id;
        }

        public Vehicle(VehicleBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            vehicle = box.comboBox_vehicle.Text;
            vehicleClass = box.comboBox_class.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        public override string GetObjectName()
        {
            return "Vehicle_" + ID;
        }

        public override Position GetPosition()
        {
            return position;
        }

        public override void SetPosition(Position pos)
        {
            position = pos;
        }

        public override int GetID()
        {
            return ID;
        }

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string vehicle { get; set; } = "TT77 NOSOROG";

        [XmlElement]
        public string vehicleClass { get; set; } = "DEFAULT";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());
    }

    public class VehicleMetadata : Metadata
    {

        public VehicleMetadata() { }

        public VehicleMetadata(VehicleControl control)
        {
            ObjectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string ObjectiveType { get; set; } = "ELIMINATE";
    }
}
