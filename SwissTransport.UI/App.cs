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
            if (!FormValidator.FormComponentsValid(new ComboBox[] {cmbFrom, cmbFrom},
                new DateTimePicker[] {dtpDate, dtpTime}))
            {
                MessageBox.Show("Die Eingabefelder sind nicht korrekt ausgefüllt",
                    "Invalide Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dateTime = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour,
                dtpTime.Value.Minute, 0);

            var connections = _transportService.GetConnections(cmbFrom.Text, cmbTo.Text, dateTime);

            if (connections.Count < 1)
            {
                MessageBox.Show("Es wurden keine Verbindungen anhand der Stationen und dem Zeitpunkt gefunden.",
                    "Suche fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvConnections.Rows.Clear();
            foreach (var connection in connections)
            {
                dgvConnections.Rows.Add(Convert.ToDateTime(connection.From.Departure).ToString("HH:mm"),
                    Convert.ToDateTime(connection.To.Arrival).ToString("HH:mm"),
                    connection.Duration.Remove(0, 3).Remove(5, 3) + " h");
            }
        }

        private void BtnGetStationBoard_Click(object sender, EventArgs e)
        {
            if (!FormValidator.FormComponentsValid(new ComboBox[] { cmbFrom, cmbFrom },
                new DateTimePicker[] { dtpDate, dtpTime }))
            {
                MessageBox.Show("Die Eingabefelder sind nicht korrekt ausgefüllt",
                    "Invalide Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var stationBoards = _transportService.GetStationConnections(cmbStation.Text);

            if (stationBoards.Count < 1)
            {
                MessageBox.Show("Es wurden keine Verbindungen anhand der eingegebenen Station gefunden",
                    "Suche fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvStationConnections.Rows.Clear();
            foreach (var stationBoard in stationBoards)
            {
                dgvStationConnections.Rows.Add(stationBoard.Number,
                    stationBoard.To,
                    Convert.ToDateTime(stationBoard.Stop.Departure).ToString("HH:mm"));
            }
        }
    }
}