using FluentValidation;

namespace WebApi.CreateBook
{
    public class CreateBookCommandValidators:AbstractValidator<CreateBookCommand>
    {
        //Ben burda direk CreateBookModel i valide etmedim ben createbookcommandı valide ederek onun içindeki her bir nesneyi aslında 
        ///valide etmiş oluyorum.Çünkü belki benim orda birden vfazla modelim olabilir.Her bir model için derğil toplucana aslında erişim sağladım
        

        public CreateBookCommandValidators()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            //burda model içinden genreıdye eriştim ve sııfırdan büyük değerler alabilir dedim


            RuleFor(command=>command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            //tarihi şimdiki tarihden daha küçük olmalıdır yani gelecekteki bir tarihi diyemez

            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);


        }
    }
}
