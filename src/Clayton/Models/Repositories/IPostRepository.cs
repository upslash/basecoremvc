using Clayton.Models.ViewModels;
using System.Collections.Generic;

namespace Clayton.Models.Reposistories
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }
        IEnumerable<Post> GetRecentPosts(int number);
        IEnumerable<Post> PostsOftheWeek { get; }

        Post GetPostById(int postId);

        void UpdatePost(PostViewModel post);

        void DeletePost(int postId);

        Post AddPost(PostViewModel postModel);

        void HardDelete(int postId);
        void RevivePost(int postId);
        void ActivatePost(int postId);
        void DeactivatePost(int postId);
    }
}
