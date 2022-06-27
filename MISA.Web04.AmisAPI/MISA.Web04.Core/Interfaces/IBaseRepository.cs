using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Array</returns>
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>Entity</returns>
        MISAEntity GetById(Guid id);

        /// <summary>
        ///    Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>int</returns>
        int Insert(MISAEntity entity);

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>int</returns>
        int Update(MISAEntity entity);

        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>int</returns>
        int Delete(Guid id);
    }
}
