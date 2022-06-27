using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA.Web04.AmisAPI.Model;

namespace MISA.Web04.AmisAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        // các thiệt lập kết nối database
        IConfiguration _configuration;
        string _connectionString;
        public PositionsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("NVTAN");
        }

        /// <summary>
        /// Lấy tất cả vị trí
        /// </summary>
        /// <returns>Array</returns>
        [HttpGet]
        public IActionResult GetPositions()
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var Positionss = sqlConnection.Query<Positions>("Proc_GetPositions", commandType: System.Data.CommandType.StoredProcedure);
                // 3. Trả dữ liệu về 
                return Ok(Positionss);
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
        /// Lấy vị trí theo id
        /// </summary>
        /// <param name="PositionId"></param>
        /// <returns>Object</returns>
        [HttpGet("{positionId}")]
        public IActionResult GetPositionById(string positionId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_GetPositionById";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_PositionId", positionId);
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

        [HttpPut]
        public IActionResult UpdatePosition(string positionId, Positions position)
        {
            try
            {
                if (positionId == null || positionId == "")
                {
                    var errorValidate = new
                    {
                        userMsg = "PositionId eror"
                    };
                    return StatusCode(400, errorValidate);
                }

                else
                {
                    // 1.Khởi tạo kết nối tới database
                    var sqlConnection = new MySqlConnection(_connectionString);
                    // 2. Lấy dữ liệu từ database
                    var sqlCommand = "Proc_UpdatePosition";
                    // 3. lấy parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_PositionId", positionId);
                    parameters.Add("@m_PositionName", position.PositionName);
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
        /// Thêm mới vị trí
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePosition(Positions position)
        {
            try
            {
                if (string.IsNullOrEmpty(position.PositionName))
                {
                    var errorValidate = new
                    {
                        userMsg = "Tên phòng ban không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                else
                {
                    // 1.Khởi tạo kết nối tới database
                    var sqlConnection = new MySqlConnection(_connectionString);
                    // 2. Lấy dữ liệu từ database
                    var sqlCommand = "Proc_AddPosition";
                    // 3. lấy parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_PositionName", position.PositionName);
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
        /// Xoá vị trí
        /// </summary>
        /// <param name="PositionId"></param>
        /// <returns></returns>
        [HttpDelete("{positionId}")]
        public IActionResult DeletePosition(string positionId)
        {
            try
            {
                // 1.Khởi tạo kết nối tới database
                var sqlConnection = new MySqlConnection(_connectionString);
                // 2. Lấy dữ liệu từ database
                var sqlCommand = "Proc_DeletePosition";
                // 3. lấy parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_PositionId", positionId);
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
