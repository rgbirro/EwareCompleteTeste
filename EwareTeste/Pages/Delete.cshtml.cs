#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EwareTeste.Model;

namespace EwareTeste.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly EwareTeste.Model.EwareTesteContext _context;

        public DeleteModel(EwareTeste.Model.EwareTesteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Caminhao Caminhao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Caminhao = await _context.Caminhoes.FirstOrDefaultAsync(m => m.Id == id);

            if (Caminhao == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Caminhao = await _context.Caminhoes.FindAsync(id);

            if (Caminhao != null)
            {
                _context.Caminhoes.Remove(Caminhao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
