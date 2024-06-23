using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Data;
using MVCRealEstate.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCRealEstate.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OffersApiController : ControllerBase
	{
		private readonly ILogger<OffersApiController> _logger;
		private readonly MVCRealEstateContext _context;

		public OffersApiController(ILogger<OffersApiController> logger, MVCRealEstateContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("latest")]
		public async Task<ActionResult<IEnumerable<Offer>>> GetLatestOffers()
		{

			var offers = await _context.Offer
				.Include(o => o.Agency)
				.Include(o => o.Location)
				.Include(o => o.OwnerInfo)
				.OrderByDescending(x => x.CreatedAt)
				.Take(3)
				.ToListAsync();

			foreach (var offer in offers)
			{
				offer.OfferMedias = await _context.OfferMedia
					.Where(m => offer.OfferMediaId.Contains(m.OfferMediaId))
					.ToListAsync();
			}

			return Ok(offers);
		}
	}
}
