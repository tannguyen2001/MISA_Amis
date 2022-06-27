using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces;
using MISA.Web04.Core.Exceptions;

namespace MISA.Web04.Core.Services
{
    public class DepartmentService : BaseService<Department>,IDepartmentService
    {
        IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository):base(repository) 
        {
            _repository = repository;
        }

        /// <summary>
        /// Validate insert phòng ban
        /// </summary>
        /// <param name="department">department</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateInsert(Department department)
        {
            // validate dữ liệu
            var isValid = true;
            // Bắt buộc phải nhập tên
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DepartmentNotEmpty);
            }

            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Validate update phòng ban
        /// </summary>
        /// <param name="department">department</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateUpdate(Department department)
        {
            // validate dữ liệu
            var isValid = true;
            // Bắt buộc phải nhập tên
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_DepartmentNotEmpty);
            }

            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
