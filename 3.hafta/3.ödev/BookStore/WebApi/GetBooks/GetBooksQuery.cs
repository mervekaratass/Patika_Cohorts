using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext) 
        { //veritabanı ile bağlantımı oluşturucağım nesne
        
        _dbContext = dbContext;
        }


        public List<BooksViewModel> Handle()
        {// kitaplarımı oluşturduğum modele foreach döngüsü ile ekliyorum
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    Publishdate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount = book.PageCount,

                }) ;
            }
            //modeli geri döndürüyorum
            return vm;
        }


        public class BooksViewModel
        {//kişinin gelen kitaplarda görmesini isteediğim değrler için bir view model oluşturuyorum.
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string Publishdate { get; set; }
            public string Genre { get; set; }
        }
    }
}
