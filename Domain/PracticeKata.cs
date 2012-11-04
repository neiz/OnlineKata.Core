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
    //[BsonIgnoreExtraElements]
    public class PracticeKata
    {

        [BsonId]
        public ObjectId PracticeKataId { get; set; }
        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }
        [ScaffoldColumn(false)]
        public string KataChallengeId { get; set; }
        [ScaffoldColumn(false)]
        public string Author { get; set; }
        [ScaffoldColumn(false)]
        public string KataCode { get; set; }
        [ScaffoldColumn(false)]
        public string KataTimeSpan { get; set; }
        [ScaffoldColumn(false)]
        public int TotalComments { get; set; }
        [ScaffoldColumn(false)]
        public IList<Comment> Comments { get; set; }
    }
}
