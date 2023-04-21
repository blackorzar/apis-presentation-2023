using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrudSample.Models;

namespace CrudSample.Pages.Productos
{
    public class CreateModel : PageModel
    {
        private readonly CrudSample.Models.McsVecinosContext _context;

        public CreateModel(CrudSample.Models.McsVecinosContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Producto Producto { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Productos == null || Producto == null)
            {
                return Page();
            }

            _context.Productos.Add(Producto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
