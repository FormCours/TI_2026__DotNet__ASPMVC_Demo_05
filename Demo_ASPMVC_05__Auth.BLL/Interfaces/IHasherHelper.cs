namespace Demo_ASPMVC_05__Auth.BLL.Interfaces
{
    public interface IHasherHelper
    {
        // Helper dédié au Hashage, permet d'isolé l'algo de hash utilisé.
        // Exemple de solution possible : Argon2, PBKDF2, Bcrypt, ...

        string Hash(string value);
        bool Verify(string hash, string value);
    }
}
