using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using OnlineKata.Core.Domain;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Web;
using OnlineKata.Core.Helpers;
using System.Diagnostics;

namespace OnlineKata.Core.Services
{
    public class KataChallengeService
    {
        private readonly MongoHelper<KataChallenge> _kataChallenges;

        public KataChallengeService()
        {
            _kataChallenges = new MongoHelper<KataChallenge>();
        }

        public KataChallenge GetRandomChallenge()
        {
            var kataChallenge = _kataChallenges.Collection.FindAll().Single();
            return kataChallenge;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}