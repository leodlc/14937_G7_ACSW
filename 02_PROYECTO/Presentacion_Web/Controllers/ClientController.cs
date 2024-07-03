using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Data;
using Presentacion_Web.Service;
using Presentacion_Web.Utils;

namespace Presentacion_Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ActionResult> Index()
        {
            var clientes = await _clientService.GetClientesAsync();
            return View(clientes);
        }

        public ActionResult Create()
        {
            return PartialView("_CrearClienteForm", new CLIENTE());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CLIENTE cliente)
        {
            //try
            //{
            //    Validations.VerificaIdentificacion(cliente.CEDULACLIENTE);
            //    Validations.ValidaNombre(cliente.NOMBRECLIENTE);
            //    Validations.ValidaApellido(cliente.APELLIDOCLIENTE);
            //    Validations.ValidaTelefono(cliente.TELEFONOCLIENTE);
            //    Validations.ValidaDireccion(cliente.DIRECCLIENTE);
            //    Validations.ValidaFecha(cliente.FECHANACCLIENTE ?? DateTime.Now);
            //    Validations.VerificaInputStr(cliente.CEDULACLIENTE);
            //    Validations.VerificaInputStr(cliente.NOMBRECLIENTE);
            //    Validations.VerificaInputStr(cliente.APELLIDOCLIENTE);
            //    Validations.VerificaInputStr(cliente.DIRECCLIENTE);
            //    Validations.VerificaInputStr(cliente.TELEFONOCLIENTE);
            //}
            //catch (ArgumentException ex)
            //{
            //    ModelState.AddModelError("", ex.Message);
            //}

            if (ModelState.IsValid)
            {
                await _clientService.CreateClienteAsync(cliente);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearClienteForm", cliente);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var cliente = await _clientService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CrearClienteForm", cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CLIENTE cliente)
        {
            //try
            //{
            //    Validations.VerificaIdentificacion(cliente.CEDULACLIENTE);
            //    Validations.ValidaNombre(cliente.NOMBRECLIENTE);
            //    Validations.ValidaApellido(cliente.APELLIDOCLIENTE);
            //    Validations.ValidaTelefono(cliente.TELEFONOCLIENTE);
            //    Validations.ValidaDireccion(cliente.DIRECCLIENTE);
            //    Validations.ValidaFecha(cliente.FECHANACCLIENTE ?? DateTime.Now);
            //    Validations.VerificaInputStr(cliente.CEDULACLIENTE);
            //    Validations.VerificaInputStr(cliente.NOMBRECLIENTE);
            //    Validations.VerificaInputStr(cliente.APELLIDOCLIENTE);
            //    Validations.VerificaInputStr(cliente.DIRECCLIENTE);
            //    Validations.VerificaInputStr(cliente.TELEFONOCLIENTE);
            //}
            //catch (ArgumentException ex)
            //{
            //    ModelState.AddModelError("", ex.Message);
            //}

            if (ModelState.IsValid)
            {
                await _clientService.UpdateClienteAsync(cliente.IDCLIENTE, cliente);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearClienteForm", cliente);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clientService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteClienteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
