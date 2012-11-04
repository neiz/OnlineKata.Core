using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineKata.Core.Domain
{
    public class KataChallenge
    {
        [BsonId]
        public ObjectId KataChallengeId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Summary { get; set; }
        [AllowHtml]
        public string Tips { get; set; }
        public string Tests { get; set; }
        public int Difficulty { get; set; }
        public double Random { get; set; }
    }
}
