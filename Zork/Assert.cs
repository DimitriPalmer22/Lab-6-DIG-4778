using System.Diagnostics;

namespace Zork;

public static class Assert
{
    [Conditional("DEBUG")]
    public static void IsTrue(bool condition, string message = null)
    {
        if (condition == false)
            throw new Exception(message);
    }
}