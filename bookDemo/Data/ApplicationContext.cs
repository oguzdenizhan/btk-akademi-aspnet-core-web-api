using bookDemo.Controllers;
using bookDemo.Models;

namespace bookDemo.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books {get;set;}
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book(){Id=1,Title="Ayrılan Kalpler",Price=75},
                new Book(){Id=2,Title="Fahrenheit 451",Price=150},
                new Book(){Id=3,Title="Dünyanın Merkezine Seyahat",Price=75},

            };
            
        }
    }
}
