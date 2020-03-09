using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstApp.Models;
using System.Collections.Generic;
using DBFirstApp.Controllers.Employee.ViewModel;
using System;
using DBFirstApp.Service;
using DBFirstApp.Controllers.Employee.ViewModel.Factory;
using Microsoft.AspNetCore.Mvc.Filters;
using DBFirstApp.Ex;
using DBFirstApp.Domain.Employees;

namespace DBFirstApp.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        private readonly MydatabaseContext _context;

        private readonly ICreateEmployeeService _CreateEmployeeService;
        private readonly IEmployeeListService _EmployeeListService;
        private readonly IEmployeeSelfReferenceService _EmployeeSelfReferenceService;
        private readonly IEmployeeFamilyReferenceService _EmployeeFamilyReferenceService;
        private readonly IEmployeeFamilySaveService _EmployeeFamilySaveService;
        private readonly IEmployeeSelfSaveService _EmployeeSelfSaveService;

        private readonly IEmployeeDetailReferenceService _EmployeeDetailReferenceService;
        private readonly Dictionary<int, string>  HumanType =new Dictionary<int, string>() {
                { 0, "男" },
                { 1, "女" },
                { 2, "other" },
            };

        public EmployeeController(MydatabaseContext context,
            ICreateEmployeeService createEmployeeService,
            IEmployeeListService employeeListService,
            IEmployeeSelfReferenceService employeeSelfReferenceService,
            IEmployeeFamilyReferenceService employeeFamilyReferenceService,
            IEmployeeDetailReferenceService employeeDetailReferenceService,
            IEmployeeFamilySaveService employeeFamilySaveService,
            IEmployeeSelfSaveService employeeSelfSaveService)
        {
            _context = context;
            _CreateEmployeeService = createEmployeeService;
            _EmployeeListService = employeeListService;
            _EmployeeSelfReferenceService = employeeSelfReferenceService;
            _EmployeeFamilyReferenceService = employeeFamilyReferenceService;
            _EmployeeDetailReferenceService = employeeDetailReferenceService;
            _EmployeeFamilySaveService = employeeFamilySaveService;
            _EmployeeSelfSaveService = employeeSelfSaveService;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            EmployeeListResponse responce = await _EmployeeListService.HandleAsync(new EmployeeListRequest());

            return View(EmployeeViewModelFactory.ToListViewModel(responce));
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var responce = await _EmployeeDetailReferenceService.Handle(new EmployeeDetailReferenceRequest(id));

            return View(EmployeeViewModelFactory.ToDetailViewModel(responce));
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            TempData["HumanType"] = HumanType;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel selfViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["HumanType"] = HumanType;
                return View(selfViewModel);
            }
            var request = new UserCreateRequest(
                employeeId : selfViewModel.Id,
                firstName: selfViewModel.FirstName,
                lastName: selfViewModel.LastName,
                sex: selfViewModel.Sex,
                birthday: selfViewModel.Birthday,
                email: selfViewModel.Email
            );

            await this._CreateEmployeeService.Handle(request);
            return RedirectToAction(nameof(Self),new { id = selfViewModel.Id });
        }
        

        // GET: Employee/Edit/5
        public async Task<IActionResult> Self(string id)
        {
            var resopnse = await _EmployeeSelfReferenceService.Handle(new EmployeeSelfReferenceRequest(id));

            TempData["HumanType"] = HumanType;
            return View(EmployeeViewModelFactory.ToSelfViewModle(resopnse));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Self(string id, EmployeeSelfViewModel selfViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(selfViewModel);
            }

            var request = new EmployeeSelfSaveRequest(
                employeeId: selfViewModel.Id,
                firstName: selfViewModel.FirstName,
                lastName: selfViewModel.LastName,
                sex: selfViewModel.Sex,
                birthday: selfViewModel.Birthday,
                email: selfViewModel.Email
            );

            EmployeeSelfSaveResponse reponse = await _EmployeeSelfSaveService.HandleAsync(request);
            return RedirectToAction(nameof(Self), new { id = selfViewModel.Id });
        }


        // GET: Employee/Edit/5
        public async Task<IActionResult> Family(string id)
        {
            EmployeeFamilyReferenceResponse resopnse = await _EmployeeFamilyReferenceService.Handle(
                new EmployeeFamilyReferenceRequest()
                {
                    EmployeeId = id
                });
            TempData["HumanType"] = HumanType;

            return View(EmployeeViewModelFactory.ToFamilyViewModel(resopnse));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Family(string id,
            EmployeeFamilyViewModel employeeFamilyViewModel)
        {
            await _EmployeeFamilySaveService.HandleAsync(new EmployeeFamilySaveRequest()
            {
                EmployeeId = id,
                HumanId = employeeFamilyViewModel.Family.HumanId,
                FirstName = employeeFamilyViewModel.Family.FirstName,
                LastName = employeeFamilyViewModel.Family.LastName,
                Birthday = employeeFamilyViewModel.Family.Birthday,
                Sex = employeeFamilyViewModel.Family.Sex,
                RelationShip = employeeFamilyViewModel.Family.RelationShip
            });

            return RedirectToAction(nameof(Family), new { id = id });
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tEmployee = await _context.TEmployee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (tEmployee == null)
            {
                return NotFound();
            }

            return View(tEmployee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tEmployee = await _context.TEmployee.FindAsync(id);
            _context.TEmployee.Remove(tEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TEmployeeExists(string id)
        {
            return _context.TEmployee.Any(e => e.EmployeeId == id);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is NotFoundException)
            {
                context.Exception = null;
                context.Result = NotFound();
            }
            base.OnActionExecuted(context);
        }
    }
}
