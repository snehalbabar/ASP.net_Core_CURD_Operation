using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetMvcCRUD.Data;
using ASPNetMvcCRUD.Models;
using ASPNetMvcCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNetMvcCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MvcDemoDbContext mvcDemoDbContext;

        public EmployeeController(MvcDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        // GET: /<controller>/


        [HttpGet]
        public async Task< IActionResult> Index()
        {

           var employess = await mvcDemoDbContext.MyProperty.ToListAsync();
            return View(employess);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModel)
        {
            var employee = new Employees()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeViewModel.Name,
                Email = addEmployeeViewModel.Email,
                salary = addEmployeeViewModel.salary,
                Department = addEmployeeViewModel.Department,
                DateOfBirth = addEmployeeViewModel.DateOfBirth

            };

            await mvcDemoDbContext.MyProperty.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var employee = await mvcDemoDbContext.MyProperty.FirstOrDefaultAsync(x => x.Id == Id);

            if (employee != null)
            {
                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    salary = employee.salary,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department

                };
                return await Task.Run(()=> View("View", viewmodel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            var employee = await mvcDemoDbContext.MyProperty.FindAsync(updateEmployeeViewModel.Id);

            if (employee != null)
            {
    
                employee.Name = updateEmployeeViewModel.Name;
                employee.Email = updateEmployeeViewModel.Email;
                employee.salary = updateEmployeeViewModel.salary;
                employee.DateOfBirth = updateEmployeeViewModel.DateOfBirth;
                employee.Department = updateEmployeeViewModel.Department;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            var employee = await mvcDemoDbContext.MyProperty.FindAsync(updateEmployeeViewModel.Id);

            if (employee != null)
            {
                mvcDemoDbContext.MyProperty.Remove(employee);
                mvcDemoDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}

