using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Interfaces;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;

namespace MISA.Web04.Infrastructure.Repository
{
    /// <summary>
    /// Kế thừa từ cha
    /// </summary>
    public class PositionRepository:BaseRepository<Position>,IPositionRepository
    {

        public PositionRepository(IConfiguration configuration):base(configuration){}
    }
}
