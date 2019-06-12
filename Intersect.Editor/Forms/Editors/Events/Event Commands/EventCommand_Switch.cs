﻿using System;
using System.Windows.Forms;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandSwitch : UserControl
    {
        private readonly FrmEvent mEventEditor;
        private bool mLoading;
        private SetSwitchCommand mMyCommand;

        public EventCommandSwitch(SetSwitchCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            mLoading = true;
            InitLocalization();
            if (mMyCommand.SwitchType == SwitchTypes.ServerSwitch)
            {
                rdoGlobalSwitch.Checked = true;
            }
            mLoading = false;
            InitEditor();
        }

        private void InitLocalization()
        {
            grpSetSwitch.Text = Strings.EventSetSwitch.title;
            lblSwitch.Text = Strings.EventSetSwitch.label;
            rdoGlobalSwitch.Text = Strings.EventSetSwitch.global;
            rdoPlayerSwitch.Text = Strings.EventSetSwitch.player;
            lblTo.Text = Strings.EventSetSwitch.to;
            cmbSetSwitchVal.Items.Clear();
            cmbSetSwitchVal.Items.Add(Strings.EventSetSwitch.False);
            cmbSetSwitchVal.Items.Add(Strings.EventSetSwitch.True);
            btnSave.Text = Strings.EventSetSwitch.okay;
            btnCancel.Text = Strings.EventSetSwitch.cancel;
            chkSyncParty.Text = Strings.EventSetSwitch.syncparty;
        }

        private void InitEditor()
        {
            cmbSetSwitch.Items.Clear();
            int switchCount = 0;
            if (rdoPlayerSwitch.Checked)
            {
                cmbSetSwitch.Items.AddRange(PlayerVariableBase.Names);
                cmbSetSwitch.SelectedIndex = PlayerVariableBase.ListIndex(mMyCommand.SwitchId);
            }
            else
            {
                cmbSetSwitch.Items.AddRange(ServerVariableBase.Names);
                cmbSetSwitch.SelectedIndex =ServerVariableBase.ListIndex(mMyCommand.SwitchId);
            }
            cmbSetSwitchVal.SelectedIndex = Convert.ToInt32(mMyCommand.Value);
            chkSyncParty.Checked = mMyCommand.SyncParty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rdoPlayerSwitch.Checked)
            {
                mMyCommand.SwitchType = SwitchTypes.PlayerSwitch;
                mMyCommand.SwitchId = PlayerVariableBase.IdFromList(cmbSetSwitch.SelectedIndex);
            }
            if (rdoGlobalSwitch.Checked)
            {
                mMyCommand.SwitchType = SwitchTypes.ServerSwitch;
                mMyCommand.SwitchId = ServerVariableBase.IdFromList(cmbSetSwitch.SelectedIndex);
            }
            mMyCommand.Value = Convert.ToBoolean(cmbSetSwitchVal.SelectedIndex);
            mMyCommand.SyncParty = chkSyncParty.Checked;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void rdoPlayerSwitch_CheckedChanged(object sender, EventArgs e)
        {
            InitEditor();
            if (!mLoading && cmbSetSwitch.Items.Count > 0) cmbSetSwitch.SelectedIndex = 0;
            if (!mLoading) cmbSetSwitchVal.SelectedIndex = 0;
        }

        private void rdoGlobalSwitch_CheckedChanged(object sender, EventArgs e)
        {
            InitEditor();
            if (!mLoading && cmbSetSwitch.Items.Count > 0) cmbSetSwitch.SelectedIndex = 0;
            if (!mLoading) cmbSetSwitchVal.SelectedIndex = 0;
        }
    }
}