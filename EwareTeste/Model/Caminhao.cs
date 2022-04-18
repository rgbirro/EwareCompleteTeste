using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwareTeste.Model
{
    public class Caminhao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required (ErrorMessage ="Nome obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Descrição obrigatória")]
        public string Descricao { get; set; }                
        public virtual ModeloCaminhao? Modelo { get; set; }
        [Required(ErrorMessage ="Modelo obrigatório")]
        public int ModeloId { get; set; }        
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }        
    }
}
