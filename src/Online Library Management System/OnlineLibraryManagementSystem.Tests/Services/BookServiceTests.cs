namespace OnlineLibraryManagementSystem.Tests.Services
{
    using Common;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using OnlineLibraryManagementSystem.Services.Implementations;
    using OnlineLibraryManagementSystem.Services.Models.Books;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class BookServiceTests
    {
        private readonly Random randomGenerator;

        public BookServiceTests()
        {
            this.randomGenerator = new Random();
            Tests.Initialize();
        }

        [Fact]
        public async Task GetBooksCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int BooksCount = 25;

            for (var i = 0; i < BooksCount; i++)
            {
                await db.Books.AddAsync(new Book
                {
                    Title = $"Some title {i}"
                });
            }

            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            var books = await bookService.GetBooksCountAsync();

            books.Should().Be(BooksCount);
        }

        [Fact]
        public async Task GetBorrowedCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 50; i++)
            {
                await db.AddAsync(new Book
                {
                    Title = $"Title {i}"
                });
            }

            const int BorrowedBooks = 25;

            for (var i = 0; i < BorrowedBooks; i++)
            {
                await db.AddAsync(new Book
                {
                    Title = $"Title {i + 50}",
                    BorrowerId = Guid.NewGuid().ToString()
                });
            }

            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            var borrowedBooks = await bookService.GetBorrowedCountAsync();

            borrowedBooks.Should().Be(BorrowedBooks);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllBooks()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 30; i++)
            {
                await db.AddAsync(new Book
                {
                    Title = $"Some rnd title{i}"
                });
            }

            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            var booksPage1 = await bookService.GetAllAsync(1);

            booksPage1
                .Should()
                .HaveCount(GlobalConstants.BooksOnPage);

            booksPage1
                .Should()
                .BeInAscendingOrder(b => b.Id);

            var booksPage2 = await bookService.GetAllAsync(2);

            booksPage2
                .Should()
                .HaveCount(GlobalConstants.BooksOnPage);

            booksPage2
                .Should()
                .BeInAscendingOrder(b => b.Id);

            var booksPage3 = await bookService.GetAllAsync(3);

            booksPage3
                .Should()
                .HaveCount(GlobalConstants.BooksOnPage);

            booksPage3
                .Should()
                .BeInAscendingOrder(b => b.Id);

            var booksPage4 = await bookService.GetAllAsync(4);

            booksPage4
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task AddAsyncShouldAddBook()
        {
            var db = DbInfrastructure.GetDatabase();

            var bookService = new BookService(db);

            var author1 = new User
            {
                UserName = "pesho"
            };

            var author2 = new User
            {
                UserName = "gosho"
            };

            await db.AddRangeAsync(author1, author2);

            const string Title = "Some title";
            const string Description = "Description of the book";

            await bookService.AddAsync(Title, Description, author1.Id);

            var book = await db.Books.FirstOrDefaultAsync();

            book.Title.Should().Be(Title);

            book.Description.Should().Be(Description);

            book.AuthorId.Should().Be(author1.Id);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnBookById()
        {
            var db = DbInfrastructure.GetDatabase();

            var books = new List<Book>();

            for (var i = 0; i < 10; i++)
            {
                var currentBook = new Book
                {
                    Title = $"title {i + 1}",
                };

                books.Add(currentBook);
            }

            await db.AddRangeAsync(books);
            await db.SaveChangesAsync();

            var expectedBook = books.First();

            var bookService = new BookService(db);

            var book = await bookService.ByIdAsync(expectedBook.Id);

            book.Title.Should().Be("title 1");
        }

        [Fact]
        public async Task ByAuthorAsyncShouldReturnBookByAuthor()
        {
            var db = DbInfrastructure.GetDatabase();

            var authors = new List<User>();

            for (var userIndex = 0; userIndex < 10; userIndex++)
            {
                var currentAuthor = new User
                {
                    UserName = $"author {userIndex + 1}"
                };

                authors.Add(currentAuthor);
                await db.AddAsync(currentAuthor);

                for (var bookIndex = 0; bookIndex < 10; bookIndex++)
                {
                    await db.AddAsync(new Book
                    {
                        Title = $"title {bookIndex + 1}",
                        AuthorId = currentAuthor.Id,
                        BorrowedTimes = this.randomGenerator.Next(1, 51)
                    });
                }
            }

            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            var author = authors.First();

            var books = await bookService.ByAuthorAsync(author.Id, 1);

            books
                .Should()
                .HaveCount(GlobalConstants.BooksOnPage);

            books
                .Should()
                .BeInAscendingOrder(b => b.BorrowedTimes);

            foreach (var book in books)
            {
                book
                    .Should()
                    .Match<BookServiceModel>(b => b.AuthorId == author.Id);
            }
        }

        [Fact]
        public async Task GetByAuthorCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            var authors = new List<User>();

            var booksCount = 50;

            for (var authorIndex = 0; authorIndex < 15; authorIndex++)
            {
                var currentAuthor = new User
                {
                    UserName = $"username {authorIndex + 1}",
                };

                authors.Add(currentAuthor);
                await db.AddAsync(currentAuthor);

                for (var bookIndex = 0; bookIndex < booksCount; bookIndex++)
                {
                    await db.AddAsync(new Book
                    {
                        Title = $"Title {bookIndex + 1}",
                        AuthorId = currentAuthor.Id
                    });
                }

                booksCount += this.randomGenerator.Next(10, 51);
            }

            booksCount = 50;

            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            var author = authors.First();

            var result = await bookService.GetByAuthorCountAsync(author.Id);

            booksCount.Should().Be(booksCount);
        }

        [Fact]
        public void BorrowAsyncShouldThrowInvalidOperationExceptionIfUserIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var bookService = new BookService(db);

            Func<Task> func = async () => await bookService.BorrowAsync(1, "UsErNaMe");

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.UserNotFound);
        }

        [Fact]
        public async Task BorrowAsyncShouldThrowInvalidOperationExceptionIfBookIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var user = new User
            {
                UserName = "test"
            };

            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            Func<Task> func = async () => await bookService.BorrowAsync(15, user.UserName);

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.BookNotFound);
        }

        [Fact]
        public async Task BorrowAsyncShouldThrowInvalidOperationExceptionIfBookIsAlreadyBorrowed()
        {
            var db = DbInfrastructure.GetDatabase();

            var user = new User
            {
                UserName = "test"
            };

            var book = new Book
            {
                Title = "Title test",
                BorrowerId = Guid.NewGuid().ToString()
            };

            await db.AddAsync(book);
            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var bookService = new BookService(db);

            Func<Task> func = async () => await bookService.BorrowAsync(book.Id, user.UserName);

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.BookAlreadyBorrowed);
        }

        [Fact]
        public async Task BorrowAsyncShouldMarkBookAsBorrowedAndIncrementItsBorrowedTimes()
        {
            var db = DbInfrastructure.GetDatabase();

            var bookService = new BookService(db);

            var user = new User
            {
                UserName = "test userName"
            };

            for (var i = 0; i < 15; i++)
            {
                await db.AddAsync(new User
                {
                    UserName = $"Test User {i}"
                });

                await db.AddAsync(new Book
                {
                    Title = $"Test Book {i + 10}"
                });
            }

            const string BookTitle = "New Book";

            var book = new Book
            {
                Title = BookTitle
            };

            await db.AddAsync(book);
            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var bookTitle = await bookService.BorrowAsync(book.Id, user.UserName);

            bookTitle.Should().Be(BookTitle);

            book.BorrowerId.Should().Be(user.Id);

            book.BorrowedTimes.Should().Be(1);
        }

        [Fact]
        public void MyBorrowedAsyncShoulThrowInvalidOperationExceptionIfUserIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var bookService = new BookService(db);

            Func<Task> func = async () => await bookService.MyBorrowedAsync("UsErNaMe", 1);

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.UserNotFound);
        }

        [Fact]
        public async Task MyBorrowedAsyncShoulReturnCorrectBooks()
        {
            var db = DbInfrastructure.GetDatabase();

            const int UsersCount = 60;

            const int BooksPerAuthor = 15;

            var bookService = new BookService(db);

            const string UserNameConst = "User No: 1";

            for (var userIndex = 0; userIndex < UsersCount; userIndex++)
            {
                var currentUser = new User
                {
                    UserName = $"User No: {userIndex + 1}"
                };

                await db.AddAsync(currentUser);

                for (var bookIndex = 0; bookIndex < BooksPerAuthor; bookIndex++)
                {
                    var book = new Book
                    {
                        Title = $"New Book No: {bookIndex}",
                        AuthorId = currentUser.Id,
                        BorrowedTimes = bookIndex
                    };

                    await db.AddAsync(book);
                    await db.SaveChangesAsync();

                    if (currentUser.UserName == UserNameConst)
                    {
                        await bookService.BorrowAsync(book.Id, currentUser.UserName);
                    }
                }
            }

            await db.SaveChangesAsync();

            var user = await db.Users.FirstAsync(u => u.UserName == UserNameConst);

            var booksPage1 = await bookService.MyBorrowedAsync(user.UserName, 1);

            booksPage1.Should().HaveCount(GlobalConstants.BooksOnPage);

            var originalBooksPage1 = new List<Book>();

            foreach (var book in booksPage1)
            {
                originalBooksPage1.Add(await db.Books.FindAsync(book.Id));
            }

            originalBooksPage1
                .Should()
                .BeInAscendingOrder(b => b.BorrowedTimes);

            foreach (var book in originalBooksPage1)
            {
                book.Should().Match<Book>(b => b.BorrowerId == user.Id);
            }

            var booksPage2 = await bookService.MyBorrowedAsync(user.UserName, 2);

            booksPage2.Should().HaveCount(5);

            var originalBooksPage2 = new List<Book>();

            foreach (var book in booksPage1)
            {
                originalBooksPage2.Add(await db.Books.FindAsync(book.Id));
            }

            originalBooksPage2
                .Should()
                .BeInAscendingOrder(b => b.BorrowedTimes);

            foreach (var book in originalBooksPage2)
            {
                book.Should().Match<Book>(b => b.BorrowerId == user.Id);
            }
        }
    }
}