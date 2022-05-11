using AutoMapper;
using Gooapp.Autenticacao.Data.Dtos;
using Gooapp.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;

namespace Gooapp.Autenticacao.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDTO, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
   
        }
    }
}
