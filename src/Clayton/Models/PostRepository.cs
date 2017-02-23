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

        public Post GetPostById(int postId)
        {
            throw new NotImplementedException();
        }


    }
}
