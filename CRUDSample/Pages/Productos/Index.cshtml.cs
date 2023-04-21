using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrudSample.Models;

namespace CrudSample.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly CrudSample.Models.McsVecinosContext _context;

        public IndexModel(CrudSample.Models.McsVecinosContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Productos != null)
            {
                Producto = await _context.Productos.ToListAsync();
            }
        }
    }
}
