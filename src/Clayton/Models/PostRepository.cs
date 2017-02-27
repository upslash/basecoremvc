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

        public Post AddPost(Post post)
        {
            post.Createdate = DateTime.Now;
            _appDbContext.Posts.Add(post);
            _appDbContext.SaveChanges();
            return post;
        }

        public void DeletePost(int postId)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == postId).FirstOrDefault();
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

        public void HardDelete(int postId)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            if (dataPost != null)
            {
                _appDbContext.Posts.Remove(dataPost);
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public void UpdatePost(Post post)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == post.PostId).FirstOrDefault();
            if (dataPost != null)
            {
                dataPost.Title = post.Title;
                dataPost.Content = post.Content;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public void RevivePost(int postId)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            if (dataPost != null)
            {
                dataPost.DeletedDate = null;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }
    }
}
