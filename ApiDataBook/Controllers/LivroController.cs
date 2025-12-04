using ApiDataBook.Dto.Livro;
using ApiDataBook.Model;
using ApiDataBook.Services.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDataBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livroInterface.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var livro = await _livroInterface.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);
        }

        [HttpPost("CadastrarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> CadastrarLivro(CadastrarLivroDto livroDto)
        {
            var livro = await _livroInterface.CadastrarLivro(livroDto);
            return Ok(livro);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> EditarLivro(EditarLivroDto livroDto)
        {
            var livro = await _livroInterface.EditarLivro(livroDto);
            return Ok(livro);
        }

        [HttpDelete("DeletarLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> DeletarLivro(int idLivro)
        {
            var livro = await _livroInterface.DeletarLivro(idLivro);
            return Ok(livro);
        }
    }
}
