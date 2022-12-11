using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimerManager
{
    private static Dictionary<string, Timer> Timers = new Dictionary<string, Timer>();

    public static void NewTimer(string C_Idenfitier, float C_WaitTime, float C_TimeUntil = 0)
    {
        Timers.Remove(C_Idenfitier);
        Timers.Add(C_Idenfitier, new Timer(C_WaitTime, C_TimeUntil));
    }

    public static Timer GetTimer(string C_Idenfitier)
    {
        return Timers[C_Idenfitier];
    }

    public static void RemoveTimer(string C_Idenfitier)
    {
        _ = Timers.Remove(C_Idenfitier);
    }
}

public class Timer
{
    public float StartTime;
    public int FrameStart;
    public float WaitTime;

    public bool Active => Time.fixedUnscaledTime - StartTime >= 0;
    public float EndTime => StartTime + WaitTime;

    public float TimePassed => Active ? Time.fixedUnscaledTime > EndTime ? WaitTime : Time.fixedUnscaledTime - StartTime : 0;
    public float FramesPassed => Active ? 0 : Time.frameCount - FrameStart;

    public float Percent => TimePassed / WaitTime;
    public bool Finished => TimePassed >= WaitTime;
    public string String => "(Start:" + StartTime + ", Wait:" + WaitTime + ", Active:" + Active + ", TimePassed:" + TimePassed + ")";

    public bool StartFrame => Active && TimePassed <= Time.deltaTime;

    public Timer(float C_WaitTime, float C_TimeUntil = 0)
    {
        StartTime = Time.fixedUnscaledTime + C_TimeUntil;
        FrameStart = Time.frameCount;
        WaitTime = C_WaitTime;
    }

    public static implicit operator bool(Timer C_Timer) => C_Timer.Finished;
    public static implicit operator float(Timer C_Timer) => C_Timer.Percent;
}
