namespace NorthWind.UserManager.Presenter;

public class LoginPresenter : ILoginPresenter
{
    // Token estara en un formato JWT
    public string Token { get; private set; }

    readonly IConfigurationSection JWTConfigurationSection;

    public LoginPresenter(IConfigurationSection jWTConfigurationSection)
    {
        JWTConfigurationSection = jWTConfigurationSection;
    }

    public ValueTask Handle(UserDto userDto)
    {
        SigningCredentials signingCredentials = GetSigningCredentials();
        List<Claim> userClaims = GetClaims(userDto);
        JwtSecurityToken tokenOptions = GetTokenOptions(signingCredentials, userClaims);
        Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return ValueTask.CompletedTask;
    }

    private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> userClaims) =>
        new JwtSecurityToken(issuer: JWTConfigurationSection["ValidIssuer"], audience: JWTConfigurationSection["ValidAudience"],
            claims: userClaims, expires: DateTime.Now.AddMinutes(Convert.ToDouble(JWTConfigurationSection["ExpireInMinutes"])), 
            signingCredentials: signingCredentials);

    private List<Claim> GetClaims(UserDto userDto) => new List<Claim>()
    {
        new Claim(ClaimTypes.Name,userDto.Email)
    };

    private SigningCredentials GetSigningCredentials()
    {
        byte[] key = Encoding.UTF8.GetBytes(JWTConfigurationSection["SecurityKey"]);
        SymmetricSecurityKey secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
}
