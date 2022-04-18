#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EwareTeste.Model;

namespace EwareTeste.Pages
{
    public class CreateModel : PageModel
    {
        private readonly EwareTeste.Model.EwareTesteContext _context;

        public CreateModel(EwareTeste.Model.EwareTesteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ModelosCaminhao = _context.ModelosCaminhoes.ToList();
            return Page();
        }

        [BindProperty]
        public Caminhao Caminhao { get; set; }
        [BindProperty]
        public List<ModeloCaminhao> ModelosCaminhao { get; set; }
        [BindProperty]
        public String ErroValidacaoForm { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {            
            if ((Caminhao.AnoModelo != DateTime.Now.Year && Caminhao.AnoModelo != DateTime.Now.Year+1) ||
                Caminhao.AnoFabricacao != DateTime.Now.Year || 
                Caminhao.ModeloId == 0 ||
                String.IsNullOrEmpty(Caminhao.Nome) ||
                String.IsNullOrEmpty(Caminhao.Descricao))
            {
                ErroValidacaoForm = "Caminhão com campos errados ou faltando algum campo obrigatório.";
                ModelosCaminhao = _context.ModelosCaminhoes.ToList();
                return Page();
            }
            String descricaoModeloCaminhao = _context.ModelosCaminhoes.Where(m => m.Id == Caminhao.ModeloId).FirstOrDefault().Descricao;
            if (!descricaoModeloCaminhao.Equals("FH") && !descricaoModeloCaminhao.Equals("FM"))
            {
                ErroValidacaoForm = "Modelo só pode ser FH ou FM";
                ModelosCaminhao = _context.ModelosCaminhoes.ToList();
                return Page();
            }
            
            _context.Caminhoes.Add(Caminhao);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
