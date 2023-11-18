using AutoMapper;
using DexefTask.BL.DTOS;
using DexefTask.BL.DTOS.Identity;
using DexefTask.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.Mapping
{
    public class MapperClass : Profile
    {
        
        public MapperClass() 
        {
            CreateMap<DexefUser,DexefUserDto>()
                .ForPath(des => des.UserName, src => src.MapFrom(src => src.UserName))
                .ForPath(des => des.Image, src => src.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<DexefUser, ReadDexefUserDto>()
                //.ForPath(des => des.UserName, src => src.MapFrom(src => src.UserName))
                //.ForPath(des => des.Image, src => src.MapFrom(src => src.Image))
                .ReverseMap();


            CreateMap<DexefUser, UpdateDexefUserDto>()
                
                .ForPath(des => des.UserName, src => src.MapFrom(src => src.UserName))
                .ForPath(des => des.Email, src => src.MapFrom(src => src.Email))
                .ForPath(des => des.Image, src => src.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<DexefUser,RegisterUserDto>()
                .ForPath(des => des.UserName, src => src.MapFrom(src => src.UserName))
                .ForPath(des => des.Email, src => src.MapFrom(src => src.Email))
                .ForPath(des => des.Image, src => src.MapFrom(src => src.Image))
                .ForPath(des => des.CreatedAt, src => src.MapFrom(src => src.CreatedAt))
                .ForPath(des => des.Address, src => src.MapFrom(src => src.Address))
                .ForPath(des => des.JobTitle, src => src.MapFrom(src => src.JobTitle))
                .ForPath(des => des.Salary, src => src.MapFrom(src => src.Salary))

                .ForPath(des => des.Password, src => src.MapFrom(src => src.PasswordHash))
                .ReverseMap();





        }

    }
}
