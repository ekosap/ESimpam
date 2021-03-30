using CoreSimpam.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IRoleScreenAccessRepo
    {

    }
    public class RoleScreenAccessRepo : IRoleScreenAccessRepo
    {
        private readonly SimpamDBContext dBContext;

        public RoleScreenAccessRepo(SimpamDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
    }
}
