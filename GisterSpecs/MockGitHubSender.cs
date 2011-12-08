﻿using System;
using System.Linq;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{

    public class MockGitHubSender : GitHubSender
    {
        private string failureStatusDescription;

        public bool SentAGist { get; private set; }

        public string ResultingUrl { get; set; }

        public string LastPasswordUsed { get; private set; }

        public string LastUsernameUsed { get; private set; }

        public GitHubCredentials LastCredentialsApplied { get; private set; }

        public string SendGist(string fileName, string content, GitHubCredentials credentials)
        {
            LastUsernameUsed = githubusername;
            LastPasswordUsed = githubpassword;
            LastCredentialsApplied = credentials;

            if (ShouldThrow())
                throw new GitHubUnauthorizedException(failureStatusDescription);

            SentAGist = true;

            return ResultingUrl;
        }

        private bool ShouldThrow()
        {
            return !string.IsNullOrEmpty(failureStatusDescription);
        }

        public void FailWith(string statusDescription)
        {
            failureStatusDescription = statusDescription;
        }
    }
}
