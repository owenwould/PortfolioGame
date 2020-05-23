using System.Collections;

using System;

public static class distance 
{
    public static bool isntTooClose(float val,float val2)
    {
       float distance =  Math.Abs(val - val2);

        if (distance < constants.firendlyDetectionRange)
            return false;
        else
            return true;
    }
}
    
