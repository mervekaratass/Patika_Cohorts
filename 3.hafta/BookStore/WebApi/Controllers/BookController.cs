using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApi.CreateBook;
using WebApi.DbOperations;
using WebApi.DeleteBook;
using WebApi.GetBookDetail;
using WebApi.GetBooks;
using WebApi.UpdateBook;
using static WebApi.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
		readonly BookStoreDbContext _context;
        //readonly değişkenler uygulama içerisinden değiştirilemezler sadece constructorda set edilebilrler
		public BookController(BookStoreDbContext context)
		{
			_context = context;
		}

		#region Context kullanmadan önce liste ile 
		//burda static yapmasaydım her actionda istek geldidğindde controllera gelip baştan bir nesne oluşturucaktı ve o zman ekledikerlimi bile t
		//tekrar get yapsam göremiycektim o yüzden ststaic ile program kapatılana kdar korudum
		//private static List<Book> BookList = new List<Book>()
		//{
		//    new Book()
		//    {
		//        Id = 1,
		//        Title="Learn Startup",
		//        GenreId=1,//Personal Growth
		//        PageCount=200,
		//        PublishDate=new DateTime(2001,06,12)
		//    },

		//    new Book()
		//    {
		//        Id = 2,
		//        Title="Hearland",
		//        GenreId=2,//Science Fiction
		//        PageCount=250,
		//        PublishDate=new DateTime(2010,05,23)
		//    },
		//    new Book()
		//    {
		//        Id = 3,
		//        Title="Dune",
		//        GenreId=2,//Science Fiction
		//        PageCount=540,
		//        PublishDate=new DateTime(2001,12,21)
		//    },

		//}; 
		#endregion


		[HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
        }



        //id ile yaparken genelde route ile yaparız yani bunu
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {  //hatalrı try blığu ile yakalayıp catchte gösteriyorum
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                 result = query.Handle();
               

            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }

            return Ok(result);

        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context);
            try
            { //throwla hata fırlattığım yeri try catchele yakalmam için buraya yazdım yoksa throw
                //gelirse aslında kodum kırılır
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
       

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();


        }

        [HttpDelete("{id}")]
        public IActionResult deleteBook(int id) 
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handel();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
