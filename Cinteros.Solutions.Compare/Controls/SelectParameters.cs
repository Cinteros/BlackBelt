﻿namespace Cinteros.Solutions.Compare.Controls
{
    using Cinteros.Solutions.Compare.Utils;
    using McTools.Xrm.Connection;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class SelectParameters : UserControl
    {
        #region Public Constructors

        public SelectParameters()
        {
            InitializeComponent();

            this.ParentChanged += this.SelectEnvironments_ParentChanged;
        }

        #endregion Public Constructors

        #region Private Methods

        private void cbToggleOrganizations_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (CheckBox)sender;

            this.lvOrganizations.ItemChecked -= lvOrganizations_ItemChecked;
            foreach (var item in this.lvOrganizations.Items.Cast<ListViewItem>().ToArray())
            {
                item.Checked = cb.Checked;
            }
            this.lvOrganizations.ItemChecked += lvOrganizations_ItemChecked;

            this.UpdateCompareSolutionsButton();
        }

        private void cbToggleSolutions_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (CheckBox)sender;

            this.lvSolutions.ItemChecked -= lvSolutions_ItemChecked;
            foreach (var item in this.lvSolutions.Items.Cast<ListViewItem>().ToArray())
            {
                item.Checked = cb.Checked;
            }
            this.lvSolutions.ItemChecked += lvSolutions_ItemChecked;

            this.UpdateCompareSolutionsButton();
        }

        private void GetOrganizations(ConnectionDetail[] connections)
        {
            this.lvOrganizations.Items.Clear();

            foreach (var connection in connections)
            {
                var row = new string[] {
                    connection.OrganizationFriendlyName,
                    connection.ServerName,
                };

                var item = new ListViewItem(row);
                item.Tag = connection;

                this.lvOrganizations.Items.Add(item);
            }
        }

        /// <summary>
        /// Event handler capturing changes in organization selections
        /// </summary>
        /// <param name="sender">Organizations list view</param>
        /// <param name="e">Event arguments</param>
        private void lvOrganizations_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.cbToggleOrganizations.CheckedChanged -= this.cbToggleOrganizations_CheckedChanged;
            this.UpdateSwitcher((ListView)sender, this.cbToggleOrganizations, e.Item.Checked);
            this.cbToggleOrganizations.CheckedChanged += this.cbToggleOrganizations_CheckedChanged;

            this.UpdateCompareSolutionsButton();
        }

        /// <summary>
        /// Event handler capturing changes in organization selections
        /// </summary>
        /// <param name="sender">Organizations list view</param>
        /// <param name="e">Event arguments</param>
        private void lvOrganizations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var list = (ListView)sender;

            foreach (var item in list.Items.Cast<ListViewItem>().Where(x => x.Selected == true))
            {
                item.Checked = !item.Checked;
            }

            this.UpdateCompareSolutionsButton();
        }

        /// <summary>
        /// Event handler capturing changes in solution selections
        /// </summary>
        /// <param name="sender">Solutions list view</param>
        /// <param name="e">Event arguments</param>
        private void lvSolutions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.cbToggleSolutions.CheckedChanged -= this.cbToggleSolutions_CheckedChanged;
            this.UpdateSwitcher((ListView)sender, this.cbToggleSolutions, e.Item.Checked);
            this.cbToggleSolutions.CheckedChanged += this.cbToggleSolutions_CheckedChanged;

            this.UpdateCompareSolutionsButton();
        }

        /// <summary>
        /// Event handler capturing changes in solution selections
        /// </summary>
        /// <param name="sender">Solutions list view</param>
        /// <param name="e">Event arguments</param>
        private void lvSolutions_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var list = (ListView)sender;

            foreach (var item in list.Items.Cast<ListViewItem>().Where(x => x.Selected == true))
            {
                item.Checked = !item.Checked;
            }

            this.UpdateCompareSolutionsButton();
        }

        /// <summary>
        /// Event handler capturing when parent control is changed (current conntrol is beeing added
        /// to the plugin's main form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEnvironments_ParentChanged(object sender, EventArgs e)
        {
            var parent = (MainScreen)this.Parent;

            if (parent != null)
            {
                string[] row;
                ListViewItem item;

                if (parent.ConnectionDetail != null)
                {
                    parent.WorkAsync(string.Format("Getting solutions information from '{0}'...", parent.ConnectionDetail.OrganizationFriendlyName),
                        (a) => // Work To Do Asynchronously
                        {
                            a.Result = parent.Service.RetrieveMultiple(Helpers.CreateSolutionsQuery()).Entities.Select(x => new Solution(x)).ToArray<Solution>();
                        },
                        (a) =>  // Cleanup when work has completed
                        {
                            this.lvSolutions.Items.Clear();
                            foreach (var solution in (Solution[])a.Result)
                            {
                                row = new string[] {
                                    solution.FriendlyName,
                                    solution.Version.ToString(),
                                };

                                item = new ListViewItem(row);
                                item.Tag = solution;

                                this.lvSolutions.Items.Add(item);
                            }
                        }
                    );

                    row = new string[] {
                        parent.ConnectionDetail.OrganizationFriendlyName,
                        parent.ConnectionDetail.ServerName,
                    };

                    this.lvReference.Items.Clear();
                    this.lvReference.Items.Add(new ListViewItem(row));

                    // All connections except currently connected one
                    this.GetOrganizations(new ConnectionManager().ConnectionsList.Connections.Where(x => x.ConnectionId != parent.ConnectionDetail.ConnectionId).ToArray<ConnectionDetail>());
                }
                else
                {
                    // All connections
                    this.GetOrganizations(new ConnectionManager().ConnectionsList.Connections.ToArray<ConnectionDetail>());
                }

                this.lvOrganizations_ItemSelectionChanged(this.lvOrganizations, null);
                this.lvSolutions_ItemSelectionChanged(this.lvSolutions, null);
            }
        }

        /// <summary>
        /// Updates button on tool depending on currently checked items
        /// </summary>
        private void UpdateCompareSolutionsButton()
        {
            ToolStripButton button = null;

            var menu = this.Parent.Controls.Find("tsMenu", true).Cast<ToolStrip>().FirstOrDefault();

            if (menu != null)
            {
                button = menu.Items.Find("tsbCompareSolutions", true).Cast<ToolStripButton>().FirstOrDefault();
            }

            if (button != null)
            {
                if (this.lvSolutions.CheckedItems.Count > 0 && this.lvOrganizations.CheckedItems.Count > 0)
                {
                    button.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Updates 'Select all' button, depending on currently checked items
        /// </summary>
        /// <param name="list"></param>
        /// <param name="switcher"></param>
        /// <param name="status"></param>
        private void UpdateSwitcher(ListView list, CheckBox switcher, bool status)
        {
            if (!status)
            {
                switcher.Checked = false;
            }
            else
            {
                if (list.CheckedItems.Count == list.Items.Count)
                {
                    switcher.Checked = true;
                }
            }
        }

        #endregion Private Methods
    }
}