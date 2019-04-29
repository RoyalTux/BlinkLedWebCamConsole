namespace BlinkWebCam
{
    class BlinkLed
    {
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WS_CHILD = 0x40000000;
        private const int WS_POPUP = unchecked((int)0x80000000);
        private const int WM_CAP_SAVEDIB = 0x419;

        [System.Runtime.InteropServices.DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
        public static extern System.IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(System.IntPtr hWnd, uint Msg, int wParam, int lParam);

        public void ControllCamWithoutScreenshot()
        {
            System.IntPtr hWndC = capCreateCaptureWindowA("VFW Capture", WS_POPUP | WS_CHILD, 0, 0, 320, 240, 0, 0); // get camera handle
            SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0); // connect to camera
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0); // turn off the camera
        }

        public void ControllCamWithScreenshot()
        {
            string dName = "".PadRight(100);
            string dVersion = "".PadRight(100);
            System.IntPtr hWndC = capCreateCaptureWindowA("VFW Capture", WS_POPUP | WS_CHILD, 0, 0, 320, 240, 0, 0); // get camera handle
            SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0); // connect to camera
            string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Pictures\\" + System.DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".jpg";
            System.IntPtr hBmp = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt32()); // save screenshot
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0); // turn off the camera
        }
    }
}
