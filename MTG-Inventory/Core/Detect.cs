using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using WebcamCapturer.Controls.WPF;
using WebcamCapturer.Core;

namespace MTG_Inventory.Core
{
    internal class Detect
    {

        public static async Task TestAsync()
        {
            var view = new WebcamCaptureWindow();
            var presenter = new WebcamCapturePresenter(view);
            //presenter.show();           
        }
    }
}
