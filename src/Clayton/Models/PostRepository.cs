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
                // http://stackoverflow.com/questions/36725543/many-to-many-query-in-entity-framework-7
                // Must eager load for many to many

                return _appDbContext.Posts
                    .Include(x => x.PostCategory)
                    .ThenInclude(x => x.Category);
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
            post.CreateDate = DateTime.Now;

            // Now add many to many cateogry/post relations now that we have the ID
            PostCategory postCategories = new PostCategory();
            foreach (var category in SelectedCategories)
            {
                post.PostCategory = new List<PostCategory>();
                post.PostCategory.Add(new PostCategory { CategoryId = category.CategoryId });
            }

            // Save post so we get the ID
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
                dataPost.Active = false;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public Post GetPostById(int postId)
        {

            return _appDbContext.Posts
                .Where(x => x.PostId == postId)
                .Include(x => x.PostCategory)
                .ThenInclude(x => x.Category)
                .FirstOrDefault();

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

        public void UpdatePost(PostViewModel model)
        {
            var dataPost = GetPostById(model.Post.PostId);
            if (dataPost != null)
            {
                dataPost.Title = model.Post.Title;
                dataPost.Content = model.Post.Content;
                dataPost.Description = model.Post.Description;
                dataPost.DescriptionPicture = model.Post.DescriptionPicture;
                dataPost.Slug = model.Post.Slug;

                List<Category> SelectedCategories = new List<Category>();
                foreach (var catId in model.SelectedCategories)
                {
                    SelectedCategories.Add(_categryRepository.GetById(Convert.ToInt32(catId)));
                }

                // Add create date
                dataPost.ModifiedDate = DateTime.Now;


                // Attach new list of post categories if they don't exist
                if(dataPost.PostCategory == null)
                {
                    dataPost.PostCategory = new List<PostCategory>();
                }

                // Add new categories
                PostCategory postCategories = new PostCategory();
                foreach (var category in SelectedCategories)
                {
                    var exists = dataPost.PostCategory.Any(x => x.CategoryId == category.CategoryId);
                    if (!exists)
                    {
                        dataPost.PostCategory.Add(new PostCategory { CategoryId = category.CategoryId });
                    }
                }

                // Remove any that were removed
                IEnumerable<PostCategory> activeCategories = dataPost.PostCategory.ToList();
                foreach (var category in activeCategories)
                {
                    var exists = SelectedCategories.Any(x => x.CategoryId == category.CategoryId);
                    if (!exists)
                    {
                        dataPost.PostCategory.Remove(category);
                    }
                }

                // Save post
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

        public void ActivatePost(int postId)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            if (dataPost != null)
            {
                dataPost.Active = true;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public void DeactivatePost(int postId)
        {
            var dataPost = _appDbContext.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            if (dataPost != null)
            {
                dataPost.Active = false;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }
    }
}
