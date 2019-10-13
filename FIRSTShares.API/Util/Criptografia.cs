using CryptSharp;

namespace FIRSTShares.API.Util
{
    public static class Criptografia
    {
        public static string Codifica(string senha)
        {
            return Crypter.Blowfish.Crypt(senha);
        }

        public static bool Compara(string senha, string hash)
        {
            return Crypter.CheckPassword(hash, senha);
        }
    }
}
