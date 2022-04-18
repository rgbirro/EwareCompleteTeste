using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwareTeste.Model
{
    public class ModeloCaminhao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        [Display(Name ="Descrição Modelo")]
        public String Descricao { get; set; }       
        
        public virtual List<Caminhao> Caminhoes { get; set; }
    }
}
