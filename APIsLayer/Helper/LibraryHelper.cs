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
        Task Execute();
    }
    public  class LibraryHelper : ILibraryHelper
    {

        private readonly IAdminManager _adminManager;
        private readonly ISalesManager _salesManager;
        private readonly IRentalManager _rentalManager;
        private readonly ICustomerManager _customerManager; 
        private readonly IBookManager _bookManager; 
        public LibraryHelper(IAdminManager adminManager , ICustomerManager customerManager, IBookManager bookManager , ISalesManager salesManager , IRentalManager rentalManager )
        {
            _adminManager = adminManager;
            _bookManager = bookManager;
            _customerManager = customerManager;
            _salesManager = salesManager;
            _rentalManager = rentalManager;
        }

        public async Task Execute()
        {
            //Console.WriteLine("*****LOGIN*****\n");
            //Console.WriteLine("Please enter userName and password to Login  \n");
            //var loginModel = new LoginModel();
            //Console.Write("Username:");
            //loginModel.UserName = Console.ReadLine();
            //Console.Write("Password:");
            //loginModel.Password = Console.ReadLine();
            //var admin = _adminManager.login(loginModel);
            //if (admin is null)
            //{
            //    Console.WriteLine("Invalid username or password");
            //    Console.ReadKey();
            //}
            displayMenu();

        }

        public  void displayMenu()
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
        public  void selectChoice()
        {
            //int x = Convert.ToInt32(Console.ReadLine());
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
                       // process();
                        backtomenu();
                        break;
                    }
                case "4":
                    {
                        Console.Clear();
                        //returnbook();
                        backtomenu();
                        break;
                    }
                case "5":
                    {
                        Console.Clear();
                       // displaysales();
                        backtomenu();
                        break;
                    }
                case "6":
                    {
                        Console.Clear();
                      //  displayrentals();
                        backtomenu();
                        break;
                    }
                case "7":
                    {
                        //login();
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
            var book = new BookModel();
            Console.Write("ISBN:");
            book.Isbn = Console.ReadLine();
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
            var isCreated= _bookManager.Create(book);
            if (isCreated)
            {
                Console.WriteLine("Book Added successfully");
                backtomenu();
            }
            Console.WriteLine("Something Wrong");



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
            Console.Write("Quantity:");
            book.Quantity = Convert.ToInt32(Console.ReadLine());
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
                //Sales();
            }
            if (y == "2")
            {
                
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
                _bookManager.Delete(bookmodel.Id);
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
        //public void sales(BookModel book)
        //{
        //    Console.Clear();
        //    string nationalid = "";
        //    int quantity = 0;
        //    int bookquantity = 0;
        //    bool flag = false;
        //    double price = 0;
        //    Guid bookid = Guid.NewGuid();
        //    Guid customerid = Guid.NewGuid();
        //    string bookname = "";
        //    string customername = "";
        //    foreach (Book book in booklist)
        //    {

        //        if (book.Isbn == isbn)
        //        {
        //            Console.WriteLine($"\n\nBook ISBN:{book.Isbn}");
        //            Console.WriteLine($"Book Name:{book.Name}");
        //            Console.WriteLine($"Book Price:{book.Price}");
        //            Console.WriteLine($"Book Quantity:{book.Quantity}\n\n");
        //            price = book.Price;
        //            bookid = book.Id;
        //            bookname = book.Name;
        //            bookquantity = book.Quantity;
        //        }
        //    }

        //    Console.WriteLine("Please enter this information \n");
        //    while (true)
        //    {
        //        Console.Write("National ID to the Customer:");
        //        nationalid = Console.ReadLine();
        //        foreach (Customer customer in customerlist)
        //        {
        //            if (customer.Nationalid == nationalid)
        //            {
        //                flag = true;
        //            }

        //        }
        //        if (flag == false)
        //        {
        //            Console.WriteLine("this customer not found");
        //        }
        //        if (flag == true)
        //        {
        //            break;
        //        }
        //    }
        //    Console.WriteLine($"please enter quantity <= {bookquantity}");
        //    Console.Write("Quantity:");
        //    quantity = Convert.ToInt32(Console.ReadLine());
        //    while (true)
        //    {
        //        if (quantity > bookquantity)
        //        {
        //            Console.WriteLine($"please enter quantity <= {bookquantity}");
        //            Console.Write("Quantity:");
        //            quantity = Convert.ToInt32((Console.ReadLine()));
        //        }
        //        else
        //            break;
        //    }
        //    foreach (Customer customer in customerlist)
        //    {
        //        if (customer.Nationalid == nationalid)
        //        {
        //            customerid = customer.Id;
        //            customername = customer.Name;
        //        }
        //    }
        //    Console.WriteLine("\n\nOrder Sales:");
        //    Console.WriteLine($"Book Name:{bookname}");
        //    Console.WriteLine($"Customer Name:{customername}");
        //    Console.WriteLine($"Quantity:{quantity}");
        //    Console.WriteLine($"Price order sale:{quantity * price}");
        //    Sales sales = new Sales
        //    {
        //        Bookid = bookid,
        //        Customerid = customerid,
        //        Quantity = quantity,
        //        Price = quantity * price,
        //        Date = DateTime.Now.ToString("yyyy-MM-dd"),

        //    };
        //    string js = File.ReadAllText(@"C:\Users\TKmail\source\repos\book library\book library\sales.json");
        //    saleslist = JsonConvert.DeserializeObject<IList<Sales>>(js) != null ? JsonConvert.DeserializeObject<IList<Sales>>(js) : new List<Sales>();

        //    saleslist.Add(sales);

        //    var js1 = JsonConvert.SerializeObject(saleslist);
        //    File.WriteAllText(@"C:\Users\TKmail\source\repos\book library\book library\sales.json", js1);
        //    foreach (Book book1 in booklist)
        //    {
        //        if (book1.Isbn == isbn)
        //        {
        //            book1.Quantity = book1.Quantity - quantity;
        //        }
        //    }
        //    var j1 = JsonConvert.SerializeObject(booklist);
        //    File.WriteAllText(@"C:\Users\TKmail\source\repos\book library\book library\jsconfig1.json", j1);
        //    Console.WriteLine("The book has been successfully sale");
        //    Console.ReadKey();
        //    displayMenu();
        //    selectChoice();


        //}








    }
}
