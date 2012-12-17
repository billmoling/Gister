using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister.Api
{
    public class NoWhereGitHubSender : IGitHubSender
    {
        public string SendGist(string fileName, string content,string description,bool isPublic, GitHubCredentials credentials)
        {
            return "";
        }
    }
}