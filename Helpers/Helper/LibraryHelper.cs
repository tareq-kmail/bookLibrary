using Domain.Manager;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Helper
{
    public interface ILibraryHelper
    {
        void Execute();
    }
    public class LibraryHelper : ILibraryHelper
    {

        private readonly IAdminManager _adminManager;
        private readonly ISalesManager _salesManager;
        private readonly IRentalManager _rentalManager;
        private readonly ICustomerManager _customerManager;
        private readonly IBookManager _bookManager;
        public LibraryHelper(IAdminManager adminManager, ICustomerManager customerManager, IBookManager bookManager, ISalesManager salesManager, IRentalManager rentalManager)
        {
            _adminManager = adminManager;
            _bookManager = bookManager;
            _customerManager = customerManager;
            _salesManager = salesManager;
            _rentalManager = rentalManager;
        }

        public void Execute()
        {
            login();
            //var admin = new AdminModel
            //{
            //    Name = "Tareq",
            //    Nationalid = "22",
            //    Address = "Jenin",
            //    Username = "tareq22",
            //    Password = "12345",
            //    Phone = "0566699987"

            //};
            //_adminManager.Create(admin);


        }
        public void login()
        {
            Console.Clear();
            Console.WriteLine("*****LOGIN*****\n");
            Console.WriteLine("Please enter userName and password to Login  \n");
            var loginModel = new LoginModel();
            Console.Write("Username:");
            loginModel.UserName = Console.ReadLine();
            Console.Write("Password:");
            loginModel.Password = Console.ReadLine();
            var admin = _adminManager.login(loginModel);
            if (admin is null)
            {
                Console.WriteLine("Invalid username or password");
                Console.ReadKey();
            }
            displayMenu();
        }

        public void displayMenu()
        {
            Console.Clear();
            Console.WriteLine("0:Add new Book");
            Console.WriteLine("1:Add new Customer");
            Console.WriteLine("2:Search for abook");
            Console.WriteLine("3:Process on Books");
            Console.WriteLine("4:Return Book");
            Console.WriteLine("5:Display Books Sales");
            Console.WriteLine("6:Books Rentals Display");
            Console.WriteLine("7:Logout\n");
            Console.Write("please enter number from this menu:");
            selectChoice();
        }
        public void selectChoice()
        {
            
            string x = Console.ReadLine();
            switch (x)
            {
                case "0":
                    {
                        addBook();
                        break;
                    }
                case "1":
                    {
                        addcustomer();
                        break;
                    }
                case "2":
                    {
                        search();
                        break;
                    }
                case "3":
                    {
                        Console.Clear();
                        process();
                        backtomenu();
                        break;
                    }
                case "4":
                    {
                        Console.Clear();
                        ReturnBook();
                        backtomenu();
                        break;
                    }
                case "5":
                    {
                        Console.Clear();
                        DisplaySales();
                        backtomenu();
                        break;
                    }
                case "6":
                    {
                        Console.Clear();
                        DisplayRentals();
                        backtomenu();
                        break;
                    }
                case "7":
                    {
                        login();
                        break;
                    }
                default:
                    {
                        displayMenu();
                        selectChoice();
                        break;
                    }
            }

        }
        public void backtomenu()
        {
            Console.WriteLine("press any key to continue");
            Console.ReadKey();

            displayMenu();
            selectChoice();
        }
        public void addBook()
        {
            Console.Clear();
            var booklist = _bookManager.GetAll();
            var book = new BookModel();
            bool flag = false;
            Console.Write("ISBN:");
            book.Isbn = Console.ReadLine();
            foreach (var book1 in booklist)
            {
                if (book1.Isbn.Equals(book.Isbn))
                {
                    Console.Write("Please enter Quantity > 0:");
                    book.Quantity = Convert.ToInt32(Console.ReadLine());
                    while (true)
                    {
                        if (book.Quantity <= 0)
                        {
                            Console.Write("please enter Quantity > 0:");
                            book.Quantity = Convert.ToInt32((Console.ReadLine()));
                        }
                        else
                            break;
                    }
                    book1.Quantity += book.Quantity;
                    _bookManager.Update(book1);
                    flag = true;
                }
            }
            if (flag)
            {
                Console.WriteLine("Book Added successfully");
                backtomenu();
            }
            if (flag == false)
            {
                Console.Write("Book_Name:");
                book.Name = Console.ReadLine();
                Console.Write("Book_Price:");
                book.Price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Please enter Quantity > 0:");
                book.Quantity = Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if (book.Quantity <= 0)
                    {
                        Console.Write("please enter Quantity > 0:");
                        book.Quantity = Convert.ToInt32((Console.ReadLine()));
                    }
                    else
                        break;
                }

                var isCreated = _bookManager.Create(book);
                if (isCreated)
                {
                    Console.WriteLine("Book Added successfully");
                    backtomenu();
                }
                Console.WriteLine("Something Wrong");
                backtomenu();
            }

        }
        public void addcustomer()
        {
            Console.Clear();
            CustomerModel customer = new CustomerModel();
            Console.Write("National_ID:");
            customer.Nationalid = Console.ReadLine();
            Console.Write("Name:");
            customer.Name = Console.ReadLine();
            Console.Write("Phone:");
            customer.Phone = Console.ReadLine();
            Console.Write("City:");
            customer.City = Console.ReadLine();
            var isCreated = _customerManager.Create(customer);
            if (isCreated)
            {
                Console.WriteLine("Customer Added successfully");
                backtomenu();
            }
            Console.WriteLine("Something Wrong");
        }
        public void Update(BookModel book)
        {
            Console.Clear();
            Console.WriteLine($"\n\nBook_Isbn:{book.Isbn}");
            Console.WriteLine($"Book_Name:{book.Name}");
            Console.WriteLine($"Book_Price:{book.Price}");
            Console.WriteLine($"Quantity:{book.Quantity}\n\n\nPlease enter updates on this book\n");
            Console.Write("Name:");
            book.Name = Console.ReadLine();
            Console.Write("Price:");
            book.Price = Convert.ToDouble(Console.ReadLine());
            var isUpdated = _bookManager.Update(book);
            if (isUpdated)
            {
                Console.WriteLine("updated successfully");
                backtomenu();
            }

        }
        public void process()
        {
            Console.Clear();
            var booklist = _bookManager.GetAll();
            foreach (BookModel b in booklist)
            {
                Console.WriteLine($"Book ISBN:{b.Isbn}       Book Name:{b.Name}       Book Price:{b.Price}       Book Quantity:{b.Quantity}");
            }

            Console.WriteLine("\nPlease enter the ISBN OR Name you want to search for\n");
            Console.Write("ISBN OR NAME:");
            string bookname = Console.ReadLine();
            bool flag = false;
            string y = "";
            var bookmodel = new BookModel();
            foreach (BookModel book in booklist)
            {
                if (book.Isbn == bookname || book.Name == bookname)
                {
                    Console.WriteLine($"\n\nISBN:{book.Isbn}");
                    Console.WriteLine($"Book_Name:{book.Name}");
                    Console.WriteLine($"Book_Price:{book.Price}");
                    Console.WriteLine($"Quantity:{book.Quantity}\n\n");
                    bookmodel.Id = book.Id;
                    bookmodel.Isbn = book.Isbn;
                    bookmodel.Name = book.Name;
                    bookmodel.Price = book.Price;
                    bookmodel.Quantity = book.Quantity;
                    flag = true;
                }
            }
            if (flag == true)
            {
                Console.WriteLine("1-Sale book          2-Rental book          3-Back on Menu\n\n");
                Console.Write("please enter number from choices:");
                y = Console.ReadLine();
            }
            if (flag == false)
            {
                Console.WriteLine("\nthis book not found");
                backtomenu();

            }
            if (y == "1")
            {
                Sales(bookmodel);
            }
            if (y == "2")
            {
                Rental(bookmodel);
            }
            if (y == "3")
            {
                displayMenu();
                selectChoice();
            }
            if (y != "1" && y != "2" && y != "3")
            {
                displayMenu();
                selectChoice();
            }
        }

        public void search()
        {
            Console.Clear();
            var booklist = _bookManager.GetAll();
            foreach (BookModel b in booklist)
            {
                Console.WriteLine($"Book ISBN:{b.Isbn}       Book Name:{b.Name}       Book Price:{b.Price}       Book Quantity:{b.Quantity}");
            }

            Console.WriteLine("\nPlease enter the ISBN OR Name you want to search for\n");
            Console.Write("ISBN OR NAME:");
            string bookname = Console.ReadLine();
            bool flag = false;
            string y = "";
            var bookmodel = new BookModel();
            foreach (BookModel book in booklist)
            {
                if (book.Isbn == bookname || book.Name == bookname)
                {
                    Console.WriteLine($"\n\nISBN:{book.Isbn}");
                    Console.WriteLine($"Book_Name:{book.Name}");
                    Console.WriteLine($"Book_Price:{book.Price}");
                    Console.WriteLine($"Quantity:{book.Quantity}\n\n");
                    bookmodel.Id = book.Id;
                    bookmodel.Isbn = book.Isbn;
                    bookmodel.Name = book.Name;
                    bookmodel.Price = book.Price;
                    bookmodel.Quantity = book.Quantity;
                    flag = true;
                }

            }
            if (flag == true)
            {
                Console.WriteLine("1-Update           2-Delete          3-Back on Menu\n\n");
                Console.Write("please enter number from choices:");
                y = Console.ReadLine();
            }
            if (flag == false)
            {
                Console.WriteLine("\nthis book not found");
                backtomenu();

            }
            if (y == "1")
            {
                Update(bookmodel);
            }
            if (y == "2")
            {
                bookmodel.Quantity = 0;
                _bookManager.Update(bookmodel);
                Console.WriteLine($"Deleted book {bookmodel.Name} successfully");
                backtomenu();

            }
            if (y == "3")
            {
                displayMenu();
                selectChoice();
            }
            if (y != "1" && y != "2" && y != "3")
            {
                displayMenu();
                selectChoice();
            }

        }

        public void Sales(BookModel bookmodel)
        {
            if (bookmodel.Quantity > 0)
            {
                Console.WriteLine("Please enter this information \n");

                Console.Write("National ID to the Customer:");
                var nationalid = Console.ReadLine();
                var customer = _customerManager.GetByNationalId(nationalid);
                if (customer is null)
                {
                    Console.WriteLine("this customer not found");
                    backtomenu();
                }
                Console.WriteLine($"please enter quantity <= {bookmodel.Quantity}");
                Console.Write("Quantity:");
                var quantity = Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if (quantity > bookmodel.Quantity)
                    {
                        Console.WriteLine($"please enter quantity <= {bookmodel.Quantity}");
                        Console.Write("Quantity:");
                        quantity = Convert.ToInt32((Console.ReadLine()));
                    }
                    else
                        break;
                }

                var sales = new SalesModel
                {
                    Quantity = quantity,
                    Price = quantity * bookmodel.Price,
                    Date = DateTime.Now.ToString("yyyy-MM-dd")
                };
                sales.BookId = bookmodel.Id;
                sales.CustomerId = customer.Id;
                Console.WriteLine("\n\nOrder Sales:");
                Console.WriteLine($"Book Name:{bookmodel.Name}");
                Console.WriteLine($"Customer Name:{customer.Name}");
                Console.WriteLine($"Quantity:{quantity}");
                Console.WriteLine($"Price order sale:{sales.Price}");
                var isAdded = _salesManager.Create(sales);
                if (isAdded)
                {
                    bookmodel.Quantity -= quantity;
                    _bookManager.Update(bookmodel);
                    Console.WriteLine("The book has been successfully sale");
                }
            }
            else
            {
                Console.WriteLine("this book not found now");
            }
            backtomenu();


        }

        public void Rental(BookModel bookmodel)
        {
            if (bookmodel.Quantity > 0)
            {
                Console.Clear();
            bool flag2 = false;
            Console.WriteLine($"\n\nBook ISBN:{bookmodel.Isbn}");
            Console.WriteLine($"Book Name:{bookmodel.Name}");
            Console.WriteLine($"Book sale Price :{bookmodel.Price}");
            Console.WriteLine("Book rental Price:15 ");
            Console.WriteLine($"Book Quantity:{bookmodel.Quantity}\n\n");
            Console.WriteLine("Please enter National ID to the Customer  \n");
            Console.Write("National ID:");
            var nationalid = Console.ReadLine();
            var customer = _customerManager.GetByNationalId(nationalid);
            if (customer is null)
            {
                Console.WriteLine("this customer not found");
            }
            
                var rental = new RentalModel
                {
                    BookId = bookmodel.Id,
                    CustomerId = customer.Id,
                    Price = 15,
                    BookingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    BookingExpiryDate = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd")
                };
                var rentallist = _rentalManager.GetAll();

                foreach (var rental1 in rentallist)
                {
                    if (rental1.BookId == bookmodel.Id && rental1.CustomerId == customer.Id)
                    {
                        flag2 = true;
                    }
                }
                if (flag2 == true)
                {
                    Console.WriteLine($"this customer {customer.Name} rental this book {bookmodel.Name} before");

                }
                if (flag2 == false)
                {
                    Console.WriteLine("\n\nOrder Rental:");
                    Console.WriteLine($"Book Name:{bookmodel.Name}");
                    Console.WriteLine($"Customer Name:{customer.Name}");
                    Console.WriteLine("Price Rental Book:15");
                    Console.WriteLine($"book return date:{DateTime.Now.AddDays(14).ToString("yyyy-MM-dd")}");
                    var isAdded = _rentalManager.Create(rental);
                    if (isAdded)
                    {
                        bookmodel.Quantity -= 1;
                        _bookManager.Update(bookmodel);
                        Console.WriteLine("The book has been successfully rental");
                    }

                }
            }
            else
            {
                Console.WriteLine("this book not found now");
            }
            backtomenu();
            }
        public void ReturnBook()
        {
            Console.Clear();
            Console.Write("Book ISBN:");
            string isbn = Console.ReadLine();
            Console.Write("NationalID to the Customer:");
            string nationalid = Console.ReadLine();
            var bookToUpdate = _bookManager.GetByIsbn(isbn);
            var customer = _customerManager.GetByNationalId(nationalid);
            var rentallist = _rentalManager.GetAll();
            if(bookToUpdate is null)
            {
                Console.WriteLine("this Book not found");
            }
            if (customer is null)
            {
                Console.WriteLine("this customer not registered");
            }
            if (customer != null && bookToUpdate !=null)
            {
                RentalModel rental = rentallist.FirstOrDefault(e => e.BookId == bookToUpdate.Id && e.CustomerId == customer.Id);
                var isDeleted = _rentalManager.Delete(rental.Id);
                if (isDeleted)
                {
                    bookToUpdate.Quantity += 1;
                    _bookManager.Update(bookToUpdate);
                    Console.WriteLine("The book return has been successfully ");
                }
                else
                {
                    Console.WriteLine($"this customer {customer.Name} not rental this book {bookToUpdate.Name}");
                }

                Console.ReadKey();
                displayMenu();
                selectChoice();

            }

        }
        public void DisplaySales()
        {
            Console.Clear();
            var saleslist = _salesManager.GetAll();
            if (saleslist.Count > 0)
            {
                foreach (var sales in saleslist)
                {
                    Console.Write($"Book ISBN:{sales.Book.Isbn}     Book Name:{sales.Book.Name} ");
                    Console.Write($"Customer NationalID:{sales.Customer.Nationalid}     Customer Name:{sales.Customer.Name}     Quantity:{sales.Quantity}     ");
                    Console.Write($"Price:{sales.Price}     Date:{sales.Date}\n\n");
                }
            }
            else
            {
                Console.WriteLine("No Books Sold");

            }

        }
        public void DisplayRentals()
        {
            Console.Clear();
            var rentalList = _rentalManager.GetAll();

            if (rentalList.Count > 0)
            {
                foreach (var rental in rentalList)
                {
                    Console.Write($"Book ISBN:{rental.Book.Isbn}     Book Name:{rental.Book.Name}     ");
                    Console.Write($"Customer NationalID:{rental.Customer.Nationalid}     Customer Name:{rental.Customer.Name}     Price:{rental.Price}     ");
                    Console.Write($"Booking Date:{rental.BookingDate}     Booking Expiry Date:{rental.BookingExpiryDate}\n\n");
                }
            }
            if (rentalList.Count == 0)
            {
                Console.WriteLine("Not found Books Rentaled");

            }
        }

    }
}
