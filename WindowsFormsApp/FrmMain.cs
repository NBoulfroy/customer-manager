﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibrary;
using DatabaseLibrary;
using DocumentLibrary;

namespace WindowsFormsApp
{
    public partial class FrmMain : Form
    {
        // Object contains data from Access database.
        Access access;
        // Customers display in lbxCustomers
        List<Customer> customers;
        // Customers display in lbxCustomersSelected
        List<Customer> customersSelected;

        public FrmMain()
        {
            InitializeComponent();
            access = new Access();
            customers = access.GetData().GetCustomers();
            customersSelected = new List<Customer>();
        }

        #region Load methods

        private void FrmMain_Load(object sender, EventArgs e)
        {
            access.LoadData();
            LbxCustomers_load();
        }

        /// <summary>
        /// Configures and insert data from Access database to lbxCustomers component.
        /// </summary>
        private void LbxCustomers_load()
        {
            // Define where is from data.
            lbxCustomers.DataSource = customers;
            // Value show in listbox.
            lbxCustomers.DisplayMember = "LastNameFirstName";

            lbxCustomers.ValueMember = "Id";
        }

        #endregion

        #region List minipulations methods

        /// <summary>
        /// Refresh ListBox component.
        /// </summary>
        /// <param name="listBox">ListBox component</param>
        /// <param name="list">List which contains customers</param>
        public void Refresh_listBox(ListBox listBox, List<Customer> list)
        {
            // Total clean-up for ListBox component.
            listBox.DataSource = null;

            // Defines DataSource for ListBox component.
            listBox.DataSource = list;

            // Displays data in ListBox component.
            listBox.DisplayMember = "LastNameFirstName";
            listBox.ValueMember = "Id";
        }

        /// <summary>
        /// Refreshes ListBox components.
        /// </summary>
        /// <param name="listBox">ListBox component</param>
        /// <param name="list">List which contains customers</param>
        /// <param name="customer">Customer object</param>
        /// <param name="otherListBox">ListBox component</param>
        /// <param name="otherList">List which contains customers</param>
        private void Refresh_listboxes(ListBox listBox, List<Customer> list, Customer customer, ListBox otherListBox, List<Customer> otherList)
        {
            // Total clean-up for ListBox components.
            listBox.DataSource = null;
            otherListBox.DataSource = null;

            // Extracts data from the list.
            list.Remove(customer);

            // Inserts data in an other list.
            otherList.Add(customer);

            // Defines DataSource for ListBox components.
            listBox.DataSource = list;
            otherListBox.DataSource = otherList;

            // Displays data in ListBox components.
            listBox.DisplayMember = "LastNameFirstName";
            listBox.ValueMember = "Id";
            otherListBox.DisplayMember = "LastNameFirstName";
            otherListBox.ValueMember = "Id";
        }

        #endregion

        #region pbxAddCustomer element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAddCustomer_mouseHover(object sender, EventArgs e)
        {
            pbxAddCustomer.Image = Properties.Resources.right_arrow_green_light;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAddCustomer_mouseLeave(object sender, EventArgs e)
        {
            pbxAddCustomer.Image = Properties.Resources.right_arrow_green_dark;
        }

        /// <summary>
        /// Substracts the selected customer from lbxCustomers component 
        /// to add in lbxSelectedCustomers component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAddCustomer_click(object sender, EventArgs e)
        {
            Customer customer = (Customer)lbxCustomers.SelectedItem;

            Refresh_listboxes(lbxCustomers, customers, customer, lbxCustomersSelected, customersSelected);
        }

        #endregion

        #region pbxRemoveCustomer element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemoveCustomer_mouseHover(object sender, EventArgs e)
        {
            pbxRemoveCustomer.Image = Properties.Resources.left_arrow_red_light;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemoveCustomer_mouseLeave(object sender, EventArgs e)
        {
            pbxRemoveCustomer.Image = Properties.Resources.left_arrow_red_dark;
        }

        /// <summary>
        /// Substracts the selected customer from lbxSelectedCustomers component 
        /// to add in lbxCustomers component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemoveCustomer_click(object sender, EventArgs e)
        {
            Customer customer = (Customer)lbxCustomersSelected.SelectedItem;

            Refresh_listboxes(lbxCustomersSelected, customersSelected, customer, lbxCustomers, customers);
        }

        #endregion

        #region pbxAdd element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAdd_mouseHover(object sender, EventArgs e)
        {
            pbxAdd.Image = Properties.Resources.add_light_32;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAdd_mouseLeave(object sender, EventArgs e)
        {
            pbxAdd.Image = Properties.Resources.add_dark_32;
        }

        /// <summary>
        /// Opens new FrmCustomers WinForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxAdd_click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer(this, access, customers);
            frmCustomer.ShowDialog();
        }

        #endregion

        #region pbxRemove element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemove_mouseHover(object sender, EventArgs e)
        {
            pbxRemove.Image = Properties.Resources.delete_light_32;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemove_mouseLeave(object sender, EventArgs e)
        {
            pbxRemove.Image = Properties.Resources.delete_dark_32;
        }

        /// <summary>
        /// Deletes selected customer selected in lbxCustomers component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxRemove_click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm the deletation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Gets the currently customer selected in listbox component.
                Customer customer = (Customer)lbxCustomers.SelectedItem;

                // Delete the customer in database and in the software memory.
                access.DeleteCustomer(customer.GetId());

                // Delete the customer in customers list.
                customers.Remove(customer);

                // Refresh the ListBox component.
                Refresh_listBox(lbxCustomers, customers);
            }
        }

        #endregion

        #region pbxSave element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxSave_mouseHover(object sender, EventArgs e)
        {
            pbxSave.Image = Properties.Resources.save_light_32;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxSave_mouseLeave(object sender, EventArgs e)
        {
            pbxSave.Image = Properties.Resources.save_dark_32;
        }

        /// <summary>
        /// Opens new saveFileDialog window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxSave_click(object sender, EventArgs e)
        {
            // Defines all choices.
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|OpenDocument Spreadsheet files (*.ods)|*.ods|Excel 1993-1998 files (*.xls)|*.xls"; // |Excel 2007 files (*.xlsx)|*.xlsx|All files (*.*)|*.*
            saveFileDialog.AddExtension = true;

            // Get choice in DialogResult object.
            DialogResult result = saveFileDialog.ShowDialog();

            // DialogResult treatment.
            if (result == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        CSV csv = new CSV("", saveFileDialog.FileName);
                        csv.DocumentBuilder(customersSelected);
                        break;
                    case 2:
                        Spreadsheet ods = new Spreadsheet("", saveFileDialog.FileName);
                        ods.DocumentBuilder(customersSelected);
                        break;
                    case 3:
                        Spreadsheet excel = new Spreadsheet("", saveFileDialog.FileName);
                        excel.DocumentBuilder(customersSelected);
                        break;
                }
            }
        }

        #endregion

        #region pbxPrint element

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxPrint_mouseHover(object sender, EventArgs e)
        {
            pbxPrint.Image = Properties.Resources.print_light_32;
        }

        /// <summary>
        /// Changes the image in Picturebox component when the mouse is not on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxPrint_mouseLeave(object sender, EventArgs e)
        {
            pbxPrint.Image = Properties.Resources.print_dark_32;
        }

        /// <summary>
        /// Opens new print dialog window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PbxPrint_click(object sender, EventArgs e)
        {
            // Displays new window.
            printDialog.ShowDialog();
        }

        #endregion

        #region Message method

        /// <summary>
        /// Displays message in MessageBox window component if no customers is loaded from Access database.
        /// </summary>
        private void DisplayNoCustomers()
        {
            if (customers.Count == 0)
            {
                MessageBox.Show("No customers in database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion
    }
}
