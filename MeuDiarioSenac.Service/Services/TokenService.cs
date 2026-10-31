using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Service;

public class TokenService
{
    private readonly IConfiguration configuracao;

    public TokenService(IConfiguration configuracao)
    {
        this.configuracao = configuracao;
    }

    public string GerarToken(Usuario usuario)
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracao["Jwt:Key"]!));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim("nome", usuario.Nome)
        };

        var expiracaoMinutos = int.Parse(configuracao["Jwt:ExpiracaoMinutos"]!);

        var token = new JwtSecurityToken(
            issuer: configuracao["Jwt:Issuer"],
            audience: configuracao["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiracaoMinutos),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
