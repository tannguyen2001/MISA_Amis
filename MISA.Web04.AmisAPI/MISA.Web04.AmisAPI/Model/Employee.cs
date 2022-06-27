using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Web04.AmisAPI.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int? Gender { get; set; }
        public string GenderName { get
            {
                if(Gender == null)
                {
                    return "";
                }
                else if(Gender == 0)
                {
                    return "Nữ";
                }
                else if(Gender == 1)
                {
                    return "Nam";
                }
                else
                {
                    return "Không xác định";
                }
            } set { } }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string? IdentityPlace { get; set; }
        public DateTime? IdentityDate { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PositionId { get; set; }
        public string PositionName { get; set; }
        public double? Salary { get; set; }
        public string? Address { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankBranchName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
