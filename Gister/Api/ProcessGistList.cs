using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchelonTouchInc.Gister.Api.Credentials;
using Newtonsoft.Json.Linq;

namespace EchelonTouchInc.Gister.Api
{
    public class ProcessGistList
    {
        public static IGitHubReceiver GitHubReceiver { get; set; }

        public static List<GistObject> GetUserGistList(GitHubCredentials mycredentials)
        {
            GitHubReceiver = new HttpGitHubDownloader();
            JArray gistJsonArray = GitHubReceiver.ListGist(mycredentials);
            List<GistObject> GistList = new List<GistObject>();

            for (int i = 0; i < gistJsonArray.Count; i++)
            {
                dynamic gist = JObject.Parse(gistJsonArray[i].ToString());
                GistObject gistObj = new GistObject();
                gistObj.Id = gist.id;
                gistObj.Description = gist.description;
                GistList.Add(gistObj);
            }
            return GistList;
        }


        public static string GetContentById(string Id,GitHubCredentials mycredentials)
        {
            
            GitHubReceiver = new HttpGitHubDownloader();
            string content = GitHubReceiver.GetGistById(Id, mycredentials);
            //TODO:need to process this result to the real world code style
            return content;
        }





    }
}
