﻿using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class FormMain : Form
    {
        ManagerMaster managerMaster = new ManagerMaster();

        private Setup setupPage;
        private Details detailPage = new Details();
        private List<GroupBox> detailPageBoxes = new List<GroupBox>();
        private int panelNum = 0;


        CoreDetails coreDetails = new CoreDetails();
        List<QuestObjectDetails> questDetails = new List<QuestObjectDetails>();

        public FormMain()
        {
            setupPage = new Setup(managerMaster);

            InitializeComponent();
            GoToPanel();
            buttonBack.Visible = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            panelNum++;
            this.GoToPanel();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            panelNum--;
            this.GoToPanel();
        }
        private bool isFilled()
        {
            //return true; // FOR DEBUG
            if (string.IsNullOrEmpty(setupPage.textBoxFPKName.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestNum.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestTitle.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestDesc.Text))
                return false;
            if (setupPage.comboBoxCategory.SelectedIndex == -1 || setupPage.comboBoxReward.SelectedIndex == -1 || setupPage.comboBoxProgressNotifs.SelectedIndex == -1 || setupPage.comboBoxRegion.SelectedIndex == -1)
                return false;
            if (setupPage.comboBoxCP.Enabled)
                if (setupPage.comboBoxCP.SelectedIndex == -1 || setupPage.comboBoxLoadArea.SelectedIndex == -1 || setupPage.comboBoxRadius.SelectedIndex == -1 || string.IsNullOrEmpty(setupPage.textBoxXCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxYCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxZCoord.Text))
                    return false;

            return true;
        }
        private void GoToPanel()
        {
            switch (panelNum)
            {
                case 0:
                    ShowSetup();
                    break;

                case 1:
                    if (isFilled())
                    {
                        ShowWait();
                        Application.DoEvents();

                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please fill in the remaining Setup and Flavor Text fields.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panelNum--;
                        return;
                    }
                    break;

                case 2:
                    BuildQuest();
                    MessageBox.Show("Build Complete", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelNum--;
                    break;
            }
        }

        private void ShowSetup()
        {
            buttonBack.Visible = false;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(setupPage);
            //setupPage.refreshCoordinateBoxes(EntitiesManager.GetQuestEntities());
            buttonNext.Text = "Next >>";
        }

        private void ShowWait()
        {
            panelMain.Controls.Clear();
            Waiting waitingPage = new Waiting();
            panelMain.Controls.Add(waitingPage);
            buttonNext.Enabled = false;
            Application.DoEvents();
        }

        private void ShowDetails()
        {
            panelMain.Controls.Clear();
            buttonBack.Visible = true;
            buttonNext.Text = "Build";
            panelMain.Controls.Add(detailPage);
            buttonNext.Enabled = true;
        }

        private void BuildQuest()
        {
            /*
            definitionDetails = setupPage.getDefinitionDetails();
            questDetails = detailPage.GetEntityLists();

            AssetsBuilder.ClearQuestFolders(definitionDetails);

            LangBuilder.WriteQuestLangs(definitionDetails);

            LuaBuilder.WriteDefinitionLua(definitionDetails, questDetails);
            LuaBuilder.WriteMainQuestLua(definitionDetails, questDetails);

            Fox2Builder.WriteItemFox2(definitionDetails, questDetails);
            Fox2Builder.WriteQuestFox2(definitionDetails, questDetails);

            AssetsBuilder.BuildAssets(definitionDetails, questDetails);
            */
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            //setupPage.ShiftGroups(Height - 100, Width - 42);
            //detailPage.ShiftGroups(Height - 100, Width - 42);
            //Console.WriteLine(setupPage.Width + ", " + setupPage.Height);
            //Console.WriteLine(this.panelMain.Width);
        }

        public static List<Coordinates> BuildCoords(string rawString)
        {
            List<Coordinates> coordList = new List<Coordinates>();
            return coordList;
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            setupPage.refreshNotifsList();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            if (panelNum != 0)
            {
                panelNum = 0; GoToPanel();
            }

            Quest quest = new Quest();

            if (quest.Load(loadFile.FileName))
            {
                //setupPage.setDefinitionDetails(quest.definitionDetails);
                //EntitiesManager.setQuestEntities(quest.questEntities);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFilled())
            {
                DialogResult result = MessageBox.Show("Do you want to save this Sideop to an Xml file?", "SOC", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    Save();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Save()
        {
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Xml File|*.xml";
                //saveFile.FileName = setupPage.getDefinitionDetails().FpkName;
                DialogResult saveResult = saveFile.ShowDialog();
                if (saveResult != DialogResult.OK) return;
                if (panelNum != 0)
                {
                    panelNum = 0; GoToPanel();
                }
                //Quest quest = new Quest(setupPage.getDefinitionDetails(), EntitiesManager.GetQuestEntities());
                //quest.Save(saveFile.FileName);
            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch { }
        }
    }
}
