using ApiDataBook.Data;
using ApiDataBook.Dto.Livro;
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
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

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

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int iAutor)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Autor.Id == iAutor)
                    .ToListAsync();
                if (livro == null)
                {
                    response.Mensagem = "ID do autor está incorreto ou autor não cadastrado!";
                    return response;
                }

                response.Dados = livro;
                response.Mensagem = "Dados do livro retornados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CadastrarLivro(CadastrarLivroDto livroDto)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroDto.Autor.Id);

                if(autor == null)
                {
                    response.Mensagem = "Autor não localizado ou não cadastrado!";
                    return response;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroDto.Titulo,
                    Autor = autor
                };

                _context.Livros.Add(livro); 
                await _context.SaveChangesAsync();

                response.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                response.Mensagem = "Livro cadastrado com sucesso!";
                return response;

            } catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<LivroModel>> DeletarLivro(int idLivro)
        {
            ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null) {
                    response.Mensagem = "ID do livro está incorreto ou livro não cadastrado";
                    return response;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                response.Mensagem = "Livro deletado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;

            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(EditarLivroDto livroDto)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroDto.Id);


                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroDto.Autor.Id);

                if (livro == null)
                {
                    response.Mensagem = "ID livro incorreto ou livor não está cadastrado!";
                    return response;
                }


                if(autor == null)
                {
                    response.Mensagem = "Autor não cadastrado!";
                    return response;
                }

                livro.Titulo = livroDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
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
                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
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
