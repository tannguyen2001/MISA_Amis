using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace MISA.Web04.Infrastructure.Repository
{
    /// <summary>
    /// Kế thừa từ cha
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration):base(configuration){}
    }
}
