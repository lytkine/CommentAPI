using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentAPI.Model
{
    public class CommentContext
    {
        private object _lockfield = new Object();
        private int _nextID;
        private SortedList<DateTime, CommentModel> _store;

        public CommentContext()
        {
            _store = new SortedList<DateTime, CommentModel>(new CommentContextCompare());
            _nextID = 0;
        }

        public int AddComment(CommentModel comment)
        {
            comment.ID = GetNextID();
            comment.CreatedOn = DateTime.UtcNow;
            _store.Add(comment.CreatedOn, comment);

            return 0;
        }

        public IEnumerable<CommentModel> GetAllComments()
        {
            if (_store.Keys.Count == 0) return new List<CommentModel>();

            return _store.Values.ToList();
        }

        public CommentModel GetByID(int commentid)
        {
            return _store.Values.FirstOrDefault( x=> x.ID == commentid);
        }

        public bool DeleteComment(int commentid)
        {
            lock (_lockfield)
            {

                var findComment = GetByID(commentid);
                if ( findComment == null)
                {
                    return false;
                }

                _store.RemoveAt(_store.IndexOfValue(findComment));
            }

            return true;
        }

        private int GetNextID()
        {
            lock (_lockfield)
            {
                _nextID++;
                return _nextID;
            }
        }
    }
}
