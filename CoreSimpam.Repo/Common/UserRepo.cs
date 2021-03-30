using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using CryptSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface IUserRepo
    {
        Task<Metadata<UserViewModel>> GetAll();
        Task<Metadata<UserViewModel>> GetByID(long UserID);
        Task<Metadata> Insert(UserViewModel model);
        Task<Metadata> Update(UserViewModel model);
        Task<Metadata> Delete(long UserID);
        Task<Metadata<UserViewModel>> Validate(string username, string password);
    }
    public class UserRepo : IUserRepo
    {
        private readonly SimpamDBContext context;

        public UserRepo(SimpamDBContext dBContext)
        {
            context = dBContext;
        }
        private async Task<Metadata> ValidateUserAsync(string username, string password)
        {
            var userModel = await GetUserAsync(username);
            if (userModel == null) return new Metadata() { status = false, data = "Username not found" };
            string salt = userModel.Salt;
            string pass = userModel.Password;
            if (!string.Equals(pass, HashedPassword(password, salt)))
                if (userModel == null) return new Metadata() { status = false, data = "Password Incorrect" };
            return new Metadata() { status = true, data = userModel.UserID.ToString() };
        }
        private string HashedPassword(string password, string salt) => Crypter.Blowfish.Crypt(password, salt);
        private async Task<UserModel> GetUserAsync(string username) => await context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        private string GenerateSalt(int number = 10) => Crypter.Blowfish.GenerateSalt(number);

        public async Task<Metadata<UserViewModel>> GetAll()
        {
            Metadata<UserViewModel> res = new Metadata<UserViewModel>();
            res.data.users = await context.Users
                .Join(
                    context.Roles,
                    u => u.RoleID,
                    r => r.RoleID,
                    (u, r) => new UserViewModel()
                    {
                        Email = u.Email,
                        UserID = u.UserID,
                        UserName = u.UserName,
                        RoleID = u.RoleID,
                        RoleName = r.RoleName
                    })
                .Where(x => x.RoleID > 1)
                .ToListAsync();
            res.status = true;
            return res;
        }

        public async Task<Metadata<UserViewModel>> GetByID(long UserID)
        {
            Metadata<UserViewModel> res = new Metadata<UserViewModel>();
            var data = await context.Users.FirstOrDefaultAsync(x => x.UserID == UserID);
            var dataRole = await context.Roles.FirstOrDefaultAsync(x => x.RoleID == data.RoleID);            
            if (data != null)
            {
                res.status = true;
                res.data.UserID = data.UserID;
                res.data.Email = data.Email;
                res.data.UserName = data.UserName;
                res.data.RoleID = data.RoleID;
                res.data.RoleName = dataRole.RoleName;
            }
            return res;
        }

        public async Task<Metadata> Insert(UserViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var dataUSer = await context.Users.AnyAsync(x => x.UserName.Contains(model.UserName) && x.UserID != model.UserID);
                if (dataUSer)
                    return new Metadata() { status = false, data = "User name is ready" };
                string saltNow = GenerateSalt();
                await context.Users.AddAsync(new UserModel()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Salt = saltNow,
                    Password = HashedPassword(model.Password, saltNow),
                    RoleID = model.RoleID
                });
                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully inserted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata> Update(UserViewModel model)
        {
            Metadata res = new Metadata();
            try
            {
                var data = await context.Users.AnyAsync(x => x.UserName.Contains(model.UserName) && x.UserID != model.UserID);
                if (data)
                    return new Metadata() { status = false, data = "User name is ready" };
                var dataUSer = await context.Users.Where(x => x.UserID == model.UserID).FirstOrDefaultAsync();
                if (dataUSer == null)
                    return new Metadata() { status = false, data = "User not found" };

                dataUSer.Email = model.Email;
                dataUSer.RoleID = model.RoleID;

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully updated";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata> Delete(long UserID)
        {
            Metadata res = new Metadata();
            try
            {
                var dataUSer = await context.Users.Where(x => x.UserID == UserID).FirstOrDefaultAsync();
                if (dataUSer == null)
                    return new Metadata() { status = false, data = "User not found" };

                context.Users.Remove(dataUSer);

                await context.SaveChangesAsync();
                res.status = true;
                res.data = "Data was successfully deleted";
            }
            catch (Exception ex)
            {
                res.status = false;
                res.data = ex.Message;
            }

            return res;
        }

        public async Task<Metadata<UserViewModel>> Validate(string username, string password)
        {
            Metadata<UserViewModel> res = new Metadata<UserViewModel>();
            var data = await ValidateUserAsync(username, password);
            if (data.status) res.data = (await GetByID(Convert.ToInt64(data.data))).data;
            res.status = data.status;
            return res;
        }
    }
}
