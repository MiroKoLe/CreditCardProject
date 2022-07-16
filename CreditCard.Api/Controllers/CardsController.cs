using CreditCard.Api.Data;
using CreditCard.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext _cardsDbContext;

        public CardsController(CardsDbContext cardsDbContext)
        {
            this._cardsDbContext = cardsDbContext;
        }
        // GET: All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardsDbContext.Cards.ToListAsync();
            return View(cards);
        }

        [HttpGet]
        [Route("id:guid")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(cards != null)
            {
                return Ok(cards);
            }

            return NotFound("Card not found");     
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await _cardsDbContext.Cards.AddAsync(card);
            await _cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), card.Id, card);
        }

        [HttpPost]
        public async Task<IActionResult> EditCard([FromRoute]Guid id, Card card)
        {
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                existingCard.CardNumber = card.CardNumber;
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CVC = card.CVC;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                await _cardsDbContext.SaveChangesAsync();

                return Ok(existingCard);

            }

            return NotFound("Card Not Found");
        }

    }
       
}
