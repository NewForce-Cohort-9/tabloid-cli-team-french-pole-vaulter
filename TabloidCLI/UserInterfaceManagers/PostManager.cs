﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": List(); return this;
                case "2":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "3": Add(); return this;
                case "4": Edit(); return this;
                case "5": Remove(); return this;
                case "0": return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publish Date (yyyy-mm-dd): ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            // Choose an Author from a list
            Author selectedAuthor = ChooseAuthor();
            if (selectedAuthor != null)
            {
                post.Author = selectedAuthor;
            }
            else
            {
                Console.WriteLine("Invalid selection. Post creation aborted.");
                return;
            }

            // Choose a Blog from a list
            Blog selectedBlog = ChooseBlog();
            if (selectedBlog != null)
            {
                post.Blog = selectedBlog;
            }
            else
            {
                Console.WriteLine("Invalid selection. Post creation aborted.");
                return;
            }

            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }

            Console.Write("New URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            Console.Write("New publish date (yyyy-mm-dd) (blank to leave unchanged): ");
            string publishDate = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(publishDate))
            {
                postToEdit.PublishDateTime = DateTime.Parse(publishDate);
            }

            Console.Write("New Author Id (blank to leave unchanged): ");
            string authorIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorIdInput))
            {
                postToEdit.Author = new Author() { Id = int.Parse(authorIdInput) };
            }

            Console.Write("New Blog Id (blank to leave unchanged): ");
            string blogIdInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogIdInput))
            {
                postToEdit.Blog = new Blog() { Id = int.Parse(blogIdInput) };
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }

        private Author ChooseAuthor()
        {
            List<Author> authors = new AuthorRepository(_connectionString).GetAll();

            Console.WriteLine("Select an Author:");
            for (int i = 0; i < authors.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {authors[i].FullName}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
        }

        private Blog ChooseBlog()
        {
            List<Blog> blogs = new BlogRepository(_connectionString).GetAll();

            Console.WriteLine("Select a Blog:");
            for (int i = 0; i < blogs.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {blogs[i].Title}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
        }
    }
}
