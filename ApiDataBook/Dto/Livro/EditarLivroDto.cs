using ApiDataBook.Dto.Livro.Vinculo;
using ApiDataBook.Model;

namespace ApiDataBook.Dto.Livro
{
    public class EditarLivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
