using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace WPF_Base_Styling_App.UI.Windows
{
    [TemplatePart(Name = "PART_WindowFrame", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Maximize", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Minimize", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Close", Type = typeof(FrameworkElement))]
    public class StyledWindow : Window
    {
        private readonly FrameworkElement _windowFrame;
        private readonly FrameworkElement _closeButton;
        private readonly FrameworkElement _maximizeButton;
        private readonly FrameworkElement _minimizeButton;
        private bool _mRestoreIfMove;

        public bool CloseActive { get; private set; }
        public bool MinimizeActive { get; private set; }
        public bool MaximizeActive { get; private set; }

        public StyledWindow()
        {
            Style = (Style)FindResource("WindowStyle");
            ApplyTemplate();
            
            SourceInitialized += OnSourceInitialized;

            _windowFrame = Template.FindName("PART_WindowFrame", this) as FrameworkElement;
            _closeButton = Template.FindName("PART_Close", this) as FrameworkElement;
            _maximizeButton = Template.FindName("PART_Maximize", this) as FrameworkElement;
            _minimizeButton = Template.FindName("PART_Minimize", this) as FrameworkElement;
            
            _windowFrame.PreviewMouseMove += WindowFrame_OnPreviewMouseMove;
            _windowFrame.MouseLeftButtonDown += WindowFrame_OnMouseLeftButtonDown;
            _closeButton.MouseLeftButtonDown += SkipBubblingEvent_OnMouseLeftButtonDown;
            _maximizeButton.MouseLeftButtonDown += SkipBubblingEvent_OnMouseLeftButtonDown;
            _minimizeButton.MouseLeftButtonDown += SkipBubblingEvent_OnMouseLeftButtonDown;
            
            _closeButton.PreviewMouseLeftButtonUp += CloseButton_OnPreviewMouseLeftButtonUp;
            _maximizeButton.PreviewMouseLeftButtonUp += MaximizeButton_OnPreviewMouseLeftButtonUp;
            _minimizeButton.PreviewMouseLeftButtonUp += MinimizeButton_OnPreviewMouseLeftButtonUp;
            Loaded += Window_UpdatePositionOnLoad;
        }

        private void Window_UpdatePositionOnLoad(object sender, RoutedEventArgs e)
        {
            // Override the WindowStartupLocation when the window is loaded.
            // This helps to solve positioning Dialog windows via ShowDialog().
            if (this == Application.Current.MainWindow)
                return;

            POINT mainWindow;

            if (Owner != null) {
                mainWindow = new POINT((int)Owner.Left, (int)Owner.Top);
            } else
            {
                Window Main = Application.Current.MainWindow;
                mainWindow = new POINT((int)Main.Left, (int)Main.Top);
            }

            IntPtr mainWindowScreen = MonitorFromPoint(mainWindow, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            MONITORINFO mainWindowMonitor = new MONITORINFO();
            
            if (!GetMonitorInfo(mainWindowScreen, mainWindowMonitor))
                return;

            if (Owner == null && WindowStartupLocation == WindowStartupLocation.CenterOwner)
                // No owner, fall back to screen
                WindowStartupLocation = WindowStartupLocation.CenterScreen;

            switch(WindowStartupLocation)
            {
                case WindowStartupLocation.CenterOwner:
                    // Center the window on the center of the main window.
                        Left = Owner.Left + ((Owner.ActualWidth - ActualWidth) / 2);
                        Top = Owner.Top + ((Owner.ActualHeight - ActualHeight) / 2);
                    break;
                case WindowStartupLocation.CenterScreen:
                    // Center the window on the same monitor the main window is on.
                    int MonitorWidth = mainWindowMonitor.rcMonitor.Right - mainWindowMonitor.rcMonitor.Left,
                        MonitorHeight = mainWindowMonitor.rcMonitor.Bottom - mainWindowMonitor.rcMonitor.Top;
                    Left = mainWindowMonitor.rcMonitor.Left + ((MonitorWidth - (int)ActualWidth) / 2);
                    Top = mainWindowMonitor.rcMonitor.Top + ((MonitorHeight - (int)ActualHeight) / 2);
                break;
                case WindowStartupLocation.Manual:
                    // Gets the position of the window on the default monitor, then moves it relative to the same monitor the main window is on.
                    StyledWindow.POINT currentWindow = new StyledWindow.POINT((int)Left, (int)Top);
                    IntPtr currentWindowScreen = StyledWindow.MonitorFromPoint(currentWindow, StyledWindow.MonitorOptions.MONITOR_DEFAULTTONEAREST);
                    StyledWindow.MONITORINFO currentWindowMonitor = new StyledWindow.MONITORINFO();
                    
                    if (!GetMonitorInfo(currentWindowScreen, currentWindowMonitor))
                        return;
                    
                    Left = mainWindowMonitor.rcMonitor.Left + (((int)Left - currentWindowMonitor.rcMonitor.Left) / 2);
                    Top = mainWindowMonitor.rcMonitor.Top + (((int)Top - currentWindowMonitor.rcMonitor.Top) / 2);
                break;
            }
        }

        private void SkipBubblingEvent_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            CloseActive = false;
            MinimizeActive = false;
            MaximizeActive = false;

            if (sender == _closeButton)
                CloseActive = true;
            else if (sender == _maximizeButton)
                MaximizeActive = true;
            else if (sender == _minimizeButton)
                MinimizeActive = true;

            mouseButtonEventArgs.Handled = true;
        }

        private void OnSourceInitialized(object sender, EventArgs eventArgs)
        {
            IntPtr mWindowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(mWindowHandle).AddHook(WindowProc);
        }

        private void CloseButton_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (CloseActive)
                Close();
        }

        private void MaximizeButton_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (MaximizeActive)
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void MinimizeButton_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (MinimizeActive)
                WindowState = WindowState.Minimized;
        }

        private void WindowFrame_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseActive = false;
            MinimizeActive = false;
            MaximizeActive = false;

            if (e.ClickCount == 2)
            {
                if ((ResizeMode == ResizeMode.CanResize) || (ResizeMode == ResizeMode.CanResizeWithGrip))
                    SwitchWindowState();
                return;
            }

            if (WindowState == WindowState.Maximized)
            {
                _mRestoreIfMove = true;
                return;
            }

            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void WindowFrame_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_mRestoreIfMove)
            {
                _mRestoreIfMove = false;

                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = RestoreBounds.Height * percentVertical;

                WindowState = WindowState.Normal;

                POINT lMousePosition;
                GetCursorPos(out lMousePosition);

                Left = lMousePosition.X - targetHorizontal;
                Top = lMousePosition.Y - targetVertical;

                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }
		
		private static void WmGetMinMaxInfo(IntPtr hWnd, IntPtr lParam)
		{
            RECT winRect;
		    GetWindowRect(new HandleRef(null, hWnd), out winRect);

		    POINT winPoint = new POINT(winRect.Left, winRect.Top);

			// Get primary monitor info.
            IntPtr lCurrentScreen = MonitorFromPoint(winPoint, MonitorOptions.MONITOR_DEFAULTTONEAREST);
			MONITORINFO lCurrentScreenInfo = new MONITORINFO();

            // Get taskbar info
			Taskbar taskbarInfo = new Taskbar();
			IntPtr lTaskbarScreen = MonitorFromPoint(new POINT(taskbarInfo.Location.X, taskbarInfo.Location.Y), MonitorOptions.MONITOR_DEFAULTTONEAREST);
			MONITORINFO lTaskBarScreenInfo = new MONITORINFO();

			if (!GetMonitorInfo(lCurrentScreen, lCurrentScreenInfo) || !GetMonitorInfo(lTaskbarScreen, lTaskBarScreenInfo))
				return;

            int left = 0, top = 0;
			int right = Math.Abs(lCurrentScreenInfo.rcWork.Right - lCurrentScreenInfo.rcWork.Left);
			int bottom = Math.Abs(lCurrentScreenInfo.rcWork.Bottom - lCurrentScreenInfo.rcWork.Top);

			if (taskbarInfo.AutoHide) {
				// Reserve space for taskbar to get mouse focus
				switch (taskbarInfo.Position)
				{
					case TaskbarPosition.Bottom:
						bottom -= 2;
						break;
					case TaskbarPosition.Left:
						left += 2;
						break;
					case TaskbarPosition.Right:
						right -= 2;
						break;
					case TaskbarPosition.Top:
						top += 2;
						break;
				}
			} else {
				// Reserve space for taskbar to get mouse focus
				switch (taskbarInfo.Position)
				{
					case TaskbarPosition.Bottom:
						//bottom -= taskbarInfo.Size.Height;
						break;
					case TaskbarPosition.Left:
						left += taskbarInfo.Size.Width;
				        right += 2;
						break;
					case TaskbarPosition.Right:
						//right -= taskbarInfo.Size.Width;
						break;
					case TaskbarPosition.Top:
						top += taskbarInfo.Size.Height;
						break;
				}
			}

			MINMAXINFO lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            lMmi.ptMaxPosition.X = left;
            lMmi.ptMaxPosition.Y = top;
            lMmi.ptMaxSize.X = right;
            lMmi.ptMaxSize.Y = bottom;
            
            Marshal.StructureToPtr(lMmi, lParam, true);
		}
		 
        private void SwitchWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                {
                    WindowState = WindowState.Maximized;
                    break;
                }
                case WindowState.Maximized:
                {
                    WindowState = WindowState.Normal;
                    break;
                }
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        public enum TaskbarPosition
        {
            Unknown = -1,
            Left,
            Top,
            Right,
            Bottom,
        }

        public sealed class Taskbar
        {
            private const string ClassName = "Shell_TrayWnd";

            public Rectangle Bounds
            {
                get;
                private set;
            }

            public TaskbarPosition Position
            {
                get;
                private set;
            }

            public System.Drawing.Point Location
            {
                get
                {
                    return this.Bounds.Location;
                }
            }

            public System.Drawing.Size Size
            {
                get
                {
                    return this.Bounds.Size;
                }
            }

            //Always returns false under Windows 7
            public bool AlwaysOnTop
            {
                get;
                private set;
            }

            public bool AutoHide
            {
                get;
                private set;
            }

            public Taskbar()
            {
                IntPtr taskbarHandle = User32.FindWindow(Taskbar.ClassName, null);

                APPBARDATA data = new APPBARDATA();
                data.cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA));
                data.hWnd = taskbarHandle;
                IntPtr result = Shell32.SHAppBarMessage(ABM.GetTaskbarPos, ref data);
                if (result == IntPtr.Zero)
                    throw new InvalidOperationException();

                this.Position = (TaskbarPosition)data.uEdge;
                this.Bounds = Rectangle.FromLTRB(data.rc.Left, data.rc.Top, data.rc.Right, data.rc.Bottom);

                data.cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA));
                result = Shell32.SHAppBarMessage(ABM.GetState, ref data);
                int state = result.ToInt32();
                this.AlwaysOnTop = (state & ABS.AlwaysOnTop) == ABS.AlwaysOnTop;
                this.AutoHide = (state & ABS.Autohide) == ABS.Autohide;
            }
        }

        public enum ABM : uint
        {
            New = 0x00000000,
            Remove = 0x00000001,
            QueryPos = 0x00000002,
            SetPos = 0x00000003,
            GetState = 0x00000004,
            GetTaskbarPos = 0x00000005,
            Activate = 0x00000006,
            GetAutoHideBar = 0x00000007,
            SetAutoHideBar = 0x00000008,
            WindowPosChanged = 0x00000009,
            SetState = 0x0000000A,
        }

        public enum ABE : uint
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }

        public static class ABS
        {
            public const int Autohide = 0x0000001;
            public const int AlwaysOnTop = 0x0000002;
        }

        public static class Shell32
        {
            [DllImport("shell32.dll", SetLastError = true)]
            public static extern IntPtr SHAppBarMessage(ABM dwMessage, [In] ref APPBARDATA pData);
        }

        public static class User32
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public uint cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public ABE uEdge;
            public RECT rc;
            public int lParam;
        }
    }
}
