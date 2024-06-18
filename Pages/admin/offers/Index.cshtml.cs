using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;

namespace MVCRealEstate.Pages.admin.offers
{
    public class IndexModel : PageModel
    {
        private readonly MVCRealEstateContext _context;

        public IndexModel(MVCRealEstateContext context)
        {
            _context = context;
        }

        public IList<Models.Offer> Offers { get; set; }

        public async Task OnGetAsync()
        {
            Offers = await _context.Offer
                .Include(o => o.Location)
                .Include(o => o.Agency)
                .Include(o => o.OwnerInfo)
                .ToListAsync();
        }
    }
}
