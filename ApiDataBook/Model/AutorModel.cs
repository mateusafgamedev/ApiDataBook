using System.Text.Json.Serialization;

namespace ApiDataBook.Model
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [JsonIgnore]
        public ICollection<LivroModel> livros { get; set; }
    }
}
