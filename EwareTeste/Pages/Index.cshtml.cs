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
    public class IndexModel : PageModel
    {
        private readonly EwareTeste.Model.EwareTesteContext _context;

        public IndexModel(EwareTeste.Model.EwareTesteContext context)
        {
            _context = context;
        }

        public IList<Caminhao> Caminhao { get;set; }

        public async Task OnGetAsync()
        {
            Caminhao = await _context.Caminhoes.Include(x => x.Modelo).ToListAsync();
        }
    }
}
