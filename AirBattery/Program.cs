namespace AirBattery;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        AirBatteryForm? mainForm;
        try
        {
            mainForm = new AirBatteryForm();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
            return;
        }

        Application.Run(mainForm);
    }

}