using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        //kişinin hangi kitabı güncelliyeceğini tutucak bir id değeri ve güncellemek istediği alanların değerleini tutucak model değişkenimi tanıımlıyorum
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            //bu id ye sahip bir kitap varmı diye bakıyorum eğer yoksa hata fırlatıyorum
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellenecek Kitap bulunamadı");

            //varsa güncellenmiş olan değerlerimi book nesneme atıyorum
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
           
            //değişiklikleri kaydet
            _dbContext.SaveChanges();
        

        }

        public class UpdateBookModel
        {//güncellenecek alanlar iççin oluşturduğum modelim
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
