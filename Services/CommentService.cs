using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineKata.Core.Domain;
using OnlineKata.Core.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace OnlineKata.Core.Services
{
    public class CommentService
    {
        private readonly MongoHelper<PracticeKata> _practiceKatas;

        public CommentService()
        {
            _practiceKatas = new MongoHelper<PracticeKata>();
        }

        public void AddComment(ObjectId postId, Comment comment)
        {
            _practiceKatas.Collection.Update(Query.EQ("_id", postId),
                Update.PushWrapped("Comments", comment).Inc("TotalComments", 1));
        }

        public void RemoveComment(ObjectId postId, ObjectId commentId)
        {
            _practiceKatas.Collection.Update(Query.EQ("_id", postId),
                Update.Pull("Comments", Query.EQ("_id", commentId)).Inc("TotalComments", -1));
        }

        public IList<Comment> GetComments(ObjectId postId, int skip, int limit, int totalComments)
        {
            var newComments = GetTotalComments(postId) - totalComments;
            skip += newComments;

            var practiceKata = _practiceKatas.Collection.Find(Query.EQ("_id", postId)).SetFields(Fields.Exclude("Date", "Title", "Url", "Summary", "Details", "Author", "TotalComments").Slice("Comments", -skip, limit)).Single();
            return practiceKata.Comments.OrderByDescending(c => c.Date).ToList();
        }

        public int GetTotalComments(ObjectId postId)
        {
            var practiceKata = _practiceKatas.Collection.Find(Query.EQ("_id", postId)).SetFields(Fields.Include("TotalComments")).Single();
            return practiceKata.TotalComments;
        }
    }
}
