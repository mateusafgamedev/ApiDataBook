using ApiDataBook.Dto.Livro;
using ApiDataBook.Model;

namespace ApiDataBook.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
        Task<ResponseModel<List<LivroModel>>> CadastrarLivro(CadastrarLivroDto livroDto);
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int iAutor);
        Task<ResponseModel<List<LivroModel>>> EditarLivro(EditarLivroDto livroDto);
        Task<ResponseModel<LivroModel>> DeletarLivro(int idLivro);
    }
}
