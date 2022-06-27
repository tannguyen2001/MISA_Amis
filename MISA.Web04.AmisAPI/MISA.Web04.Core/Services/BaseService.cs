using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class BaseService<MISAEntity>:IBaseService<MISAEntity>
    {
        protected IBaseRepository<MISAEntity> Repository;
        protected List<string> ErrorValidateMsg;
        public BaseService(IBaseRepository<MISAEntity> repository)
        {
            Repository = repository;
            ErrorValidateMsg = new List<string>();
        }

        /// <summary>
        /// Nghiệp vụ thêm mới
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Int</returns>
        /// <exception cref="MISAValidateException">error</exception>
        public int InsertService(MISAEntity entity)
        {  // thực hiện validate dữ liệu
            var isValid = ValidateInsert(entity);

            if (isValid)
            {
                var res = Repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(Resources.ResourceVN.VN_DataNotValidate, ErrorValidateMsg);
            }
        }

        /// <summary>
        /// Nghiệp vụ cập nhật
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateException">error</exception>
        public int UpdateService(MISAEntity entity)
        {
            // thực hiện validate dữ liệu
            var isValid = ValidateUpdate(entity);

            if (isValid)
            {
                var res = Repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAValidateException(Resources.ResourceVN.VN_DataNotValidate, ErrorValidateMsg);
            }
           
        }

        /// <summary>
        /// hàm validate khi insert
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Boolean</returns>
        protected virtual bool ValidateInsert(MISAEntity entity)
        {
            return true;
        }    

        /// <summary>
        /// Hàm validate khi update
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Boolean</returns>
        protected virtual bool ValidateUpdate(MISAEntity entity)
        {
            return true;
        }
    }
}
