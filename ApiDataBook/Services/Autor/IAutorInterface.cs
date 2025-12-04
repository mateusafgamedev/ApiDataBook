using ApiDataBook.Dto.Autor;
using ApiDataBook.Model;

namespace ApiDataBook.Services.Autor
{
    public interface IAutorInterface
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
        Task<ResponseModel<List<AutorModel>>> CadastrarDadosAutor(AutorCadastrarDto autorDto);
        Task<ResponseModel<List<AutorModel>>> EditarDadosAutor(AutorEditarDto autorEditarDto);
        Task<ResponseModel<AutorModel>> DeletarAutor(int idAutor);


    }
}
