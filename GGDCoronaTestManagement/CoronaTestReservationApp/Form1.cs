using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserApp.ServiceReference1;

namespace UserApp
{
    public partial class Form1 : Form
    {
        IReservationService proxy;
        public Form1()
        {
            proxy = new ReservationServiceClient();
            InitializeComponent();
        }


        private void btGetServiceName_Click(object sender, EventArgs e)
        {
            lbServiceName.Text = proxy.GetServiceName();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Ggd> availableTestLocations = proxy.GetAvailableTestLocations();

            lbLocations.DataSource = availableTestLocations.Select(l => l.GgdCity).ToList();
            lbAvailableCapacity.DataSource = availableTestLocations.Select(l => l.AvailableCapacity).ToList();
            lbAddress.DataSource = availableTestLocations.Select(l => l.TestCenterAddress).ToList();
        }

        private void btnMakeReservation_Click(object sender, EventArgs e)
        {
            if (proxy.MakeReservation(lbLocations.SelectedItem.ToString()))
            {
                MessageBox.Show("Your appointment is successfully created!");
            }
            else
            {
                MessageBox.Show("Failed to create an appointment");
            }
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            if (proxy.CancelReservation(lbLocations.SelectedItem.ToString()))
            {
                MessageBox.Show("Successfully cancelled reservation");
            }
            else
            {
                MessageBox.Show("Failed to cancel the reservation");
            }
        }
    }
}
