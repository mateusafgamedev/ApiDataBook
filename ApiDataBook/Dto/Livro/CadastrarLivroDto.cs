using ApiDataBook.Dto.Livro.Vinculo;
using ApiDataBook.Model;

namespace ApiDataBook.Dto.Livro
{
    public class CadastrarLivroDto
    {
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
