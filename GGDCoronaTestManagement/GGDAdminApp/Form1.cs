using GGDTestCenterAdmin.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGDTestCenterAdmin
{
    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class Form1 : Form, IAdminServiceCallback
    {
        IAdminService proxy;
        InstanceContext context;
        public Form1()
        {
            context = new InstanceContext(this);
            proxy = new AdminServiceClient(context);
            proxy.Connect();
            InitializeComponent();
        }

        public void OnIncreaseCapacity()
        {
            List<Ggd> testLocations = proxy.GetGgdInfo();

            lbTestCapacity.DataSource = testLocations.Select(t => t.AvailableCapacity).ToList();
        }

        public void OnUpdateLocations()
        {
            List<Ggd> testLocations = proxy.GetGgdInfo();

            lbLocations.DataSource = testLocations.Select(t => t.GgdCity).ToList();
            lbAvailableService.DataSource = testLocations.Select(t => t.ServicesProvided).ToList();
            lbTestCapacity.DataSource = testLocations.Select(t => t.AvailableCapacity).ToList();
            lbAddress.DataSource = testLocations.Select(t => t.TestCenterAddress).ToList();
        }

        private void btnGetTestCenters_Click(object sender, EventArgs e)
        {
            List<Ggd> testLocations = proxy.GetGgdInfo();

            lbLocations.DataSource = testLocations.Select(t => t.GgdCity).ToList();
            lbAvailableService.DataSource = testLocations.Select(t => t.ServicesProvided).ToList();
            lbTestCapacity.DataSource = testLocations.Select(t => t.AvailableCapacity).ToList();
            lbAddress.DataSource = testLocations.Select(t => t.TestCenterAddress).ToList();

        }

        private void btnIncreaseCampacity_Click(object sender, EventArgs e)
        {
            proxy.IncreaseCapacity(lbLocations.SelectedItem.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            proxy.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            proxy.AddAdditionalCoronaTestLocation(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text), textBox4.Text);
        }
    }
}
