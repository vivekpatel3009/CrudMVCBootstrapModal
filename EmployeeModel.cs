using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Crud_applicaton.Controllers;
using Crud_applicaton.Models;

namespace Crud_applicaton.Models
{

    public class EmployeeModel
    {
        public int count { get; set; }
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime bithday { get; set; }
        public string State { get; set; }        
        public string Contry { get; set; }
        [Required]
        public int StateID { get; set; }
        [Required]
        public int ContryID { get; set; }
        public int Editmode { get; set; }

       
       
        //public date bithday { get; set; }
    }
}