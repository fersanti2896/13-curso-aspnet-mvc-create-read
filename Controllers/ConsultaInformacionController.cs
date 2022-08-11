using Dominio.IServicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Transporte;

namespace Pedidos.Controllers {
    public class ConsultaInformacionController : Controller {
        private readonly IConsultaPedidoService _IConsultaPedidoService;

        public ConsultaInformacionController(IConsultaPedidoService iConsultaPedidoService) {
            _IConsultaPedidoService = iConsultaPedidoService;
        }

        [HttpPost]
        public async Task<ActionResult> ConsultaPedidosGeneral(ModeloConsultaDTO modelo) {
            /* Se deberá tomar de JWT */
            modelo.CatPerfilId = 2;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var lista = await _IConsultaPedidoService.ConsultaPedidosGeneral(modelo);

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            return PartialView(lista);
        }

        [HttpPost]
        public async Task<JsonResult> ConsultaEstadosPedidos() {
            var lista = await _IConsultaPedidoService.ConsultaEstadosPedidos();

            return Json(lista); 
        }

        [HttpPost]
        public IActionResult EliminarPedido(PedidoDTO modelo) {
            var resultado = _IConsultaPedidoService.EliminarPedido(modelo.PedidoId);

            return resultado ? Ok() : StatusCode(500, "Ocurrió un error");
        }

        [HttpPost]
        public IActionResult ActualizarEstadoPedido(PedidoDTO modelo) {
            var resultado = _IConsultaPedidoService.ActualizarEstadoPedido(modelo);

            return resultado ? Ok() : StatusCode(500, "Ocurrió un error");
        }
    }
}
