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
    public class PracticeKataService
    {
        private readonly MongoHelper<PracticeKata> _practiceKatas;

        public PracticeKataService()
        {
            _practiceKatas = new MongoHelper<PracticeKata>();
        }
        

        /// <summary>
        ///  CREATE ACTION for PracticeKata modeled data
        /// </summary>
        /// <param name="practiceKata"></param>

        public void Create(PracticeKata practiceKata)
        {
            practiceKata.Comments = new List<Comment>();
            _practiceKatas.Collection.Save(practiceKata);
        }


        /// <summary>
        /// EDIT ACTION for PracticeKata modeled data
        /// </summary>
        /// <param name="practiceKata"></param>

        public void Edit(PracticeKata practiceKata)
        {
            _practiceKatas.Collection.Update(
                Query.EQ("_id", practiceKata.PracticeKataId), 
                Update.Set("Date", practiceKata.Date)
                    .Set("KataChallengeId", practiceKata.KataChallengeId)
                    .Set("User", practiceKata.Author)
                    .Set("KataCode", practiceKata.KataCode)
                    .Set("KataTimeSpan", practiceKata.KataTimeSpan));
        }

        /// <summary>
        /// DELETE ACTION for PracticeKata modeled data
        /// </summary>
        /// <param name="PracticeKataId"></param>

        public void Delete(ObjectId PracticeKataId)
        {
            _practiceKatas.Collection.Remove(Query.EQ("_id", PracticeKataId));
        }

        /// <summary>
        /// RETRIEVE PracticeKata count
        /// </summary>
        /// <returns></returns>

        public IList<PracticeKata> GetPracticeKatas()
        {
            return _practiceKatas.Collection.FindAll().SetFields(Fields.Exclude("Comments")).SetSortOrder(SortBy.Descending("Date")).ToList();
        }

        public PracticeKata GetPracticeKata(ObjectId id)
        {
            var practiceKata = _practiceKatas.Collection.Find(Query.EQ("_id", id)).SetFields(Fields.Slice("Comments", -5)).Single();
            practiceKata.Comments = practiceKata.Comments.OrderByDescending(c => c.Date).ToList();
            return practiceKata;
        }

        public PracticeKata GetPracticeKata(string url)
        {
            var practiceKata = _practiceKatas.Collection.Find(Query.EQ("Url", url)).SetFields(Fields.Slice("Comments", -5)).Single();
            practiceKata.Comments = practiceKata.Comments.OrderByDescending(c => c.Date).ToList();
            return practiceKata;
        }
    }
}