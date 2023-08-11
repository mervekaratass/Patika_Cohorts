using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        //kişinin hangi kitabı getireceğinbi tutan id değerini bu değişkende tutucam
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            //bu id ye sahip bir kitap varmı diye baktım yoksa hata fırlatıyorum
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            //varsa oluşturduğum modele bulduğum kitapın değerlerini atıyorum 
            BookDetailViewModel vm= new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            vm.PageCount = book.PageCount;
            vm.Genre=((GenreEnum)book.GenreId).ToString();
            

            //ve modelimi geri döndürüyorum
            return vm;

        }
       
    }

    public class BookDetailViewModel
    {//kişinin kitabı getirirken görmesini istediğim değerler için bir view model oluşturuyorum
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}
