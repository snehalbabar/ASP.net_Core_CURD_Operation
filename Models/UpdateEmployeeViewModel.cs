using System;
namespace ASPNetMvcCRUD.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Department { get; set; }
    }
}

