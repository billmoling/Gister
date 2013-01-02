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
        GitHubUserCredentials mycred;
        [SetUp]
        public void Before()
        {
            mycred = new GitHubUserCredentials("XXXX", "XXXX");
        }
        

        [Test]
        public void DownloadGistList()
        {
            //throw new NotImplementedException("this test is for debugging only");
            ProcessGistList.GetUserGistList(mycred);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetGistByIdTestForMultiFiles()
        {
            ProcessGistList.GetContentById("4434052", mycred);
            
        }

        [Test]
        public void GetGistByIdTest()
        {
            ProcessGistList.GetContentById("09a1415a87b30a42411a", mycred);
        }
    }
}
