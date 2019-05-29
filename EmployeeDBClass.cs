using Crud_applicaton.Models;
using PracticleCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticleCRUD.ModelClass
{
    public class EmployeeDBClass
    {
        TestEntities _DB = new TestEntities();
        public List<EmployeeModel> GetAllEmpList()
        {
            List<EmployeeModel> Model = new List<EmployeeModel>();
            var data = _DB.Employees.ToList();
            if (data.Count > 0)
            {
                var count = 0;
                foreach (var item in data)
                {
                    EmployeeModel model = new EmployeeModel();
                    count++;
                    model.count = count;
                    model.id = item.id;
                    model.name = item.name;
                    model.bithday = (DateTime)item.bithday;
                    var Contrydata = Getcontryname((int)item.ContryID);//item.ContryID;
                    var Statedata = Getstatename((int)item.StateID);//item.ContryID;
                    if (Contrydata != null)
                    {
                        model.Contry = Contrydata.ContryName;
                    }
                    if (Statedata != null)
                    {
                        model.State = Statedata.StateName;
                    }
                    Model.Add(model);
                }
            }
            return Model;
        }
        public int addEmployeeMethod(EmployeeModel model)
        {
            if (model.Editmode == 0)
            {
                Employee em = new Employee();
                em.name = model.name;
                em.bithday = DateTime.Now;
                em.ContryID = model.ContryID;
                em.StateID = model.StateID;
                em.IsActive = true;
                _DB.Employees.Add(em);
                _DB.SaveChanges();
            }
            else if (model.Editmode == 1)
            {
                var empdata = _DB.Employees.Where(x => x.id == model.id).FirstOrDefault();
                if (empdata != null)
                {
                    empdata.id = model.id;
                    empdata.name = model.name;
                    empdata.bithday = model.bithday;
                    empdata.ContryID = model.ContryID;
                    empdata.StateID = model.StateID;
                    _DB.SaveChanges();
                }
                return 1;//edit
            }
            return 2;// add
        }
        public bool deleteEmployeeMethod(int id)
        {
            var empdata = _DB.Employees.Where(x => x.id == id).FirstOrDefault();
            if (empdata != null)
            {
                empdata.IsActive = false;
                _DB.SaveChanges();
                return true;
            }
            return false; // delete
        }
        public List<EmployeeModel> mapEmployee(List<EmployeeModel> modeldata)
        {
            List<EmployeeModel> Model = new List<EmployeeModel>();
            if (modeldata.Count > 0)
            {
                var count = 0;
                foreach (var item in modeldata)
                {
                    EmployeeModel model = new EmployeeModel();
                    count++;
                    model.count = count;
                    model.id = item.id;
                    model.name = item.name;
                    model.bithday = (DateTime)item.bithday;
                    var Contrydata = Getcontryname((int)item.ContryID);//item.ContryID;
                    var Statedata = Getstatename((int)item.StateID);//item.ContryID;
                    if (Contrydata != null)
                    {
                        model.Contry = Contrydata.ContryName;
                    }
                    if (Statedata != null)
                    {
                        model.State = Statedata.StateName;
                    }
                    Model.Add(model);
                }
            }
            return Model;
        }
        public EmployeeModel GetEmployeeDetailsById(int Id)
        {
            EmployeeModel model = new EmployeeModel();
            var data = _DB.Employees.Where(x => x.id == Id).FirstOrDefault();
            if (data != null)
            {
                // model.Count = 1;
                model.id = data.id;
                model.ContryID = (int)data.ContryID;
                model.StateID = (int)data.StateID;
                model.name = data.name;
                model.bithday = (DateTime)data.bithday;
                //model.IsEdit = 1;
            }
            return model;
        }
        public ContryTbl Getcontryname(int Cid)
        {

            return _DB.ContryTbls.Where(x => x.ContryId == Cid).FirstOrDefault();
        }
        public StateTbl Getstatename(int sid)
        {
            return _DB.StateTbls.Where(x => x.StateId == sid).FirstOrDefault();
        }
    }
}