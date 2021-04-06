using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IRoleScreenAccessRepo
    {
        Metadata<RoleScreenViewModel> get(long RoleID);
    }
    public class RoleScreenAccessRepo : IRoleScreenAccessRepo
    {
        private readonly SimpamDBContext _context;

        public RoleScreenAccessRepo(SimpamDBContext dBContext)
        {
            _context = dBContext;
        }

        public Metadata<RoleScreenViewModel> get(long RoleID)
        {
            Metadata<RoleScreenViewModel> metadata = new Metadata<RoleScreenViewModel>();
            metadata.data.RoleID = RoleID;
            var result = _context.Screen
                .GroupJoin(
                    _context.RoleScreen,
                    s => s.ScreenID,
                    rs => rs.ScreenID,
                    (s, rs) => new { s, rs = rs }
                    )
                .SelectMany(
                x => x.rs.DefaultIfEmpty(),
                (x, y) => new ScreenComponentViewModel
                {
                    ScreenID = x.s.ScreenID,
                    ScreenName = x.s.ScreenName,
                    DeleteFlag = x.rs.FirstOrDefault().DeleteFlag,
                    WriteFlag = x.rs.FirstOrDefault().WriteFlag,
                    ReadFlag = x.rs.FirstOrDefault().ReadFlag,
                    
                })
                .ToList();
            metadata.status = true;
            metadata.data.Screens = result;
            return metadata;
        }
    }
}
