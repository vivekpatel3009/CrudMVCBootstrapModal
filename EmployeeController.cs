using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crud_applicaton.Models;
using PracticleCRUD.Models;
using AutoMapper;
using PracticleCRUD.ModelClass;

namespace Crud_applicaton.Controllers
{
    public class EmployeeController : Controller
    {
        TestEntities _DB = new TestEntities();
        EmployeeDBClass ed = new EmployeeDBClass();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HandleError]
        public ActionResult List(string serach)
        {
            try
            {
                List<EmployeeModel> Model = new List<EmployeeModel>();
                var emplist = _DB.Employees.Where(x => x.IsActive == true).ToList();
                List<EmployeeModel> list = Mapper.Map<List<Employee>, List<EmployeeModel>>(emplist);
                List<EmployeeModel> mapedemployee= ed.mapEmployee(list);
                return PartialView("_EmployeeList", mapedemployee);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", new { ReturnUrl = "/Floor" });
            }
        }
        [HttpGet]
        public ActionResult CreateEmployee(EmployeeModel model)
        {
            ViewBag.ContryListVB = _DB.ContryTbls.ToList();
            ViewBag.FloorListVB = _DB.StateTbls.ToList();
            model.Editmode = 0;
            return PartialView("_addEditEmployee", model);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                ed.addEmployeeMethod(model);                
            }
           return RedirectToAction("Index");
        }
        public ActionResult EditEmployee(int id)
        {
            if (id == 0)
            {
                return null;
            }           
            ViewBag.ContryListVB = _DB.ContryTbls.ToList();
            EmployeeModel emp = ed.GetEmployeeDetailsById(id);
            ViewBag.FloorListVB = _DB.StateTbls.Where(x=>x.ContryId==emp.ContryID).ToList();
            emp.Editmode = 1;
            if (emp == null)
            {
                return null;
            }
            return PartialView("_addEditEmployee", emp);
        }
        public ActionResult DeleteEmployee(int Id)
        {
            bool data = ed.deleteEmployeeMethod(Id);
            if (data)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }        
        public JsonResult ContryChange(int id)
        {
            var ddlContry = _DB.StateTbls.Where(x => x.ContryId == id).ToList();
            List<SelectListItem> liContries = new List<SelectListItem>();
            liContries.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            if (ddlContry != null)
            {
                foreach (var x in ddlContry)
                {
                    liContries.Add(new SelectListItem { Text = x.StateName, Value = x.StateId.ToString() });
                }
            }
            return Json(new SelectList(liContries, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
    }
}