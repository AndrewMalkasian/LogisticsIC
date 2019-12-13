using IndividualCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IndividualCapstone.Controllers
{
    public class AdminEmployeeCustomerViewModel
    {
        [Key]
        public int Id { get; set; }
        public List<Admin> Admins { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Customer> Customers { get; set; }
    }
}