using System;
using System.Windows.Forms;

namespace AirBattery
{
    public partial class AirBatteryForm : Form
    {
        private readonly NotifyIcon _notifyIcon;

        public AirBatteryForm()
        {
            InitializeComponent();
            UpdateTaskbarIcon(69);

            // Initialize the notify icon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = SystemIcons.Information;
            _notifyIcon.Text = "Battery Percentage: 0%";
            _notifyIcon.Visible = true;
        }
        
        private Icon GetBatteryIcon(int batteryPercentage)
        {
            // Create a new Bitmap object with the size of the icon
            var bitmap = new Bitmap(16, 16);

            // Create a Graphics object from the Bitmap
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Draw the battery outline
                graphics.DrawRectangle(Pens.Black, 0, 0, 15, 15);

                // Calculate the width of the battery fill based on the percentage
                int fillWidth = (int)(batteryPercentage / 100.0 * 15.0);

                // Draw the battery fill
                graphics.FillRectangle(Brushes.Green, 1, 1, fillWidth, 14);
            }

            // Create an Icon object from the Bitmap
            return Icon.FromHandle(bitmap.GetHicon());
        }

        private void UpdateTaskbarIcon(int batteryPercentage)
        {
            // Create a new Bitmap object with the desired size
            var bitmap = new Bitmap(16, 16);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.DrawString(batteryPercentage.ToString(), SystemFonts.DefaultFont, Brushes.Black, new PointF(0, 0));
            try
            {
                _notifyIcon.Icon = Icon.FromHandle(bitmap.GetHicon());
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


            // Create a Graphics object from the Bitmap
            using (var g = Graphics.FromImage(bitmap))
            {
                // Clear the Bitmap
                g.Clear(Color.Transparent);

                // Draw the battery percentage as text on the Bitmap
                g.DrawString(batteryPercentage.ToString(), SystemFonts.DefaultFont, Brushes.Black, new PointF(0, 0));
            }

            // Set the Bitmap as the Icon for the taskbar
            _notifyIcon.Icon = Icon.FromHandle(bitmap.GetHicon());
        }


        private void AirBatteryForm_Load(object sender, EventArgs e)
        {
            // Retrieve the battery percentage of the AirPods using the API
            var batteryPercentage = GetAirPodsBatteryPercentage();

            // Update the taskbar notification to display the battery percentage
            _notifyIcon.Text = "Battery Percentage: " + batteryPercentage + "%";

            // Update the taskbar notification icon to represent the battery level
            _notifyIcon.Icon = GetBatteryIcon(batteryPercentage);
        }


        private int GetAirPodsBatteryPercentage()
        {
            return 50;
        }
    }
}