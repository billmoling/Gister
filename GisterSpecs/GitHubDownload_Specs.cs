using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;
using NUnit.Framework;

namespace GisterSpecs
{
    [TestFixture]
    public class GitHubDownload_Specs
    {
        

        [Test]
        public void DownloadGistList()
        {
            ProcessGistList obj = new ProcessGistList();
            GitHubUserCredentials mycred=new GitHubUserCredentials ("XXXXX","XXXX");
            obj.GetUserGistList(mycred);

        }
    }
}
