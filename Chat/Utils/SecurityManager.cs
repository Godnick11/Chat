using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Chat.Utils
{
  public class SecurityManager : ISecurityManager
  {
    private readonly SHA512Managed _hashAlgorithm;

    public SecurityManager()
    {
      _hashAlgorithm = new SHA512Managed();
    }

    public byte[] GenerateNewSalt()
    {
      var result = Guid.NewGuid().ToByteArray();
      return result;
    }

    public byte[] ComputeHash(byte[] salt, string password)
    {
      var encodedPassword = Encoding.UTF8.GetBytes(password);
      var inputSequence = salt.Concat(encodedPassword).ToArray();
      var result = _hashAlgorithm.ComputeHash(inputSequence);
      return result;
    }

    public bool IsPasswordValid(byte[] salt, string passwordToCheck, byte[] expectedHash)
    {
      var passwordHash = ComputeHash(salt, passwordToCheck);
      var result = Enumerable.SequenceEqual(passwordHash, expectedHash);
      return result;
    }
  }
}