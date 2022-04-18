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
    public class DetailsModel : PageModel
    {
        private readonly EwareTeste.Model.EwareTesteContext _context;

        public DetailsModel(EwareTeste.Model.EwareTesteContext context)
        {
            _context = context;
        }

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
    }
}
