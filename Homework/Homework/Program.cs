using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string fileName = "result.txt";
            int start = 4;
            int finish = 13;

            int k = 0;
            var postIds = new int[finish - start + 1];

            for (int i = start; i < finish + 1; i++)
            {
                postIds[k] = i;
                k++;
            }

            var postService = new PostService();
            var tasks = postIds.Select(postId => postService.GetPostAsync(postId)).ToList();
            try
            {
                var posts = await Task.WhenAll(tasks);
                foreach (var post in posts)
                {
                    await File.AppendAllLinesAsync(fileName, new[]
                    {
                        post.UserId.ToString(),
                        post.Id.ToString(),
                        post.Title,
                        post.Body
                    });
                    await File.AppendAllTextAsync(fileName, Environment.NewLine);
                }
            }
            catch
            {
                foreach (var task in tasks.Where(x => x.IsFaulted))
                {
                    await File.AppendAllLinesAsync(fileName, new[] { "Error" });
                    await File.AppendAllTextAsync(fileName, Environment.NewLine);
                }
            }
        }
    }
}
