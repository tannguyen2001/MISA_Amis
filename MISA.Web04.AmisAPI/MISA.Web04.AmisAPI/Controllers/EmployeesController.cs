using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.AmisAPI.Model;
using Dapper;
using MySqlConnector;

namespace MISA.Web04.AmisAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // các thiệt lập kết nối database
        IConfiguration _configuration;
        string _connectionString;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("NVTAN");
        }

        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var employees = sqlConnection.Query<Employee>("Proc_GetEmployees", commandType: System.Data.CommandType.StoredProcedure);
                // 3. Trả dữ liệu về 
                return Ok(employees);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Lấy nhân viên theo theo id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>Object</returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(string employeeId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_GetEmployeeById";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_EmployeeId", employeeId);
                // . Trả dữ liệu về 
                var res = sqlConnection.QueryFirstOrDefault(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Lấy nhân viên khi tìm kiếm
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetEmployeeBySearch(int pageSize, int pageIndex, string? filter)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_PagingEmployees";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_pageSize", pageSize);
                parameters.Add("@m_pageIndex", pageIndex);
                parameters.Add("@m_filter", filter ?? "");
                // . Trả dữ liệu về 
                var res = sqlConnection.Query<Employee>(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }



        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param Employee="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            // khởi tạo kết nối sql
            var sqlConnection = new MySqlConnection(_connectionString);
            try
            {
                // kiểm tra mã nhân viên có trống không
                if ( string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    var errorValidate = new
                    {

                        userMsg = "Mã nhân viên không được phép để trống" 
                    
                    };
                    return BadRequest(errorValidate);
                }
                // kiểm tra tên nhân viên có trống không
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    var errorValidate = new
                    {

                        userMsg = "Tên nhân viên không được phép để trống"

                    };
                    return BadRequest(errorValidate);
                }
                // kiểm tra mã phòng ban có trống không
                if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
                {
                    var errorValidate = new
                    {

                        userMsg = "Phòng ban không được phép để trống"

                    };
                    return BadRequest(errorValidate);
                }

                // kiểm tra định dạng email đã đúng chưa
                
                var isValidateEmail = IsValidEmail(employee.Email);
                if(!isValidateEmail && !string.IsNullOrEmpty(employee.Email))
                {
                    var errorValidate = new
                    {

                        userMsg = "Email không đúng định dạng, vui lòng kiểm tra lại"

                    };
                    return BadRequest(errorValidate);
                }
                // kiểm tra ngày sinh có lớn hơn ngày hiện tại không
                var dateOfBirth = employee.DateOfBirth;
                var currentDate = DateTime.Now; 
                if (dateOfBirth > currentDate)
                {
                    var errorValidate = new
                    {

                        userMsg = "Ngày sinh không được lớn hơn ngày hiện tại"

                    };
                    return BadRequest(errorValidate);
                }

                // kiểm tra mã nhân viên đã tồn tại hay chưa và có độ dài >20 kí tự hay không?
                if (employee.EmployeeCode.Length > 20)
                {
                    var errorValidate = new
                    {

                        userMsg = "Mã nhân viên > 20 kí tự"

                    };
                    return BadRequest(errorValidate);
                }

                var sqlCheckEmployeeCode = $"select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
                DynamicParameters paramCheck = new DynamicParameters();
                paramCheck.Add("@EmployeeCode", employee.EmployeeCode);
                var employeeCodeExist =  sqlConnection.QueryFirstOrDefault(sqlCheckEmployeeCode,param:paramCheck);
                if(employeeCodeExist != null)
                {
                    var errorValidate = new
                    {

                        userMsg = "Mã nhân viên đã tồn tại, vui lòng kiểm tra lại"

                    };
                    return BadRequest(errorValidate);
                }
                
                // thực hiện thêm mới khi dữ liệu đã được validate

                //  Lấy dữ liệu từ database
                var sqlCommand = "Proc_AddEmployee";
                //  lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_EmployeeCode", employee.EmployeeCode);
                parameters.Add("@m_EmployeeName", employee.EmployeeName);
                parameters.Add("@m_Gender", employee.Gender);
                parameters.Add("@m_DateOfBirth", employee.DateOfBirth);
                parameters.Add("@m_Email", employee.Email);
                parameters.Add("@m_PhoneNumber", employee.PhoneNumber);
                parameters.Add("@m_IdentityNumber", employee.IdentityNumber);
                parameters.Add("@m_IdentityPlace", employee.IdentityPlace);
                parameters.Add("@m_IdentityDate", employee.IdentityDate);
                parameters.Add("@m_DepartmentId", employee.DepartmentId);
                parameters.Add("@m_PositionId", employee.PositionId);
                parameters.Add("@m_Salary", employee.Salary);
                parameters.Add("@m_Address", employee.Address);
                parameters.Add("@m_TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("@m_BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("@m_BankName", employee.BankName);
                parameters.Add("@m_BankBranchName", employee.BankBranchName);
                // . Trả dữ liệu về 
                var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(201, res);
                

            }
            catch (Exception ex)
            {

                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }
        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <param string="EmployeeId"></param>
        /// <param Employee="Employee"></param>
        /// <returns></returns>
        [HttpPut("{EmployeeId}")]
        public IActionResult UpdateEmployee(string EmployeeId, [FromBody] Employee employee)
        {
            try
            {
                if (EmployeeId == null || EmployeeId == "")
                {
                    var errorValidate = new
                    {
                        userMsg = "Mã nhân viên không được để trống"
                    };
                    return StatusCode(400, errorValidate);
                }

                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    var errorValidate = new
                    {
                        userMsg = "Tên nhân viên không được để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
                {
                    var errorValidate = new
                    {
                        userMsg = "Phòng ban không được để trống"
                    };
                    return StatusCode(400, errorValidate);
                }


                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_UpdateEmployee";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_EmployeeId", EmployeeId);
                parameters.Add("@m_EmployeeCode", employee.EmployeeCode);
                parameters.Add("@m_EmployeeName", employee.EmployeeName);
                parameters.Add("@m_Gender", employee.Gender);
                parameters.Add("@m_DateOfBirth", employee.DateOfBirth);
                parameters.Add("@m_Email", employee.Email);
                parameters.Add("@m_PhoneNumber", employee.PhoneNumber);
                parameters.Add("@m_IdentityNumber", employee.IdentityNumber);
                parameters.Add("@m_IdentityPlace", employee.IdentityPlace);
                parameters.Add("@m_IdentityDate", employee.IdentityDate);
                parameters.Add("@m_DepartmentId", employee.DepartmentId);
                parameters.Add("@m_PositionId", employee.PositionId);
                parameters.Add("@m_Salary", employee.Salary);
                parameters.Add("@m_Address", employee.Address);
                parameters.Add("@m_TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("@m_BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("@m_BankName", employee.BankName);
                parameters.Add("@m_BankBranchName", employee.BankBranchName);
                // . Trả dữ liệu về 
                var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(200, res);
                
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }
        /// <summary>
        /// Xoá phòng ban theo id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Array</returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(string employeeId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_DeleteEmployee";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_EmployeeId", employeeId);
                // . Trả dữ liệu về 
                var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
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
