using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace dama_oyunu
{
    class taslar
    {
        public taslar(Size size)
        {
            picSize = size;
        }
        private Size picSize;
        private Image kPic = Image.FromFile(@"Red.png");
        private Image sPic = Image.FromFile(@"Black.png");
        private Image kkPic = Image.FromFile(@"RedKing.png");
        private Image skPic = Image.FromFile(@"BlackKing.png");
        public Image Getkırımızı()
        {
            return ResizeImage(kPic);
        }

        public Image Getsiyah()
        {
            return ResizeImage(sPic);
        }

        public Image Getkkralice()
        {
            return ResizeImage(kkPic);
        }

        public Image Getskralice()
        {
            return ResizeImage(skPic);
        }

        public static Image ResizeImage(Image image) // resimleri orantılama
        { 
            int originalWidth = image.Width;
            int originalHeight = image.Height;
            return image;
        }
    }
}