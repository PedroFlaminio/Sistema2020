using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema2020.Data;
using Sistema2020.Models;
using Sistema2020.Models.ViewModels;
using Sistema2020.Services;
using Sistema2020.Services.Exceptions;

namespace Sistema2020.Controllers
{
    public class EstadosController : Controller
    {
        private readonly EstadosService _estService;

        public EstadosController(
            EstadosService estService)
        {
            _estService = estService;
        }

        [Authorize]
        // GET: Estados
        public async Task<IActionResult> Index()
        {
            return View(await _estService.FindAllAsync());
        }

        [Authorize]
        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var estado = await _estService.FindByIdAsync(id.Value);
            if (estado == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(estado);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Sigla,Nome")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                await _estService.InsertAsync(estado);
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var estado = await _estService.FindByIdAsync(id.Value);
            if (estado == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(estado);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Sigla,Nome")] Estado estado)
        {

            if (!ModelState.IsValid)
            {
                return View(estado);
            }

            if (id != estado.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id inesperado" });
            }
            try
            {
                await _estService.UpdateAsync(estado);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Authorize(Roles = "Administrador")]

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var estado = await _estService.FindByIdAsync(id.Value); if (estado == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _estService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }


    }
}
