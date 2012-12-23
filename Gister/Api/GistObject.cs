using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchelonTouchInc.Gister.Api
{
    public class GistObject
    {
        string _Id;
        string _rawUrl;
        string _description;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        

        public string RawUrl
        {
            get { return _rawUrl; }
            set { _rawUrl = value; }
        }
        

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public GistObject()
        {
        }
    }
}
