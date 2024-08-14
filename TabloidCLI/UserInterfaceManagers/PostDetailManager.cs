using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostDetailManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;
        private string _connectionString;


        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    return new NoteManager(this, _connectionString, post.Id);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title}");
            Console.WriteLine($"URL: {post.Url}");
            Console.WriteLine($"Published on: {post.PublishDateTime}");
            Console.WriteLine($"Tags: {string.Join(", ", post.Tags)}");
        }

        private void AddTag()
        {
            Console.WriteLine("Select a tag to add:");

            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.InsertTag(new Post { Id = _postId }, tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
            }
        }

        private void RemoveTag()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine("Select a tag to remove:");

            for (int i = 0; i < post.Tags.Count; i++)
            {
                Tag tag = post.Tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = post.Tags[choice - 1];
                _postRepository.DeleteTag(_postId, tag.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
            }
        }
    }
}
