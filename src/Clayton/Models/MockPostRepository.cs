using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
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

        public Post GetPostById(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
