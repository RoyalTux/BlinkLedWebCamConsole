namespace BlinkLedWebCam
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern System.IntPtr GetConsoleWindow();
        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);
            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            icon.Icon = new System.Drawing.Icon("WebcamIcon.ico");
            icon.Visible = true;
            icon.Text = "Blink webcam led\n\nClick for open app\nDouble click for close app";
            icon.ShowBalloonTip(1000, "I'm here now!", "In tray", System.Windows.Forms.ToolTipIcon.Info);
            icon.DoubleClick += (s, e) => { System.Environment.Exit(0); };
            icon.MouseClick += (s, e) => { ShowWindow(GetConsoleWindow(), 1); icon.Visible = false;};

            System.Console.Title = "Blink webcam led";
            System.Console.WindowWidth = 30;
            System.Console.WindowHeight = 2;
            System.Console.BackgroundColor = System.ConsoleColor.Blue;
            System.Console.ForegroundColor = System.ConsoleColor.Green;

            BlinkWebCam.BlinkLed blinkLed = new BlinkWebCam.BlinkLed();
            int milliseconds = 2000;
            System.Console.WriteLine("Press ESC for exit...");

            while (!System.Console.KeyAvailable)
            {
                blinkLed.ControllCamWithoutScreenshot();
                System.Threading.Thread.Sleep(milliseconds);
            }
            while (System.Console.ReadKey(true).Key != System.ConsoleKey.Escape);
        }
    }
}
