using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace epitecture.Api
{
   class Image
       {
           public String id { set; get; } = "";
           public String title { set; get; } = "";
           public String width { set; get; } = "";
           public String height { set; get; } = "";
           public String link { set; get; } = "";
           public BitmapImage data;
       }
}
