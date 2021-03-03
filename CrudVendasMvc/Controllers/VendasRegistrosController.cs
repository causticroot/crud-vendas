using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudVendasMvc.Models;
using CrudVendasMvc.Models.Enums;
using CrudVendasMvc.Models.ViewModels;
using CrudVendasMvc.Services;
using CrudVendasMvc.Services.Exceptions;

namespace CrudVendasMvc.Controllers
{
    public class VendasRegistrosController : Controller
    {
        private readonly VendasRegistroService _vendasRegistroService;
        private readonly VendedorService _vendedorService;
        

        public VendasRegistrosController(VendasRegistroService vendasRegistroService, VendedorService vendedorService)
        {
            _vendasRegistroService = vendasRegistroService;
            _vendedorService = vendedorService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _vendasRegistroService.EncontrarTodosAsync();
            return View(lista);
        }


        public async Task<IActionResult> PesquisaSimplesAsync(DateTime? dataMin, DateTime? dataMax)
        {
            if (!dataMin.HasValue)
            {
                dataMin = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataMax.HasValue)
            {
                dataMax = DateTime.Now;
            }
            //Saída da data
            ViewData["dataMin"] = dataMin.Value.ToString("dd-MM-yyyy");
            ViewData["dataMax"] = dataMax.Value.ToString("dd-MM-yyyy");

            var resultado = await _vendasRegistroService.EncontrarPorDataAsync(dataMin, dataMax);
            
            return View(resultado);
        }


        //GET: CREATE
        public async Task<IActionResult> Create()
        {
            var listaVendedores = await _vendedorService.EncontrarTodosAsync();
            var listaEstados = Enum.GetValues(typeof(VendaEstado)).Cast<VendaEstado>().ToList();
            var viewModel = new VendasRegistroViewModel { Vendedores = listaVendedores, Estados = listaEstados };
            return View(viewModel);
        }
        
        //POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendasRegistro vendasRegistro)
        {
            //Teste de validação
            if (!ModelState.IsValid)
            {
                var listaVendedores = await _vendedorService.EncontrarTodosAsync();
                var listaEstados = Enum.GetValues(typeof(VendaEstado)).Cast<VendaEstado>().ToList();
                var viewModel = new VendasRegistroViewModel { Vendedores = listaVendedores, Estados = listaEstados }; //TODO: Rever
                return View(viewModel);
            }

            await _vendasRegistroService.InserirAsync(vendasRegistro);
            return RedirectToAction(nameof(Index));   
        }
        
        
        //GET: DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }
            
            var venda = await _vendasRegistroService.EncontrarPorIdAsync(id.Value);

            if (venda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado."});
            }

            return View(venda);
        }
        
        //GET: DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido"});
            }

            var venda = await _vendasRegistroService.EncontrarPorIdAsync(id.Value);

            if (venda == null)
            {
                return RedirectToAction(nameof(Error), new { message = " Id não encontrado"});
            }

            return View(venda);        
        }
        
        //POST: DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendasRegistroService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message});
            }
        }
        
        //GET: EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }

            var venda = await _vendasRegistroService.EncontrarPorIdAsync(id.Value);

            if (venda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado."});    
            }

            var listaVendedores = await _vendedorService.EncontrarTodosAsync();
            var listaEstados = Enum.GetValues(typeof(VendaEstado)).Cast<VendaEstado>().ToList();
            var viewModel = new VendasRegistroViewModel{ VendasRegistro = venda, Estados = listaEstados, Vendedores = listaVendedores};
            
            return View(viewModel);
        } 
        
        //POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        //EU TENHO QUE LEMBRAR DE SER MENOS RETARDADO E PASSAR O ARGUMENTO COM O MESMO NOME DO OBJETO
        //ISSO ME FODEU E ME CUSTOU HORAS TENTANDO RESOLVER UM DEFEITO QUE NÃO EXISTIA
        public async Task<IActionResult> Edit(int id, VendasRegistro vendasRegistro)
        {
            if (!ModelState.IsValid)
            {
                var listaVendedores = await _vendedorService.EncontrarTodosAsync();
                var listaEstados = Enum.GetValues(typeof(VendaEstado)).Cast<VendaEstado>().ToList();
                var viewModel = new VendasRegistroViewModel{ VendasRegistro = vendasRegistro, Estados = listaEstados, Vendedores = listaVendedores};
                return View(viewModel);
            }

            if (id != vendasRegistro.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondente."});
            }

            try
            {
                await _vendasRegistroService.AtualizarAsync(vendasRegistro);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                 return RedirectToAction(nameof(Error), new { Message = ex.Message});
            }
        }

        //Error
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
