using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Entities;

namespace MISA.Web04.Core.Interfaces
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy nhân viên theo khi tìm kiếm
        /// </summary>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageIndex">Trang số</param>
        /// <param name="filter">Tìm kiếm theo tên, mã hoặc số điện thoại</param>
        /// <returns>Array</returns>
        IEnumerable<Employee> Search(int pageSize, int pageIndex, string? filter);

        /// <summary>
        /// Lấy ra mã nhân viên mới nhất
        /// </summary>
        /// <returns>String</returns>
        string NewEmployeeCode();

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>Boolean</returns>
        public bool CheckEmployeeCodeExits(string employeeCode);

    }
}
