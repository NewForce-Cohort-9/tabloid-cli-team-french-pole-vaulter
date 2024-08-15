using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private TagRepository _tagRepository;
        private PostRepository _postRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"\n\tBlog Details: {blog.Title}");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) View Posts");
            Console.WriteLine(" 0) Return");

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
                    /*ViewPosts();*/
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"\nTitle: {blog.Title}");
            Console.WriteLine($"URL: {blog.Url}");
            if (blog.Tags.IsNullOrEmpty())
            {
                Console.WriteLine("Tags: *none*\n");
            }
            else
            {
                Console.WriteLine($"Tags:\n {string.Join(", ", blog.Tags)}\n");
            }
            Console.WriteLine("\nPress any key to return to Details menu...");
            Console.ReadKey();

        }

        private void AddTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            Console.WriteLine($"\nWhich tag would you like to add to {blog.Title}?\n");
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
                _blogRepository.InsertTag(blog, tag);

                Console.WriteLine($"\n\tTag: {tag.Name} successfully added to {blog.Title}.");
                Console.WriteLine("\n\nPress any key to return to Details menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. No tag added to this blog.");
            }
        }


        private void RemoveTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            Console.WriteLine($"\nWhich tag would you like to remove from {blog.Title}?\n");
            List<Tag> tags = blog.Tags;

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
                _blogRepository.DeleteTag(blog.Id, tag.Id);

                Console.WriteLine($"\n\tTag: {tag.Name} successfully removed from {blog.Title}.");
                Console.WriteLine("\n\nPress any key to return to Details menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. No tags will be removed from this blog.");
            }
        }

       /* private void ViewPosts()
        {
            List<Post> posts = _postRepository.GetByBlog(_blogId);
        }*/

    }
}

