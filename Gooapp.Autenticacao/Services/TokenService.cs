using Gooapp.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Gooapp.Autenticacao.Services
{
    public class TokenService
    {
        //private static RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        //private static readonly string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "secret.json");
        public Token CreateToken(IdentityUser<int> usuario, string role)
        {
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }


        //public static Token GenerateJWT(IdentityUser<int> usuario, string role)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Loadkey();

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim("username", usuario.UserName),
        //            new Claim("id", usuario.Id.ToString()),
        //            new Claim(ClaimTypes.Role, role)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(2),
        //        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //Console.WriteLine(tokenHandler.WriteToken(token));


        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        //    return new Token(tokenString);
        //}

        //private static byte[] GenerateKey(int bytes)
        //{
        //    var data = new byte[bytes];
        //    Rng.GetBytes(data);
        //    return data;
        //}

        //private static SecurityKey Loadkey()
        //{
        //    if (File.Exists(MyJwkLocation))
        //        return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

        //    var newKey = CreateJWK();
        //    File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
        //    return newKey;
        //}

        //private static JsonWebKey CreateJWK()
        //{
        //    var symetricKey = new HMACSHA256(GenerateKey(64));
        //    var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
        //    jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
        //    return jwk;
        //}


    }
}
