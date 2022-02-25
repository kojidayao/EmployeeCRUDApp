using EmployeeCRUDApp.Data;
using EmployeeCRUDApp.Infrastructure;
using EmployeeCRUDApp.Models;
using EmployeeCRUDApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;

namespace EmployeeCRUDApp.Controllers
{
    //[AuthorizeForScopes(Scopes = new[] { GraphScopes.UserReadBasicAll })]
    //[Authorize(Policy = AuthorizationPolicies.AssignmentToDirectoryViewerRoleRequired)]

    [Authorize]
    public class EmployeeController : Controller
    {
       
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
           
            return View(employees);
        }

        



        [AuthorizeForScopes(Scopes = new[] { GraphScopes.UserReadBasicAll })]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToUserReaderRoleRequired)]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


     
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [AuthorizeForScopes(Scopes = new[] { GraphScopes.UserReadBasicAll })]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToUserReaderRoleRequired)]
        public async Task<IActionResult> Edit(int? ID)
        {
            //if ID is equal to null AND ID is less than or equal to 0, return Bad Request
            if (ID == null || ID <= 0)
                return BadRequest();
            //
            var employeeinDB = await _context.Employees.FirstOrDefaultAsync(e => e.ID == ID);
            //if the database is null, return Not Found
            if (employeeinDB == null)
                return NotFound();

            return View(employeeinDB);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [AuthorizeForScopes(Scopes = new[] { GraphScopes.UserReadBasicAll })]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToUserReaderRoleRequired)]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [AuthorizeForScopes(Scopes = new[] { GraphScopes.UserReadBasicAll })]
        [Authorize(Policy = AuthorizationPolicies.AssignmentToUserReaderRoleRequired)]
        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null || ID <= 0)
                return BadRequest();
            var employeeinDB = await _context.Employees.FirstOrDefaultAsync(e => e.ID == ID);
            if (employeeinDB == null)
                return NotFound();
            _context.Employees.Remove(employeeinDB);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}
       



    

