using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace 覆盤
{
    public static class Screenshot
    {
        public static void CaptureMyScreen()

        {

            try

            {

                //Creating a new Bitmap object

                Bitmap captureBitmap = new Bitmap(1200, 976, PixelFormat.Format32bppArgb);


                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);

                //Creating a Rectangle object which will  

                //capture our Current Screen

                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;



                //Creating a New Graphics Object

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);



                //Copying Image from The Screen

                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);



                //Saving the Image File (I am here Saving it in My E drive).
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/screenshot" + DateTime.Now.ToString("HHmmss") + ".jpg";
                captureBitmap.Save(path, ImageFormat.Jpeg); //@"E:\Capture.jpg", ImageFormat.Jpeg);



                //Displaying the Successfull Result



                //MessageBox.Show("Screen Captured");

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }
    }
}
