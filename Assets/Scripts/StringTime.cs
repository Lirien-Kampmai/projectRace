using System;

public static class StringTime
{
    public static string SecondToTimeString(float sec)
    {
        return TimeSpan.FromSeconds(sec).ToString(@"mm\:ss\.ff");
    }
}
