using AutoMapper;
using DexefTask.BL.DTOS;
using DexefTask.BL.DTOS.Identity;
using DexefTask.BL.IReposatories;

using DexefTask.DAL.Context;
using DexefTask.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.Reposatories
{
    public class DexefUserRepo : IDexefUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager <DexefUser> _userManager;

        int pageSize = 5;
        public DexefUserRepo(ApplicationDbContext context , IMapper mapper , IWebHostEnvironment webHost , UserManager<DexefUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _webHost = webHost;
            _userManager = userManager;

        }

        public async Task DeleteDexefUser(Guid id)
        {
            var userToDeleteFromDB = await _userManager.FindByIdAsync(id.ToString());
            if(userToDeleteFromDB == null)
            {
                throw new Exception("NOT FOUND !!");
            }
            else
            {
               await _userManager.DeleteAsync(userToDeleteFromDB);
               await SaveChanges();
            }

        }

        public async Task<IEnumerable<ReadDexefUserDto>> GetAll()
        {
            var AllUserNotMapped = await _userManager.Users.ToListAsync();
            var MappedUserList = new List<ReadDexefUserDto>();
            
            foreach (var user in AllUserNotMapped)
            {
                MappedUserList
                    .Add(_mapper.Map<ReadDexefUserDto>(user));
            }
            return MappedUserList;

            
        }

        public async Task<ReadDexefUserDto> GetById(Guid id)
        {
            var DexefUser = await _userManager.FindByIdAsync(id.ToString());
            if(DexefUser == null)
            {
                throw new Exception("User Not Found !");
            }
            else
            {
               
                return _mapper.Map<ReadDexefUserDto>(DexefUser);
            }
            
        }

        public async Task<IEnumerable<ReadDexefUserDto>> GetDexefUserByName(string UserName)
        {
            var NotMappedList = await _userManager.Users.Where(user=>user.UserName!.ToLower().Contains(UserName.ToLower())).ToListAsync();
            if(NotMappedList != null)
            {
                var MappedUserList = new List<ReadDexefUserDto>();

                foreach (var user in NotMappedList)
                {
                    MappedUserList
                        .Add(_mapper.Map<ReadDexefUserDto>(user));
                }
                return MappedUserList;
            }
            else
            {
                return null;
            }
            
            
        }
        
        public async Task<IEnumerable<ReadDexefUserDto>> GetDexefUserForPage(int pageNumber)
        {
            
            var NotMappedUserWithPageList = await _userManager.Users.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
            var MappedUserList = new List<ReadDexefUserDto>();

            foreach (var user in NotMappedUserWithPageList)
            {
                MappedUserList
                    .Add(_mapper.Map<ReadDexefUserDto>(user));
            }
            return MappedUserList;
        }

        public async Task<IEnumerable<ReadDexefUserDto>> GetDexefUserForPageWithName(int pageNumber, string UserName)
        {
            var NotMpapedUsersWithPageNumberByName =
                await _userManager.Users
                                  .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                  .Where(user=>user.UserName!.ToLower().Contains(UserName.ToLower()))
                                  .ToListAsync();

            var MappedUserList = new List<ReadDexefUserDto>();

            foreach (var user in NotMpapedUsersWithPageNumberByName)
            {
                MappedUserList
                    .Add(_mapper.Map<ReadDexefUserDto>(user));
            }
            return MappedUserList;
        }



        public async Task<IEnumerable<ReadDexefUserDto>> PagedList(int? pageNumber, string? UserName)
        {
            #region old
            //var AllUserNotMapped = await _userManager.Users.ToListAsync();
            //var MappedUserList = new List<ReadDexefUserDto>();
            //int pageSize = 10;

            //if (pageNumber == null && UserName == null)
            //{

            //    foreach (var user in AllUserNotMapped)
            //    {
            //        MappedUserList
            //            .Add(_mapper.Map<ReadDexefUserDto>(user));
            //    }

            //}
            //DexefUser ReachedUsers = new DexefUser();

            //if (pageNumber != null && UserName != null)
            //{


            //    // var UserListWithSpeceficName = new List<DexefUserDto>();


            //    foreach (var UserWithName in AllUserNotMapped)
            //    {
            //        if (UserWithName.UserName!.IndexOf(UserName, StringComparison.OrdinalIgnoreCase) >= 0)
            //        {
            //            ReachedUsers = UserWithName;
            //        }
            //        MappedUserList
            //            .Add
            //            (_mapper.Map<ReadDexefUserDto>(ReachedUsers));
            //    }
            //    MappedUserList = Pagination<ReadDexefUserDto>.create(MappedUserList.AsQueryable(), pageNumber ?? 1, pageSize);


            //}

            //if (UserName != null && pageNumber == null)
            //{
            //    //var UserListWithSpeceficName = new List<DexefUserDto>();

            //    foreach (var UserWithName in AllUserNotMapped)
            //    {
            //        if (UserWithName.UserName!.IndexOf(UserName, StringComparison.OrdinalIgnoreCase) >= 0)
            //        {
            //            ReachedUsers = UserWithName;
            //        }
            //        MappedUserList
            //            .Add
            //            (_mapper.Map<ReadDexefUserDto>(ReachedUsers));
            //    }

            //}


            //if (UserName == null && pageNumber != null)
            //{
            //    MappedUserList = Pagination<ReadDexefUserDto>.create(MappedUserList.AsQueryable(), pageNumber ?? 1, pageSize);

            //}

            //return MappedUserList;
            #endregion

            if(pageNumber == null && UserName != null)
            {
               return await GetDexefUserByName(UserName);

            }
            else if(pageNumber != null && UserName == null)
            {

               return await GetDexefUserForPage(pageNumber ?? 1);

            }
            else if(pageNumber != null && UserName != null)
            {

               return await GetDexefUserForPageWithName(pageNumber ?? 1, UserName);

            }
            else
            {

                return await GetAll();

            }


        }

        public async Task RegisterDexefUser(RegisterUserDto registerUserDto)
        {
            var CheckedEmail = await _userManager.FindByEmailAsync(registerUserDto.Email);
            if (CheckedEmail != null)
            {
                throw new Exception("Email must be unique");
            }
            else
            {
                var uploadPath = Path.Combine(_webHost.WebRootPath, "Images");
                var imageName = Guid.NewGuid().ToString() + "_" + registerUserDto.Image.FileName;
                var filePath = Path.Combine(uploadPath, imageName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await registerUserDto.Image.CopyToAsync(fileStream);
                }
                var DexefUser = new DexefUser
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email,
                    Address = registerUserDto.Address,
                    JobTitle = registerUserDto.JobTitle,
                    Salary = registerUserDto.Salary,
                    Image = imageName,
                    CreatedAt = registerUserDto.CreatedAt
                    


                };

                var CreateResult = await _userManager.CreateAsync(DexefUser, registerUserDto.Password);
                if (CreateResult.Succeeded)
                {
                    var ClaimList = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,DexefUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier,DexefUser.Id),
                    new Claim(ClaimTypes.Role,"Admin")

                };
                    await _userManager.AddClaimsAsync(DexefUser, ClaimList);

                }
                else
                {
                    throw new Exception("Adding Failed");
                }
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDexefUser(UpdateDexefUserDto updateDexefUser, Guid id)
        {
            var UpdatedUserFromDB = await _userManager.FindByIdAsync(id.ToString());
            if (UpdatedUserFromDB == null)
            {
                throw new Exception("user you wanted to update not found !!");
            }
            else
            {
                UpdatedUserFromDB.PasswordHash = _userManager.PasswordHasher.HashPassword(UpdatedUserFromDB,updateDexefUser.Password);
              // await _userManager.ChangePasswordAsync(UpdatedUserFromDB, UpdatedUserFromDB.PasswordHash!, updateDexefUser.Password);
                _mapper.Map(updateDexefUser, UpdatedUserFromDB);
               // updateDexefUser.PasswordHash(UpdatedUserFromDB.PasswordHash);
                await _userManager.UpdateAsync(UpdatedUserFromDB);
                
                await SaveChanges();
            }
        }
    }
}
