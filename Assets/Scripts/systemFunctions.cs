using System.Collections;

using System;

public static class systemFunctions 
{
    public static bool isntTooClose(float val,float val2)
    {
       float distance =  Math.Abs(val - val2);

        if (distance < constants.firendlyDetectionRange)
            return false;
        else
            return true;
    }

    public static string getTimerString(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);


        string str = time.ToString(@"mm\:ss");
        return str;
    }
}
    
