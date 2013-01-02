using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EchelonTouchInc.Gister.Api.Credentials;
using FluentHttp;
using Newtonsoft.Json.Linq;

namespace EchelonTouchInc.Gister.Api
{
    public class HttpGitHubDownloader:IGitHubReceiver
    {
        public JArray ListGist(GitHubCredentials credentials)
        {
            using (var stream =new MemoryStream())
            {
                var request = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .ResourcePath("/gists")
                .Method("GET")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .OnResponseHeadersReceived((o, e) => e.SaveResponseIn(stream));

                AppliesGitHubCredentialsToFluentHttpRequest.ApplyCredentials(credentials, request);

                var response = request.Execute();

                if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                    throw new GitHubUnauthorizedException(response.Response.HttpWebResponse.StatusDescription);

                response.Response.SaveStream.Seek(0, SeekOrigin.Begin);

                var rawGistJson = FluentHttpRequest.ToString(response.Response.SaveStream);

                JArray rawGistJsonArray=JArray.Parse(rawGistJson);

                return rawGistJsonArray;

            }
        }

        public string GetGistById(string gistId, GitHubCredentials credentials)
        {
            using (var stream = new MemoryStream())
            {
                var request = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .ResourcePath(string.Format("/gists/{0}",gistId))
                .Method("GET")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .OnResponseHeadersReceived((o, e) => e.SaveResponseIn(stream));

                AppliesGitHubCredentialsToFluentHttpRequest.ApplyCredentials(credentials, request);

                var response = request.Execute();

                if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                    throw new GitHubUnauthorizedException(response.Response.HttpWebResponse.StatusDescription);

                response.Response.SaveStream.Seek(0, SeekOrigin.Begin);

                var rawGistJson = FluentHttpRequest.ToString(response.Response.SaveStream);

                JObject rawGistJsonObj =JObject.Parse(rawGistJson);

                //JArray rawGistJsonFiles=JArray.Parse(rawGistJsonObj

                //TODO: the file name is different for each Gist, so need to find a way to get the file. Also need to deal with the multi files case.

                if (rawGistJsonObj.Property("files").Value.Count() != 1)
                {
                    //TODO:multi files insert is not supported yet
                    throw new NotImplementedException("multi files insert is not supported yet");
                }

                //TODO:need to deal with the result
                dynamic rawFilesJson = rawGistJsonObj.Property("files").Value[0];

                return rawFilesJson.content;


            }
        }
    }
}
