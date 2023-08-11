using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        //kişinin yolluyacağı id yi tutucağım değişkeni oluşturdum
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handel()
        {
            //bu id ye sahip bir değişken varmı yokmu diye kontrol ettim yoksa hata fırlattım
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı");

            //zaten varsa sil ve işlemleri kaydet
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
          
        }
    }
}
