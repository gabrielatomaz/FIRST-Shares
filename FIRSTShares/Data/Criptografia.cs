using CryptSharp;

namespace FIRSTShares.Data
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
