public static class Debug
{
    public static void Log(string message)
    {
        System.Diagnostics.Debug.WriteLine(message);
    }

    public static void LogWarning(string message)
    {
        System.Diagnostics.Debug.WriteLine("WARNING:" + message);
    }
}
