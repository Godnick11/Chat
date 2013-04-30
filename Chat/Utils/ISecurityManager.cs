namespace Chat.Utils
{
  public interface ISecurityManager
  {
    byte[] GenerateNewSalt();
    byte[] ComputeHash(byte[] salt, string password);
    bool IsPasswordValid(byte[] salt, string passwordToCheck, byte[] expectedHash);
  }
}