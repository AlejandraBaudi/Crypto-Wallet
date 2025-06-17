using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet_Crypto.Context;
using Wallet_Crypto.Models;

namespace Wallet_Crypto.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }


        //Alta de compra/venta
        [HttpPost]
        public async Task<IActionResult> CrearTransaccion([FromBody] Transaction transaction)
        {

            //Validar cantidad de crypto a comprar o vender. 
            if (transaction.CryptoAmount <= 0)
                return BadRequest("La cantidad debe ser mayor a cero.");


            //Validar que el tipo de cryptomoneda y la accion no sean nulos. 
            if (string.IsNullOrEmpty(transaction.CryptoCode) || string.IsNullOrEmpty(transaction.Action))
                return BadRequest("Faltan datos obligatorios.");

            if (transaction.Action == "sale")
            {
                var totalComprado = _context.transactions
                    .Where(t => t.CryptoCode == transaction.CryptoCode && t.Action == "purchase")
                    .Sum(t => t.CryptoAmount);

                var totalVendido = _context.transactions
                    .Where(t => t.CryptoCode == transaction.CryptoCode && t.Action == "sale")
                    .Sum(t => t.CryptoAmount);

                var saldoDisponible = totalComprado - totalVendido;

                if (transaction.CryptoAmount > saldoDisponible)
                    return BadRequest("El saldo disponible no es suficiente para vender la cantidad seleccionada.");

            }


            //CriptoYa para obtener el precio actual. 
            string url = $"https://criptoya.com/api/satoshitango/{transaction.CryptoCode}/ars";
            using var http = new HttpClient();

            try
            {
                var response = await http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return StatusCode(502, "Error al consultar CriptoYa");

                var json = await response.Content.ReadAsStreamAsync();
                var datos = JsonSerializer.Deserialize<CryptoYaResponse>(json);


                //Elegir el precio segun si es compra o venta. 
                decimal precio = transaction.Action == "purchase" ? datos.TotalAsk : datos.TotalBid;

                //Calcular el monto en ARS y guardarlo en la BD. 
                transaction.Money = transaction.CryptoAmount * precio;

                _context.transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(ObtenerTransaccion), new { id = transaction.Id }, transaction);
            }
            catch
            {
                return StatusCode(500, "Error al conectar con CriptoYa");
            }
        }


        //Consulta de transaccion individual
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTransaccion(int id)
        {
            var transaccion = await _context.transactions.FindAsync(id);
            if (transaccion == null)
                return NotFound();

            return Ok(transaccion);
        }


        //EndPoint para mostrar todas las transacciones (historial)
        [HttpGet]
        public async Task<IActionResult> Historial()
        {
            var transacciones = await _context.transactions
                .OrderByDescending(t => t.DateTime)
                .ToListAsync();

            return Ok(transacciones);

        }


        //EndPoint para ver el estado de la billetera.
        [HttpGet("/wallet/status")]
        public async Task<IActionResult> ObtenerEstadoActual()
        {
            var transacciones = await _context.transactions.ToListAsync();

            var saldo = transacciones
                .GroupBy(t => t.CryptoCode)
                .Select(g => new
                {
                    CryptoCode = g.Key,
                    Cantidad = g.Where(t => t.Action == "purchase").Sum(t => t.CryptoAmount)
                    - g.Where(t => t.Action == "sale").Sum(t => t.CryptoAmount)
                })
                .Where(x => x.Cantidad > 0)
                .ToList();

            var resultados = new List<object>();
            decimal totalGeneral = 0;

            using var http = new HttpClient();

            foreach (var item in saldo)
            {
                string url = $"https://criptoya.com/api/satoshitango/{item.CryptoCode}/ars";
                try
                {
                    var response = await http.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                        continue;

                    var json = await response.Content.ReadAsStringAsync();
                    var datos = JsonSerializer.Deserialize<CryptoYaResponse>(json);

                    decimal precioActual = datos.Bid;
                    decimal valorEnPesos = item.Cantidad * precioActual;
                    totalGeneral += valorEnPesos;

                    resultados.Add(new
                    {
                        cryptoCode = item.CryptoCode,
                        cantidad = Math.Round(item.Cantidad, 8),
                        valorEnpesos = Math.Round(valorEnPesos, 2)
                    });
                }
                catch
                {
                    continue;
                }

            }
            return Ok(new
            {
                resumen = resultados,
                totalEnPesos = Math.Round(totalGeneral, 2)
            });

        }


        //Editar una transaccion
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditarTransaccion(int id, [FromBody] JsonElement cambios)
        {
            var transaccion = await _context.transactions.FindAsync(id);
            if (transaccion == null)
                return NotFound();

            if(cambios.TryGetProperty("money", out var nuevoMonto))
            {
                transaccion.Money = nuevoMonto.GetDecimal();
            }

            if(cambios.TryGetProperty("datetime", out var nuevaFecha))
            {
                transaccion.DateTime = DateTime.Parse(nuevaFecha.GetString());
            }

            await _context.SaveChangesAsync();
            return Ok(transaccion);
        }


        //Eliminar una transaccion
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTransaccion(int id)
        {
            var transaccion = await _context.transactions.FindAsync(id);
            if (transaccion == null)
                return NotFound();

            _context.transactions.Remove(transaccion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
