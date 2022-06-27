using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using MISA.Web04.Core.Interfaces;

namespace MISA.Web04.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity>:IBaseRepository<MISAEntity> where MISAEntity : class
    {
        // khởi tạo các kết nối tới database
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        private string _className;
        public BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("NVTan");
            SqlConnection = new MySqlConnection(ConnectionString);
            _className = typeof(MISAEntity).Name;
        }

        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Arrays</returns>
        public virtual IEnumerable<MISAEntity> GetAll() => GetAll(null);
        
        public virtual IEnumerable<MISAEntity> GetAll(string? storeName = null)
        {
            if(storeName == null)
            {
                storeName = $"Proc_Get{_className}s";
            }
            var entities = SqlConnection.Query<MISAEntity>(storeName, commandType: System.Data.CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">id object</param>
        /// <returns>object</returns>       
        public virtual MISAEntity GetById(Guid id)
        {
            var storeName = $"Proc_Get{_className}ById";
            // lấy parameters
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@m_{_className}Id", id);
            // . Trả dữ liệu về
            var res = SqlConnection.QueryFirstOrDefault<MISAEntity>(storeName, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Int</returns>
        public virtual int Insert(MISAEntity entity)
        {
            //  lấy dữ liệu từ database
            var storeName = $"proc_Add{_className}";
            // . trả dữ liệu về 
            var rowEffects = SqlConnection.Execute(storeName, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            return rowEffects;
        }

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Int</returns>
        public virtual int Update(MISAEntity entity)
        {
            //  lấy dữ liệu từ database
            var storeName = $"Proc_Update{_className}";
            // . trả dữ liệu về 
             var rowEffects = SqlConnection.Execute(storeName, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            return rowEffects;
        }

        /// <summary>
        /// Xoá đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Int</returns>
        public virtual int Delete(Guid id)
        {
            //  Lấy dữ liệu từ database
            var storeName = $"Proc_Delete{_className}";
            //  lấy parameters
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@m_{_className}Id", id);
            // . Trả dữ liệu về 
            var res = SqlConnection.Execute(storeName, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }
    }
   
}
