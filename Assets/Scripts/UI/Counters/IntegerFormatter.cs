using UnityEngine;

public static class IntegerFormatter
{
    public static readonly char TIME_SEPARATOR = ':';
    public static readonly char CURRENCY_SEPARATOR = '.';

    public static readonly int SECONDS_PER_MINUTE = 60;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time">Time in seconds</param>
    /// <returns>Returns formatted time in string</returns>
    public static string GetTime(int time)
    {
        string result = "";

        int minutes = time / SECONDS_PER_MINUTE;
        int seconds = (time - minutes * SECONDS_PER_MINUTE) % SECONDS_PER_MINUTE ;

        result += minutes.ToString() + TIME_SEPARATOR;

        if (seconds < 10)
        {
            result += "0" + seconds.ToString();
        }
        else
        {
            result += seconds.ToString();
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currency"></param>
    /// <returns>Returns formatted currency value in string</returns>
    public static string GetCurrency(int currency)
    {
        string result = "";

        return result;
    }
}
