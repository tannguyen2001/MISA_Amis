using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces;


namespace MISA.Web04.Core.Services
{
    public class EmployeeService :BaseService<Employee> ,IEmployeeService
    {
        IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository):base(repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// validate insert Employee
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateInsert(Employee employee)
        {
            var isValid = true;
            // kiểm tra mã nhân viên có trống không
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeNotEmpty);
            }
            // kiểm tra trùng mã:
            var isExits = _repository.CheckEmployeeCodeExits(employee.EmployeeCode);
            if (isExits)
            {
                ErrorValidateMsg.Add(String.Format(Resources.ResourceVN.VN_ValidateError_EmployeeCodeExits, employee.EmployeeCode));
            }
            // kiểm tra tên nhân viên có trống không
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeNameNotEmpty);
            }
            // kiểm tra mã phòng ban có trống không
            if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DepartmentNotEmpty);
            }

            // kiểm tra định dạng email đã đúng chưa

            var isValidateEmail = IsValidEmail(employee.Email);
            if (!isValidateEmail && !string.IsNullOrEmpty(employee.Email))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_Email);
            }
            // kiểm tra ngày sinh có lớn hơn ngày hiện tại không
            var dateOfBirth = employee.DateOfBirth;
            var currentDate = DateTime.Now;
            if (dateOfBirth > currentDate)
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DateOfBirth);
            }

            // kiểm tra mã có độ dài >20 kí tự hay không?
            if (employee.EmployeeCode.Length > 20)
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeLength);
            }
            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }

            return isValid;
        }


        /// <summary>
        /// validate update
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateUpdate(Employee employee)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeNotEmpty);
            }

            // kiểm tra trùng mã:
            var isExits = _repository.CheckEmployeeCodeExits(employee.EmployeeCode);
            var employeeCheck = _repository.GetById(employee.EmployeeId);
            if (isExits && employeeCheck.EmployeeCode != employee.EmployeeCode)
            {
                ErrorValidateMsg.Add(String.Format(Resources.ResourceVN.VN_ValidateError_EmployeeCodeExits, employee.EmployeeCode));
            }
            // kiểm tra tên nhân viên có trống không
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeNameNotEmpty);
            }
            // kiểm tra mã phòng ban có trống không
            if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DepartmentNotEmpty);
            }

            // kiểm tra định dạng email đã đúng chưa

            var isValidateEmail = IsValidEmail(employee.Email);
            if (!isValidateEmail && !string.IsNullOrEmpty(employee.Email))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_Email);
            }
            // kiểm tra ngày sinh có lớn hơn ngày hiện tại không
            var dateOfBirth = employee.DateOfBirth;
            var currentDate = DateTime.Now;
            if (dateOfBirth > currentDate)
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DateOfBirth);
            }

            // kiểm tra mã nhân viên có độ dài >20 kí tự hay không?
            if (employee.EmployeeCode.Length > 20)
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeLength);
            }
            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Kiểm tra định giạng email đã đúng chưa
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>Boolean</returns>
        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
