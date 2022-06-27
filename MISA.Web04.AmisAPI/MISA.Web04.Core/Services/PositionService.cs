using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces;

namespace MISA.Web04.Core.Services
{
    public class PositionService : BaseService<Position>,IPositionService
    {
        IPositionRepository _repository;

        public PositionService(IPositionRepository repository):base(repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Validate insert vị trí
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateInsert(Position position)
        {
            // validate dữ liệu
            var isValid = true;
            // Bắt buộc phải nhập tên
            if (string.IsNullOrEmpty(position.PositionName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_PositionNotEmpty);
            }

            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Validate update vị trí
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>Boolean</returns>
        protected override bool ValidateUpdate(Position position)
        {
            // validate dữ liệu
            var isValid = true;
            // Bắt buộc phải nhập tên
            if (string.IsNullOrEmpty(position.PositionName))
            {
                ErrorValidateMsg.Add(Resources.ResourceVN.VN_ValidateError_PositionNotEmpty);
            }

            if (ErrorValidateMsg.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
