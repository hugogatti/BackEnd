using Microsoft.AspNetCore.Mvc;
using ControleInadimplentesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleInadimplentesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobrancaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CobrancaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cobrancas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobrancaModel>>> GetCobrancas()
        {
            return await _context.Cobrancas.Include(c => c.Cliente).ToListAsync();
        }

        // GET: api/Cobrancas/5
        [HttpGet("{idcobranca}")]
        public async Task<ActionResult<CobrancaModel>> GetCobranca(int IDCobranca)
        {
            var cobranca = await _context.Cobrancas
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.IdCobranca == IDCobranca);

            if (cobranca == null)
            {
                return NotFound();
            }

            return cobranca;
        }

        // PUT: api/Cobrancas/5
        [HttpPut("{idcobranca}")]
        public async Task<IActionResult> PutCobranca(int IDCobranca, CobrancaModel cobranca)
        {
            if (IDCobranca != cobranca.IdCobranca)
            {
                return BadRequest();
            }

            _context.Entry(cobranca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CobrancaExists(IDCobranca))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cobrancas
        [HttpPost]
        public async Task<ActionResult<CobrancaModel>> PostCobranca(CobrancaModel cobranca)
        {
            _context.Cobrancas.Add(cobranca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCobranca", new { idcobranca = cobranca.IdCobranca }, cobranca);
        }

        // DELETE: api/Cobrancas/5
        [HttpDelete("{idcobranca}")]
        public async Task<IActionResult> DeleteCobranca(int IDCobranca)
        {
            var cobranca = await _context.Cobrancas.FindAsync(IDCobranca);
            if (cobranca == null)
            {
                return NotFound();
            }

            _context.Cobrancas.Remove(cobranca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CobrancaExists(int IDCobranca)
        {
            return _context.Cobrancas.Any(e => e.IdCobranca == IDCobranca);
        }
    }
}