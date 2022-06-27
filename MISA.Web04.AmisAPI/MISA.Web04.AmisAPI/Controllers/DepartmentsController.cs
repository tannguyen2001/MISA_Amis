
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using MISA.Web04.AmisAPI.Model;
using Dapper;

namespace MISA.Web04.AmisAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]


    public class DepartmentsController : ControllerBase
    {
        // các thiệt lập kết nối database
        IConfiguration _configuration;
        string _connectionString;
        public DepartmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("NVTAN");
        }

        /// <summary>
        /// Lấy tất cả phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var departments = sqlConnection.Query<Department>("Proc_GetDepartments", commandType: System.Data.CommandType.StoredProcedure);
                // 3. Trả dữ liệu về 
                return Ok(departments);
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
        /// Lấy phòng ban theo id
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns>Object</returns>
        [HttpGet("{departmentId}")]
        public IActionResult GetDepartmentById(string departmentId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_GetDepartmentById";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_DepartmentId", departmentId);
                // . Trả dữ liệu về 
                var res = sqlConnection.Query(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            try
            {
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    var errorValidate = new {
                        userMsg = "Tên phòng ban không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                else
                {
                    // 1.Khởi tạo kết nối tới database
                    var sqlConnection = new MySqlConnection(_connectionString);
                    // 2. Lấy dữ liệu từ database
                    var sqlCommand = "Proc_AddDepartment";
                    // 3. lấy parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_DepartmentName", department.DepartmentName);
                    // . Trả dữ liệu về 
                    var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return StatusCode(201, res);
                }

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
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateDepartment(string departmentId,[FromBody]Department department)
        {
            try
            {
                if (departmentId == null || departmentId == "")
                {
                    var errorValidate = new
                    {
                        userMsg = "departmentId eror"
                    };
                    return StatusCode(400, errorValidate);
                }

                else
                {
                    // 1.Khởi tạo kết nối tới database
                    var sqlConnection = new MySqlConnection(_connectionString);
                    // 2. Lấy dữ liệu từ database
                    var sqlCommand = "Proc_UpdateDepartment";
                    // 3. lấy parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_DepartmentId", departmentId);
                    parameters.Add("@m_DepartmentName", department.DepartmentName);
                    // . Trả dữ liệu về 
                    var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return StatusCode(200, res);
                }
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
        /// <param name="departmentId"></param>
        /// <returns>Array</returns>
        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(string departmentId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_DeleteDepartment";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_DepartmentId", departmentId);
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
    }
}
