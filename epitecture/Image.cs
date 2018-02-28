using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epitecture.Api
{
    class Image
    {
        private String _origin { set; get; } = "";
        private String _name { set; get; } = "";
        private UInt32 _widht { set; get; } = 0;
        private UInt32 _height { set; get; } = 0;
        private String _url { set; get; } = "";

        public Image()
        {

        }

        public Image(String orgn, String name, String url, UInt32 width, UInt32 height)
        {
            _origin = orgn;
            _name = name;
            _url = url;
            _widht = width;
            _height = height;
        }
    }
}
