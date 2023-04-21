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
    public class DeleteModel : PageModel
    {
        private readonly CrudSample.Models.McsVecinosContext _context;

        public DeleteModel(CrudSample.Models.McsVecinosContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Producto Producto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
            {
                return NotFound();
            }
            else 
            {
                Producto = producto;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null)
            {
                Producto = producto;
                _context.Productos.Remove(Producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
