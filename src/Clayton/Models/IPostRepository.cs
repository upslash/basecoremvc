using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }

        IEnumerable<Post> PostsOftheWeek { get; }

        Post GetPostById(int postId);

        void UpdatePost(Post post);

        void DeletePost(int postId);

        Post AddPost(Post post);

        void HardDelete(int postId);
        void RevivePost(int postId);
    }
}
