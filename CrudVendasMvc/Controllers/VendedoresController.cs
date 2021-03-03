using System.Security.Cryptography;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudVendasMvc.Services;
using CrudVendasMvc.Services.Exceptions;
using CrudVendasMvc.Models;
using CrudVendasMvc.Models.ViewModels;

namespace CrudVendasMvc.Controllers
{
    public class VendedoresController : Controller
    {
        
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _vendedorService.EncontrarTodosAsync();
            return View(lista);
        }

        //GET: CREATE
        public async Task<IActionResult> Create()
        {
            var listaDepartamentos = await _departamentoService.EncontrarTodosAsync();
            var ViewModel = new VendedorViewModel { Departamentos = listaDepartamentos };
            return View(ViewModel);
        }
        
        //POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            //Teste de validação
            if (!ModelState.IsValid)
            {
                var listaDepartamentos = await _departamentoService.EncontrarTodosAsync();
                var viewModel =  new VendedorViewModel{ Vendedor = vendedor, Departamentos = listaDepartamentos};
                return View(viewModel);
            }

            await _vendedorService.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        //GET: DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }

            var vendedor = await _vendedorService.EncontrarPorIdAsync(id.Value);

            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado."});
            }
            
            return View(vendedor);
        }

        //POST: DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                 return RedirectToAction(nameof(Error), new { Message = ex.Message});
            }
        }

        //GET: DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }

            var vendedor = await  _vendedorService.EncontrarPorIdAsync(id.Value);

            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado."});
            }

            return View(vendedor);
        }

        //GET: EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }

            var vendedor = await _vendedorService.EncontrarPorIdAsync(id.Value);

            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado."});
            }

            // List<Departamento> listaDepartamentos = await _departamentoService.EncontrarTodosAsync();
            // VendedorViewModel viewModel = new VendedorViewModel{ Vendedor = vendedor, Departamentos = listaDepartamentos};
            var listaDepartamentos = await _departamentoService.EncontrarTodosAsync();
            var viewModel = new VendedorViewModel{ Vendedor = vendedor, Departamentos = listaDepartamentos};
            return View(viewModel);
        }

        //POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var listaDepartamentos = await _departamentoService.EncontrarTodosAsync();
                var viewModel =  new VendedorViewModel{ Vendedor = vendedor, Departamentos = listaDepartamentos};
                return View(viewModel);
            }

            // O id não pode ser diferente do Id do URL da request
            if ( id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondente."});
            }

            try
            {
                await _vendedorService.AtualizarAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message});
            }
        }

        public IActionResult Error(String message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //Pegando Id internet da request
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
        
    }
}
