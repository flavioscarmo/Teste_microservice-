using AutoMapper;
using FluentResults;
using Gooapp.Autenticacao.Data.Dtos;
using Gooapp.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace Gooapp.Autenticacao.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, 
            UserManager<IdentityUser<int>> userManager, 
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Result> CadastraUsuario(CreateUsuarioDTO createDto)
        {
            var usuarioJaExiste = await _userManager.FindByEmailAsync(createDto.Email);
            if (usuarioJaExiste!= null)
                return Result.Fail("Usuario já existe");


            Usuario usuario = _mapper.Map<Usuario>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            
            IdentityResult resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);


            if(!resultadoIdentity.Succeeded)
                return Result.Fail("Falha ao cadastrar usuário: " + resultadoIdentity.Errors.FirstOrDefault().Description);

            await _userManager.AddToRoleAsync(usuarioIdentity, "user");

            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email },
                    "Link de Ativação", usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");

        }

        public async Task<Result> AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao);
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário: " + identityResult.Errors.FirstOrDefault().Description);
        }
    }
}

