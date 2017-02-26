using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Post> Posts
        {
            get
            {
                return _appDbContext.Posts;
            }
        }

        // This could also be written like this.
        //public IEnumerable<Post> Posts2 => _appDbContext.Posts;

        public IEnumerable<Post> PostsOftheWeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void DeletePost(Post post)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == post.PostId).FirstOrDefault();
            if (dataPost != null)
            {
                dataPost.DeletedDate = DateTime.Now;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
            
        }

        public Post GetPostById(int postId)
        {
            return _appDbContext.Posts.FirstOrDefault(x => x.PostId == postId);
        }

        public Post UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
