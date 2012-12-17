using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister.Api
{
    public interface IGitHubSender
    {
        string SendGist(string fileName, string content,string description,bool isPublic, GitHubCredentials credentials);
    }
}