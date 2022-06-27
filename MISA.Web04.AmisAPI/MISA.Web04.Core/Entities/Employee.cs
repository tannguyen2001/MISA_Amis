using MISA.Web04.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Mã giới tính
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Male:
                        return Resources.ResourceVN.VN_Gender_Male;
                    case Enum.Gender.Female:
                        return Resources.ResourceVN.VN_Gender_Female;
                    case Enum.Gender.Undefined:
                        return Resources.ResourceVN.VN_Gender_Undefined;
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Số điệnt thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số cmnd
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        /// id vị trí
        /// </summary>
        public Guid? PositionId { get; set; }
        /// <summary>
        /// vị trí
        /// </summary>
        public string? PositionName { get; set; }
        /// <summary>
        /// Mức lương
        /// </summary>
        public double? Salary { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại bàn
        /// </summary>
        public string? TelephoneNumber { get; set; }
        /// <summary>
        /// số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? Checked { get { return false; } }
    }
}
