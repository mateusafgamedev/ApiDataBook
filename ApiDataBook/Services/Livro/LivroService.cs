using ApiDataBook.Data;
using ApiDataBook.Model;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApiDataBook.Services.Livro
{
    public class LivroService : ILivroInterface
    {

        private readonly AppDbContext _context;
        public LivroService(AppDbContext context) {
            _context = context;        
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    response.Mensagem = "ID invalido ou livro não cadastrado!";
                    return response;
                }

                response.Dados = livro;
                response.Mensagem = "Sucesso ao buscar informações no banco de dados!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.ToListAsync();
                response.Dados = livros;
                response.Mensagem = "Sucesso ao buscar informações no banco de dados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
