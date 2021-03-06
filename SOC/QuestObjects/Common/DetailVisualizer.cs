﻿using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms;
using SOC.Forms.Pages;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using SOC.Classes.Common;

namespace SOC.QuestObjects.Common
{
    public abstract class DetailVisualizer
    {
        public UserControl detailControl { get; }

        public FlowLayoutPanel flowPanel { get; }

        public DetailVisualizer(UserControl control, FlowLayoutPanel panel)
        {
            detailControl = control; flowPanel = panel;
        }

        public void VisualizeDetail(Detail detail)
        {
            DrawMetadata(detail.GetMetadata());
            DrawBoxes(detail.GetQuestObjects());
        }

        public void ShowDetail()
        {
            detailControl.Visible = true;
        }

        public void HideDetail()
        {
            detailControl.Visible = false;
        }

        public abstract void DrawMetadata(Metadata meta);

        public void DrawBoxes(IEnumerable<QuestObject> questObjects)
        {
            flowPanel.Controls.Clear();
            foreach (QuestObject qObject in questObjects)
            {
                flowPanel.Controls.Add(NewBox(qObject));
            }
        }

        public Detail GetDetailFromControl()
        {
            return NewDetail(GetMetadataFromControl(), GetQuestObjectsFromControl());
        }

        public IEnumerable<QuestObject> GetQuestObjectsFromControl()
        {
            return flowPanel.Controls.OfType<QuestBox>().Select(box => box.getQuestObject());
        }

        public abstract Metadata GetMetadataFromControl();
        public abstract void SetDetailsFromSetup(Detail detail, CoreDetails core);
        public abstract QuestBox NewBox(QuestObject qObject);
        public abstract Detail NewDetail(Metadata meta, IEnumerable<QuestObject> qObjects);
    }

    public abstract class LocationalVisualizer : DetailVisualizer
    {
        public LocationalDataStub detailStub { get; }

        public LocationalVisualizer(LocationalDataStub stub, UserControl control, FlowLayoutPanel panel) : base(control, panel)
        {
            detailStub = stub;
        }

        public void DrawStubText(Detail detail)
        {
            List<Position> posList = new List<Position>();

            foreach (QuestObject qObject in detail.GetQuestObjects())
            {
                posList.Add(qObject.GetPosition());
            }
            detailStub.SetStubText(new IHLogPositions(posList));
        }

        public override void SetDetailsFromSetup(Detail detail, CoreDetails core)
        {
            List<Position> stubPositions = detailStub.GetStubLocations().GetPositions();
            List<QuestObject> qObjects = detail.GetQuestObjects().ToList();
            int positionCount = stubPositions.Count;
            int objectCount = qObjects.Count;

            for (int i = 0; i < positionCount; i++)
            {
                if (i >= objectCount) // add
                {
                    qObjects.Add(NewObject(stubPositions[i], i));
                }
                else // modify
                {
                    qObjects[i].SetPosition(stubPositions[i]);
                }
            }

            for (int i = objectCount - 1; i >= positionCount; i--) //remove
            {
                qObjects.RemoveAt(i);
            }

            detail.SetQuestObjects(qObjects);
        }

        public abstract QuestObject NewObject(Position pos, int index);

    }

    public abstract class NonLocationalVisualizer : DetailVisualizer
    {
        public NonLocationalVisualizer(UserControl control, FlowLayoutPanel panel) : base(control, panel) { }
    }
}
