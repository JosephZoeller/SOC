﻿using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SOC.Forms.Pages;
using SOC.Forms;
using SOC.UI;

namespace SOC.QuestObjects.Hostage
{
    class HostageVisualizer : DetailVisualizer
    {
        public HostageVisualizer(LocationalDataStub hostageStub, HostageControl hostageControl) : base(hostageStub, hostageControl, hostageControl.panelHosDet)
        {
            hostageControl.comboBox_Body.SelectedIndexChanged += OnBodyIndexChanged;
        }

        public override void DrawMetadata(Metadata meta)
        {
            HostageControl hostageControl = (HostageControl)detailControl;
            hostageControl.SetMetadata((HostageMetadata)meta);
        }

        public override QuestBox NewBox(QuestObject qObject)
        {
            return new HostageBox((Hostage)qObject, (HostageMetadata)GetMetadataFromControl()); ;
        }

        public override Detail NewDetail(Metadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new HostageDetail(qObjects.Cast<Hostage>().ToList(), (HostageMetadata)meta);
        }

        public override Metadata GetMetadataFromControl()
        {
            return new HostageMetadata((HostageControl)detailControl);
        }

        public override QuestObject NewObject(Position objectPosition, int objectID)
        {
            return new Hostage(objectPosition, objectID);
        }

        private void OnBodyIndexChanged(object sender, EventArgs e)
        {
            RefreshHostageLanguage();
        }

        private void RefreshHostageLanguage()
        {
            HostageMetadata meta = (HostageMetadata)GetMetadataFromControl();
            foreach (HostageBox hBox in flowPanel.Controls.OfType<HostageBox>())
            {
                hBox.RefreshLanguage(meta.hostageBodyName);
            }
        }
    }
}