using ApiDataBook.Data;
using ApiDataBook.Dto.Autor;
using ApiDataBook.Model;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiDataBook.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if(autor == null)
                {
                    response.Mensagem = "Dados do autor não localizado ou não cadastrado!";
                    return response;
                }
                response.Dados = autor;
                response.Mensagem = "Dados do autor retornados com sucesso!";
                return response;

            } catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if(livro == null)
                {
                    response.Mensagem = "Dados do autor não localizado ou não cadastrado!";
                    return response;
                }

                response.Dados = livro.Autor;
                response.Mensagem = "Dados do autor retornados com sucesso!";
                return response;
            } catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CadastrarDadosAutor(AutorCadastrarDto autorDto)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorDto.Nome,
                    Sobrenome = autorDto.Sobrenome
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Autores.ToListAsync();
                response.Mensagem = "Autor cadastrado com sucesso!";
                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AutorModel>> DeletarAutor(int idAutor)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    response.Mensagem = "Autor não localizado ou não cadastrado!";
                    return response;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();

                response.Mensagem = "Autor deletado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarDadosAutor(AutorEditarDto autorEditarDto)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEditarDto.Id);

                if (autor == null)
                {
                    response.Mensagem = "Autor não localizado ou não cadastrado!";
                    return response;
                }

                autor.Nome = autorEditarDto.Nome;
                autor.Sobrenome = autorEditarDto.Sobrenome;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Autores.ToListAsync();
                response.Mensagem = "";
                return response;

            } catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _context.Autores.ToListAsync();

                response.Dados = autores;
                response.Mensagem = "Sucesso ao buscar informações no banco de dados!";
                return response;    

            } catch (Exception ex) 
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
