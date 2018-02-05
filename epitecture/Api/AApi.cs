using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epitecture.Api
{
     abstract class AApi
    {
        private String Token { set; get; };

        public abstract void Init();
        public abstract void LoadImage();
        public abstract void UploadImage();
        public abstract void SearchImage();
    }
}
