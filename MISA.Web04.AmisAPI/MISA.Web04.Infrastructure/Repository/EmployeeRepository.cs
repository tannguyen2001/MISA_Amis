using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces;
namespace MISA.Web04.Infrastructure.Repository
{
    /// <summary>
    /// Kế thừa từ cha
    /// </summary>
    public class EmployeeRepository:BaseRepository<Employee>,IEmployeeRepository
    {
        
        public EmployeeRepository(IConfiguration configuration):base(configuration){}

        public bool CheckEmployeeCodeExits(string employeeCode)
        {
                var sqlCheckEmployeeCode = $"select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
                DynamicParameters paramCheck = new DynamicParameters();
                paramCheck.Add("@EmployeeCode", employeeCode);
                var employeeCodeExist = SqlConnection.QueryFirstOrDefault(sqlCheckEmployeeCode, param: paramCheck);
                if (employeeCodeExist != null) return true;
                return false;
                
        }

        /// <summary>
        /// Lấy nhân viên theo khi tìm kiếm
        /// </summary>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageIndex">Trang số</param>
        /// <param name="filter">Tìm kiếm theo tên, mã hoặc số điện thoại</param>
        /// <returns>Array</returns>
        public IEnumerable<Employee>Search(int pageSize, int pageIndex, string? filter)
        {
            var sqlCommand = "Proc_PagingEmployees";
            // lấy parameters
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_pageSize", pageSize);
            parameters.Add("@m_pageIndex", pageIndex);
            parameters.Add("@m_filter", filter ?? ""); // nếu null thì lấy giá trị string rỗng
            // . Trả dữ liệu về 
            var res = SqlConnection.Query<Employee>(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Lấy ra mã nhân viên mới nhất
        /// </summary>
        /// <returns>String</returns>
        public string NewEmployeeCode()
        {
            //  Lấy dữ liệu từ database
            var sqlCommand = "Proc_GetNewEmployeeCode";
            // . Trả dữ liệu về 
            var res = SqlConnection.QuerySingle<string>(sqlCommand, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }

        
    }
}
