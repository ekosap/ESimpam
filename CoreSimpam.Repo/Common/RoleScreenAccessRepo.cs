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
            var result = (from s in _context.Screen
                          join rs in _context.RoleScreen on new { screenID = s.ScreenID, roleID = RoleID } equals new { screenID = rs.ScreenID, roleID = rs.RoleID } into screenRole
                          from srs in screenRole.DefaultIfEmpty()
                          select new ScreenComponentViewModel
                          {
                              RoleID = srs == null ? 0 : srs.RoleID,
                              ScreenID = s.ScreenID,
                              ScreenName = s.ScreenName,
                              DeleteFlag = srs == null ? false : srs.DeleteFlag,
                              WriteFlag = srs == null ? false : srs.WriteFlag,
                              ReadFlag = srs == null ? false : srs.ReadFlag,
                          }).ToList();
            metadata.status = true;
            metadata.data.Screens = result;
            return metadata;
        }
    }
}
