using AutoMapper;
using WebApi.CreateBook;
using WebApi.GetBookDetail;
using static WebApi.GetBooks.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile:Profile
    {//auto mapperlarımı constructor içinde oluşturuyorum
        public MappingProfile() {

            //CreatebookModelımı booka mapledim --->CretaeBookCommand içinde kuulanıyorum           
            CreateMap<CreateBookModel, Book>();

            //Book nesnesini BookDetailViewModel 'a mapledim ----->GetBookDetailQuery içinde kullanıyorum
            //ben burda farklı olarak ihtiyaç duyucağım bazı satırları nasıl değiştiriceeğini söylemek istiyorrum kongig edicem.Bunun sebebi
            //vm.Genre=((GenreEnum)book.GenreId).ToString();  ben her seferinde bu dönüşümü yapmak yerine bu dönüşümü burda sağlıyorum
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));


            //Book nesnesini BooksViewModel 'a mapledim ----->GetBooksQuery içinde kullanıyorum
            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        
        }
    }
}
