﻿using System;
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

        void UpdatePost(PostViewModel post);

        void DeletePost(int postId);

        Post AddPost(PostViewModel postModel);

        void HardDelete(int postId);
        void RevivePost(int postId);
        void ActivatePost(int postId);
        void DeactivatePost(int postId);
    }
}
