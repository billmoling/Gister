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
        public JArray ListGist(GitHubUserCredentials credentials)
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
    }
}
