using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Interactivity;

namespace LTEDatabase.View.Behavior
{
    class WindowAeroGlassBehavior : Behavior<Window>
    {
         private const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SourceInitialized += OnSourceInitialized;
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        protected void OnSourceInitialized(object sender, EventArgs e)
        {
            GlassHelper.ExtendGlassFrame((Window)sender, new Thickness(-1));
            IntPtr hwnd = new WindowInteropHelper((Window)sender).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DWMCOMPOSITIONCHANGED)
            {
                GlassHelper.ExtendGlassFrame(AssociatedObject, new Thickness(-1));
                handled = true;
            }
            return IntPtr.Zero;
        }
    }

    public class GlassHelper
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();

        public static bool ExtendGlassFrame(Window window, Thickness margin)
        {
            try
            {
                if (!DwmIsCompositionEnabled())
                {
                    return false;
                }
                IntPtr hwnd = new WindowInteropHelper(window).Handle;
                if (hwnd == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Вікно повинно бути відображене до встановлення розширення.");
                }
                window.Background = Brushes.Transparent;
                HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;
                MARGINS margins = new MARGINS(margin);
                DwmExtendFrameIntoClientArea(hwnd, ref margins);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public MARGINS(Thickness t)
        {
            Left = (int)t.Left;
            Top = (int)t.Top;
            Right = (int)t.Right;
            Bottom = (int)t.Bottom;
        }
    }
}