using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    /// <summary>
    /// Vị trí
    /// CreatedBy: Nguyễn Văn Tân(10/08/2022)
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Được tạo vào
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Được tạo vào
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
