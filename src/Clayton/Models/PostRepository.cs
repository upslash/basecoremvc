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
        private readonly ICategoryRepository _categryRepository;

        public PostRepository(AppDbContext appDbContext, ICategoryRepository categryRepository)
        {
            _appDbContext = appDbContext;
            _categryRepository = categryRepository;
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

        public Post AddPost(PostViewModel model)
        {
            Post post = new Post();
            post = model.Post;
            List<Category> SelectedCategories = new List<Category>();
            foreach (var catId in model.SelectedCategories)
            {
                SelectedCategories.Add(_categryRepository.GetById(Convert.ToInt32(catId)));
            }

            // Add create date
            post.Createdate = DateTime.Now;

            // Save post so we get the ID
            _appDbContext.Posts.Add(post);

            // Now add many to many cateogry/post relations now that we have the ID
            PostCategory postCategories = new PostCategory();
            foreach(var category in SelectedCategories)
            {
                post.PostCategory = new List<PostCategory>();
                post.PostCategory.Add(new PostCategory {CategoryId = category.CategoryId });
            }

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
