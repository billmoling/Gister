﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchelonTouchInc.Gister.Api.Credentials;
using Newtonsoft.Json.Linq;

namespace EchelonTouchInc.Gister.Api
{
    public interface IGitHubReceiver
    {
        JArray ListGist(GitHubCredentials credentials);
        string GetGistById(string gistId, GitHubCredentials credentials);
    }
}