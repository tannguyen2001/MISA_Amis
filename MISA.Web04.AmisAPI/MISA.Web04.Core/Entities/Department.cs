using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    /// <summary>
    /// Phòng ban
    /// CreatedBy: Nguyễn Văn Tân(10/08/2022)
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

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
