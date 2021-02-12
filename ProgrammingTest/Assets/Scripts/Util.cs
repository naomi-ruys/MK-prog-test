using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public struct MyTime
    {
        public float second, milliSecond;
        public int hour, minute;

        public MyTime(int h, int m, float s, float ms)
        {
            hour = h;
            minute = m;
            second = s;
            milliSecond = ms;
        }

        public MyTime(int h, int m, float s)
        {
            hour = h;
            minute = m;
            second = s;
            milliSecond = 0;
        }

        public bool TimeIsZero()
        {
            if(hour > Clock.MinimumHMS || minute > Clock.MinimumHMS || second > Clock.MinimumHMS)
            {
                return false;
            }

            return true;
        }
    }

    public static string DoubleDigit(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }
}
