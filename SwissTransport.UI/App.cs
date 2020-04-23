using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwissTransport.UI
{
    public partial class App : Form
    {
        private readonly TransportService _transportService = new TransportService();

        public App()
        {
            InitializeComponent();
        }

        private void Cmb_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox cmb = (ComboBox) sender;
            Cursor.Current = Cursors.Default;
            var input = cmb.Text;

            if (input.Length <= 1)
            {
                return;
            }

            if (cmb.DroppedDown == false)
            {
                cmb.DroppedDown = true;
            }

            cmb.Items.Clear();
            cmb.Text = input;
            cmb.Select(input.Length, 0);

            try
            {
                var stationNames = _transportService.GetStationsName(cmb.Text);
                cmb.Items.AddRange(stationNames.ToArray());
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void BtnSearchConnections_Click(object sender, EventArgs e)
        {
            dgvConnections.Rows.Clear();

            var connections = _transportService.GetConnections(cmbFrom.Text, cmbTo.Text);

            foreach (var connection in connections)
            {
                dgvConnections.Rows.Add(Convert.ToDateTime(connection.From.Departure).ToString("HH:mm"),
                    Convert.ToDateTime(connection.To.Arrival).ToString("HH:mm"),
                    connection.Duration.Remove(0, 3).Remove(5, 3) + " h");
            }
        }

        private void BtnGetStationBoard_Click(object sender, EventArgs e)
        {
            dgvStationConnections.Rows.Clear();

            var stationBoards = _transportService.GetStationConnections(cmbStation.Text);

            foreach (var stationBoard in stationBoards)
            {
                dgvStationConnections.Rows.Add(stationBoard.Number,
                    stationBoard.To,
                    Convert.ToDateTime(stationBoard.Stop.Departure).ToString("HH:mm"));
            }
        }
    }
}