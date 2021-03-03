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
    public class DepartamentosController : Controller
    {
        private readonly DepartamentoService _departamentoService;

        public DepartamentosController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _departamentoService.EncontrarTodosAsync();
            return View(lista);
        }

        //GET: CREATE
        //Síncrono
        public IActionResult Create()
        {
            return View();
        }

        //POST: CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            await _departamentoService.InserirAsync(departamento);
            return RedirectToAction(nameof(Index));
        }

        //GET: DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido."});
            }

            var departamento = await _departamentoService.EcontrarPorIdAsync(id.Value);

            if (departamento == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não encontrado."});
            }

            return View(departamento);
        }

        //GET: DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido."});
            }

            var departamento = await _departamentoService.EcontrarPorIdAsync(id.Value);

            if (departamento == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não encontrado."});
            }

            return View(departamento);
        }

        //POST: DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departamentoService.RemoverAsync(id);
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
                return RedirectToAction(nameof(Error), new { Message = "Id não fornecido."});
            }

            var departamento = await _departamentoService.EcontrarPorIdAsync(id.Value);

            if (departamento == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não encontrado."});
            }

            return View(departamento);
        }

        //POST: EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != departamento.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não correspondente."});
            }

            try
            {
                await _departamentoService.AtualizarAsync(departamento);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                 return RedirectToAction(nameof(Error), new { Message= ex.Message});
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