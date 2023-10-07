using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string BookID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime DueDate { get; set; }
}

class Member
{
    public string MemberID { get; set; }
    public string Name { get; set; }
}
class Transaction
{
    public string Lend_bookId { get; set; }
    public string MemberID { get; set; }
    public DateTime DueDate { get; set; }
}

class Library
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();
    private List<Transaction> transactions = new List<Transaction>();

    public void AddBook(string bookID, string title, string author)
    {
        bool isDuplicate = books.Any(book => book.BookID == bookID);

        if (isDuplicate)
        {
            Console.WriteLine("\n Book with the same BookID already exists.\n");
        }
        else
        {
            
            Book book = new Book { BookID = bookID, Title = title, Author = author };
            books.Add(book);
            Console.WriteLine("\n Book added successfully.\n");
        }
    }

    public void RegisterMember(string memberID, string name)
    {
        Member member = new Member{ MemberID = memberID, Name = name };
        members.Add(member);
        Console.WriteLine("\nMember registered successfully.\n");
    }

    public void RemoveBook(string bookID)
    {
        Book book_Remove = books.FirstOrDefault(b => b.BookID == bookID);
        if (book_Remove != null)
        {
            books.Remove(book_Remove);
            Console.WriteLine("\nBook removed successfully.\n");
        }
        else
        {
            Console.WriteLine("\nBook not found. Check the details again!\n");
        }
    }

    public void RemoveMember(string memberID)
    {
        Member member_Remove = members.FirstOrDefault(m => m.MemberID == memberID);
        if (member_Remove != null)
        {
            members.Remove(member_Remove);
            Console.WriteLine("\nMember removed successfully.\n");
        }
        else
        {
            Console.WriteLine("\nMember not found. Check the details again!\n");
        }
    }

    public void SearchBookInformation(string bookID)
    {
        Book book = books.FirstOrDefault(b => b.BookID == bookID);
        if (book != null)
        {
            Console.WriteLine($"\nTitle: {book.Title}");
            Console.WriteLine($"\nAuthor: {book.Author}");
            Console.WriteLine($"\nAvailable: {book.IsAvailable}");
            Console.WriteLine($"\nDue Date: {book.DueDate}");
        }
        else
        {
            Console.WriteLine("\nBook not found.Please check book id.\n");
        }
    }

    public void SearchMemberInformation(string memberID)
    {
        Member member = members.FirstOrDefault(m => m.MemberID == memberID);
        if (member != null)
        {
            Console.WriteLine($"\nName: {member.Name}");
            Console.WriteLine($"\nMember ID: {member.MemberID}");
        }
        else
        {
            Console.WriteLine("\nMember not found.");
        }
    }

    public void DisplayBookNames()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"\nTitle: {book.Title}");
            Console.WriteLine($"Book ID: {book.BookID}");
        }
    }

    public void DisplayMemberNames()
    {
        foreach (var member in members)
        {
            Console.WriteLine($"\n Name: {member.Name}");
            Console.WriteLine($"ID: {member.MemberID}");
        }
    }

    public void LendBook(string bookID, string memberID, int daysToReturn)
    {
        Book book = books.FirstOrDefault(b => b.BookID == bookID);
        Member member = members.FirstOrDefault(m => m.MemberID == memberID);

        if (book != null && member != null && book.IsAvailable)
        {
            book.IsAvailable = false;
            book.DueDate = DateTime.Now.AddDays(daysToReturn);
            Transaction transaction = new Transaction
            {
                Lend_bookId = book.BookID,
                MemberID = member.MemberID,
                DueDate = book.DueDate
            };
            transactions.Add(transaction);
            Console.WriteLine("\nBook lent successfully.");
        }
        else
        {
            Console.WriteLine("\n Book Lending is failed. Check book availability or member information.");
        }
    }

    public void ReturnBook(string bookID)
    {
        Book book = books.FirstOrDefault(b => b.BookID == bookID);
        if (book != null && !book.IsAvailable)
        {
            book.IsAvailable = true;
            Transaction transaction = transactions.FirstOrDefault(t => t.Lend_bookId == bookID);
            if (transaction != null)
            {
                transactions.Remove(transaction);
                Console.WriteLine("\nBook returned successfully.");
            }
        }
        else
        {
            Console.WriteLine("\nBook not found or already returned.");
        }
    }

    public void ViewLendingInformation()
    {
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"\nMember: {transaction.MemberID}, Book: {transaction.Lend_bookId}, Due Date: {transaction.DueDate}");
        }
    }

    public void DisplayOverdueBooks()
    {
        DateTime currentDate = DateTime.Now;
        foreach (var transaction in transactions)
        {
            if (transaction.DueDate < currentDate)
            {
                Console.WriteLine($"\nMember: {transaction.MemberID}, Book: {transaction.Lend_bookId}, Due Date: {transaction.DueDate}");
            }
        }
    }

    public decimal CalculateFine(DateTime dueDate, DateTime returnDate)
    {
        TimeSpan overduePeriod = returnDate - dueDate;
        if (overduePeriod.TotalDays <= 7)
        {
            return Convert.ToDecimal(50 * overduePeriod.TotalDays);
        }
        else if(overduePeriod.TotalDays >=7)
        {
            // Customize the fine calculation logic as needed
            return Convert.ToDecimal(50 * 7 + 100 * (overduePeriod.TotalDays - 7));
        }
        else
        {
            return 0;
        }
    }
}

class Lm_sytem
{
    public static void Main(string[] args)
    {
        Library library = new Library();
        Console.WriteLine("\n ---------- WELCOME! -----------\n Library Management System \n");

        while (true)
        {
            Console.WriteLine("\n CATALOGUE :- ");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Register a member");
            Console.WriteLine("3. Remove a book");
            Console.WriteLine("4. Remove a member");
            Console.WriteLine("5. Search for book informations");
            Console.WriteLine("6. Search for member informations");
            Console.WriteLine("7. Display book names in system");
            Console.WriteLine("8. Display member names in system");
            Console.WriteLine("9. Lending a book");
            Console.WriteLine("10. Returning a book");
            Console.WriteLine("11. View lending informations");
            Console.WriteLine("12. Display overdue books");
            Console.WriteLine("13. Fine Calculation");
            Console.WriteLine("14. Exit");
            Console.Write("\nEnter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\n  Adding a book  ");
                        Console.Write("\nEnter Book Id: ");
                        string bookID = Console.ReadLine();
                        Console.Write("\nEnter Title: ");
                        string title = Console.ReadLine();
                        Console.Write("\nEnter Author: ");
                        string author = Console.ReadLine();
                        library.AddBook(bookID, title, author);
                        break;
                    case 2:
                        Console.WriteLine("\n  Adding a member  ");
                        Console.Write("\nEnter Member ID: ");
                        string memberID = Console.ReadLine();
                        Console.Write("\nEnter Name: ");
                        string name = Console.ReadLine();
                        library.RegisterMember(memberID, name);
                        break;
                    case 3:
                        Console.WriteLine("\n  Removing a book  ");
                        Console.Write("\nEnter Book ID: ");
                        bookID = Console.ReadLine();
                        library.RemoveBook(bookID);
                        break;
                    case 4:
                        Console.WriteLine("\n  Removing a member  ");
                        Console.Write("\nEnter Member ID: ");
                        memberID = Console.ReadLine();
                        library.RemoveMember(memberID);
                        break;
                    case 5:
                        Console.WriteLine("\n  Search for book information");
                        Console.Write("\nEnter Book ID: ");
                        bookID = Console.ReadLine();
                        library.SearchBookInformation(bookID);
                        break;
                    case 6:
                        Console.WriteLine("\n  Search for member information");
                        Console.Write("\nEnter Member ID: ");
                        memberID = Console.ReadLine();
                        library.SearchMemberInformation(memberID);
                        break;
                    case 7:
                        Console.WriteLine("\n  Books in library System:-");
                        Console.WriteLine("\nBook Names:");
                        library.DisplayBookNames();
                        break;
                    case 8:
                        Console.WriteLine("\n  Search for member information");
                        Console.WriteLine("\nMember Names:");
                        library.DisplayMemberNames();
                        break;
                    case 9:
                        Console.WriteLine("\n  Search for member information");
                        Console.Write("\nEnter Book ID to lend: ");
                        bookID = Console.ReadLine();
                        Console.Write("\nEnter Member ID: ");
                        memberID = Console.ReadLine();
                        Console.Write("\nEnter number of days to return: ");
                        if (int.TryParse(Console.ReadLine(), out int daysToReturn))
                        {
                            library.LendBook(bookID, memberID, daysToReturn);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input for days to return.");
                        }
                        break;
                    case 10:
                        Console.Write("\nEnter Book ID to return: ");
                        bookID = Console.ReadLine();
                        library.ReturnBook(bookID);
                        break;
                    case 11:
                        Console.WriteLine("\n  Lending Information:");
                        library.ViewLendingInformation();
                        break;
                    case 12:
                        Console.WriteLine("\nOverdue Books:");
                        library.DisplayOverdueBooks();
                        break;
                    case 13:
                        Console.WriteLine("\n  Fine calculation");
                        Console.Write("\nEnter due date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                        {
                            Console.Write("\nEnter return date (yyyy-mm-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
                            {
                                decimal fine = library.CalculateFine(dueDate, returnDate);
                                Console.WriteLine($"\nFine: Rs. {fine}");
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid return date.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid due date.");
                        }
                        break;
                    case 14:
                        Console.WriteLine("\nExiting the Library Management System.");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter a valid option.");
                        break;
                }
            }
        }
    }
}

