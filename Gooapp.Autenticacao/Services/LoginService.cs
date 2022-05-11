using FluentResults;
using Gooapp.Autenticacao.Data.Requests;
using Gooapp.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;

namespace Gooapp.Autenticacao.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Result> LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (resultadoIdentity.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario =>
                    usuario.NormalizedUserName == request.Username.ToUpper());

                if (identityUser == null) return Result.Fail("Usuário ou senha inválidos");

                var role = _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault();

                 Token token = _tokenService.CreateToken(identityUser, role);

                //var token = TokenService.GenerateJWT(identityUser, role);

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            IdentityUser<int> identityUser = RecuperaUsuarioPorEmail(request.Email);

            IdentityResult resultadoIdentity = _signInManager
                .UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (resultadoIdentity.Succeeded) return Result.Ok()
                    .WithSuccess("Senha redefinida com sucesso!");
            return Result.Fail("Houve um erro na operação");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            IdentityUser<int> identityUser = RecuperaUsuarioPorEmail(request.Email);

            if (identityUser != null)
            {
                string codigoDeRecuperacao = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }

        private IdentityUser<int> RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
