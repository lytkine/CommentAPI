using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommentAPI.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CommentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IMemoryCache _cache;

        public CommentsController(IMemoryCache memoryCache)
        {

            _cache = memoryCache;
        }

        private CommentContext GetCommentContext()
        {
            CommentContext _commentCtx;

            if (!_cache.TryGetValue(CacheKeys.CTX, out _commentCtx))
            {
                _commentCtx = new CommentContext();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(365));
                _cache.Set(CacheKeys.CTX, _commentCtx, cacheEntryOptions);
            }


            return _commentCtx;
        }

        // GET: api/Comments
        [HttpGet]
        public IEnumerable<CommentModel> Get()
        {
            return GetCommentContext().GetAllComments();
        }

        // GET: api/Comments/5
        [HttpGet("{id}", Name = "Get")]
        public CommentModel Get(int id)
        {
            return GetCommentContext().GetByID(id);
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] CommentModel newcomment)
        {
            if (newcomment == null)
            {
                return StatusCode(400);
            }

            newcomment.Clean();

            if (!newcomment.IsValid())
            {
                return StatusCode(400);
            }

            GetCommentContext().AddComment(newcomment);


            return StatusCode(200);
        }


        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (GetCommentContext().DeleteComment(id))
            {
                return StatusCode(200);
            }

            return StatusCode(400);
        }
    }
}
