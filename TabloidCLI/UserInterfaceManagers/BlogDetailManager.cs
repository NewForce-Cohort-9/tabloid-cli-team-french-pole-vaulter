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
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
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
                    /*AddTag();*/
                    return this;
                case "3":
                    /*RemoveTag();*/
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
            Console.WriteLine($"URL: {blog.Url}\n");
        }

    }
}


/*
Given the user is viewing the Blog Management menu
When they select the option to view blog details
Then they should be presented with a list of blogs to choose from

Given the user chooses an blog
When they enter the selection and hit enter
Then the Blog Details menu should be displayed

Given the user wishes to view the blog details
When they select "View"
Then the blog's title and url should be displayed

The Blog Detail menu should have the following options:

View

Add Tag

Remove Tag

View Posts

Return
 */
