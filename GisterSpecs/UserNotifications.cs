﻿using System.Collections.Generic;
using System.Linq;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class UserNotifications
    {
        [Test]
        public void WillTellTheUserWhenItStarts()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file1.cs", "Dum diddy, dum diddy", "get", "real");

            // Really annoying that ShouldFluent Contains wasn't working...
            statusUpdates.LastUpdate.FirstOrDefault(x => x == "Creating gist for file1.cs").Should().Not.Be.Null();
        }

        [Test]
        public void WillTellTheUserWhenItsSuccessful()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file2.cs", "Zippity do dah, zippity ah", "get", "real");

            statusUpdates.LastUpdate.FirstOrDefault(x => x == "Gist created successfully.  Url placed in the clipboard.")
                .Should().Not.Be.Null();
        }

    }

    public class MockGitHubSender : GitHubSender
    {
        public bool SentAGist { get; private set; }
        public void SendGist(string fileName, string content, string githubusername, string githubpassword)
        {
            SentAGist = true;
        }
    }

    public class MockStatusUpdates : StatusUpdates
    {
        public List<string> LastUpdate = new List<string>();
        public void NotifyUserThat(string messagetotelltheuser)
        {
            LastUpdate.Add(messagetotelltheuser);
        }
    }
}