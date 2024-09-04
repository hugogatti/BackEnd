using Microsoft.AspNetCore.Mvc;
using ControleInadimplentesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ControleInadimplentesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{idcliente}")]
        public async Task<ActionResult<ClienteModel>> GetCliente(int IDcliente)
        {
            var cliente = await _context.Clientes.FindAsync(IDcliente);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        [HttpPut("{idcliente}")]
        public async Task<IActionResult> PutCliente(int IDCliente, ClienteModel cliente)
        {
            if (IDCliente != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(IDCliente))
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteModel>> PostCliente(ClienteModel cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { idcliente = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{idcliente}")]
        public async Task<IActionResult> DeleteCliente(int IDCliente)
        {
            var cliente = await _context.Clientes.FindAsync(IDCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int IDCliente)
        {
            return _context.Clientes.Any(e => e.IdCliente == IDCliente);
        }
    }
}