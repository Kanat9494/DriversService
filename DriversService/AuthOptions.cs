namespace DriversService;

public class AuthOptions
{
    public const string ISSUER = "AuthServer"; // издатель токена
    public const string AUDIENCE = "AuthClient"; // потребитель токена
    const string KEY = "#2klm;LKEOK23)*($_#@OPWM,fmklsdp2_)_(*ASDFMK=345KLMFS_2348#^#%%^*(&$%#L;KSJDFG";   // ключ для шифрации
    public const int LIFETIME = 1; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
