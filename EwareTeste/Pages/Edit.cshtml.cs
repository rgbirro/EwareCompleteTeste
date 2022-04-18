#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EwareTeste.Model;

namespace EwareTeste.Pages
{
    public class EditModel : PageModel
    {
        private readonly EwareTeste.Model.EwareTesteContext _context;

        public EditModel(EwareTeste.Model.EwareTesteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Caminhao Caminhao { get; set; }
        [BindProperty]
        public List<ModeloCaminhao> ModelosCaminhao { get; set; }
        [BindProperty]
        public String ErroValidacaoForm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ModelosCaminhao = _context.ModelosCaminhoes.ToList();
            Caminhao = await _context.Caminhoes.FirstOrDefaultAsync(m => m.Id == id);

            if (Caminhao == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Caminhao).State = EntityState.Modified;

            try
            {
                if ((Caminhao.AnoModelo != DateTime.Now.Year && Caminhao.AnoModelo != DateTime.Now.Year + 1) ||
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

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaminhaoExists(Caminhao.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CaminhaoExists(int id)
        {
            return _context.Caminhoes.Any(e => e.Id == id);
        }
    }
}
