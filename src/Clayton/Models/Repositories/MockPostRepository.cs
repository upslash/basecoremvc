using Clayton.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Clayton.Models.Reposistories
{
    public class MockPostRepository : IPostRepository
    {

        public IEnumerable<Post> Posts
        {
            get
            {
                return new List<Post>()
                {
                    new Post() { Title = "Today I learned ASP.NET", Content = "On a sunny day, in the year 2017, Clayton brought it opon himself to learn MVC." },
                    new Post() { Title = "Today I learned Linux", Content = "On a clody day, in the year 2016, Clayton decided to learn Linux." },
                    new Post() { Title = "Today I learned iOS Development", Content = "On a stormy day, in the year 2015, Clayton made the terrible choice is learning iOS." },
                };
            }
        }

        public IEnumerable<Post> PostsOftheWeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ActivatePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Post AddPost(PostViewModel model)
        {
            throw new NotImplementedException();
        }

        public void DeactivatePost(int postId)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetRecentPosts(int number)
        {
            throw new NotImplementedException();
        }

        public void HardDelete(int postId)
        {
            throw new NotImplementedException();
        }

        public void RevivePost(int postId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(PostViewModel post)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
