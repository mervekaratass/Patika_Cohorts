using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.CreateBook
{
    public class CreateBookCommand
    {
        //kişinin yolluyacağı bilgileri bir model değişkenine atıcağım için bunun burada oluşturuyorum o modeldeki değrleri tutucak
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {//veritbanı bağlantımı oluşturuyorum
            _dbContext = dbContext;
        }

        public void Handle()
        {
            //daha önce bu isme ait kitap varsa hata fırlatıyorum
            var book =_dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            //yoksa yeni bir kitap nesnesi oluşturup modeldeki değerlerimi kitap nesnesime atıyorum
            book=new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            //kitapı ekle ve kaydet
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
           

        }
       
    }
    //kitapı eklerken kişinin girmesi için istedikleri bir model içinde oluşturuyorum
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
