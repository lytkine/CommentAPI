using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentAPI.Model
{

    class CommentContextCompare : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return 0 - Comparer<DateTime>.Default.Compare(x, y);
        }
    }
}
