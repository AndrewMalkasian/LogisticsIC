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
        public Admin Admins { get; set; }
        public Employee Employees { get; set; }
        public Customer Customers { get; set; }
    }
}