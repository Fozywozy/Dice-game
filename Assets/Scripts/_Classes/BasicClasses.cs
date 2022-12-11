using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperClass
{
    public static Vector3 VectorToSign(Vector3 C_Input)
    {
        Vector3 Output;

        Output.x = C_Input.x > 0 ? 1 : C_Input.x < 0 ? -1 : 0;
        Output.y = C_Input.y > 0 ? 1 : C_Input.y < 0 ? -1 : 0;
        Output.z = C_Input.z > 0 ? 1 : C_Input.z < 0 ? -1 : 0;

        return Output;
    }

    public static int NumToSign(float C_Input)
    {
        return (int)Mathf.Pow(C_Input, 0);
    }

    public static bool VectorBetween(Vector3 C_Point, Vector3 C_Between1, Vector3 C_Between2)
    {
        return ((C_Point.x < C_Between1.x && C_Point.x > C_Between2.x) || (C_Point.x > C_Between1.x && C_Point.x < C_Between2.x)) &&
               ((C_Point.y < C_Between1.y && C_Point.y > C_Between2.y) || (C_Point.y > C_Between1.y && C_Point.y < C_Between2.y)) &&
               ((C_Point.z < C_Between1.z && C_Point.z > C_Between2.z) || (C_Point.z > C_Between1.z && C_Point.z < C_Between2.z));
    }
}


public class HelperDirection
{
    public float Distance;
    public int IDistance => Mathf.RoundToInt(Distance);
    public Vector3 Direction;
    public Vector3Int IDirection => Vector3Int.RoundToInt(Direction);

    public Vector3 GetVector => Direction * Distance;
    public Vector3Int IGetVector => IDirection * IDistance;

    public HelperDirection(Vector3 C_Input)
    {
        Direction = C_Input.normalized;
        Distance = C_Input.magnitude;
    }
}


/// <summary>
/// A dictionary that stores a list of values for one key
/// </summary>
public class DictionaryList<TOne, TTwo>
{
    public Dictionary<TOne, List<TTwo>> Dictionary = new Dictionary<TOne, List<TTwo>>();

    /// <summary>
    /// Returns true if C_Key exists, returns false if C_Key doesn't exist
    /// </summary>
    public bool Add(TOne C_Key, TTwo C_Input)
    {
        bool Output;
        if (Dictionary.ContainsKey(C_Key))
        {
            //Key exists
            List<TTwo> NewList = Dictionary[C_Key];
            NewList.Add(C_Input);
            Dictionary.Add(C_Key, NewList);
            Output = true;
        }
        else
        {
            //Key doesn't exist
            List<TTwo> NewList = new List<TTwo> { C_Input };
            Dictionary.Add(C_Key, NewList);
            Output = false;
        }
        return Output;
    }

    /// <summary>
    /// Returns null if C_Key doesn't exist, returns false if the list doesn't contain C_Input, returns true if the list does contain C_Input
    /// </summary>
    public bool? Remove(TOne C_Key, TTwo C_Input)
    {
        bool? Output = null;
        if (Dictionary.ContainsKey(C_Key))
        {
            List<TTwo> NewList = Dictionary[C_Key];
            Output = NewList.Remove(C_Input);
            Dictionary.Add(C_Key, NewList);
        }

        return Output;
    }
}


/// <summary>
/// A dictionary that uses a hashset
/// </summary>
public class DictionaryHashSet<TOne, TTwo>
{
    public Dictionary<TOne, HashSet<TTwo>> Dictionary = new Dictionary<TOne, HashSet<TTwo>>();

    /// <summary>
    /// Returns null if C_Key doesn't exist, returns true if the hashset doesn't contain C_Input, returns false if the hashset does contain C_Input
    /// </summary>
    public bool? Add(TOne C_Key, TTwo C_Input)
    {
        bool? Output;
        if (Dictionary.ContainsKey(C_Key))
        {
            //Key exists
            HashSet<TTwo> NewOutput = Dictionary[C_Key];
            Output = NewOutput.Add(C_Input);
            Dictionary.Add(C_Key, NewOutput);
        }
        else
        {
            //No key
            HashSet<TTwo> NewOutput = new HashSet<TTwo> { C_Input };
            Dictionary.Add(C_Key, NewOutput);
            Output = null;
        }

        return Output;
    }

    /// <summary>
    /// Returns null if C_Key doesn't exist, returns true if the hashset contains C_Input, returns false if the hashset doesn;t contain C_Input
    /// </summary>
    public bool? Remove(TOne Key, TTwo Input)
    {
        bool? Output = null;
        if (Dictionary.ContainsKey(Key))
        {
            HashSet<TTwo> NewOutput = Dictionary[Key];
            Output = NewOutput.Remove(Input);
            Dictionary.Add(Key, NewOutput);
        }

        return Output;
    }
}


public class TimerList
{
    private int TimerIndexIterate;
    public int TimerIndex => TimerIndexIterate % Timers.Count;

    public List<TimerListSegmant> Timers = new List<TimerListSegmant>();

    public TimerList(List<TimerListSegmant> C_Timers)
    {
        Timers = C_Timers;
    }

    public bool Update(bool C_Activate)
    {
        float RemainingTime = Time.deltaTime;
        bool Output = false;

        while (RemainingTime > 0)
        {
            TimerListSegmant Timer = Timers[TimerIndex];

            if (Timer.TimeRemaining <= RemainingTime)
            {
                RemainingTime -= Timer.TimeRemaining;
                Timer.TimePassed = 0;
                TimerIndexIterate++;

                Timer.Triggerable.Trigger(C_Activate);
                Output = true;
            }
            else
            {
                Timer.TimePassed += RemainingTime;
                RemainingTime = 0;
            }
        }
        return Output;
    }

    public TimerListSegmant GetCurrentSegmant()
    {
        return Timers[TimerIndex];
    }

    public class TimerListSegmant
    {
        public float Percent => TimePassed / TimeFinish;
        public float TimeRemaining => TimeFinish - TimePassed;

        public TriggerableClass Triggerable;
        public float TimePassed = 0;
        public float TimeFinish;

        public TimerListSegmant(TriggerableClass C_Triggerable, float C_TimeFinish)
        {
            Triggerable = C_Triggerable;
            TimeFinish = C_TimeFinish;
        }
    }
}

public class TriggerableClass
{
    public virtual void Trigger(bool C_Active)
    {
        Debug.Log("Trigger Unintended");
    }
}

public class TriggerableTimerComplete : TriggerableClass
{
    public TimerScript Timer;
    public SceneTile TileData;
    public int Index;

    private Player PlayerScript => GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    public override void Trigger(bool C_Active)
    {
        if (C_Active)
        {
            PlayerScript.TimerCompleted(this);
        }
        Timer.PlayAnimation();
    }
}

public class LerpOnLoop
{
    public float LoopStart;
    public float LoopEnd;

    public float LoopSize => Mathf.Abs(LoopStart - LoopEnd);

    public LerpOnLoop(float C_Start, float C_End)
    {
        LoopStart = C_Start;
        LoopEnd = C_End;
    }

    /// <summary>
    /// Percent goes between 0 and 1
    /// </summary>
    public float LerpBetween(float C_Start, float C_End, float C_Percent)
    {
        C_Start = RestrictNumberWithinBounds(C_Start);
        C_End = RestrictNumberWithinBounds(C_End);

        if (Mathf.Abs(C_Start - C_End) < LoopSize / 2)
        {
            //Take direct path
            float Difference = C_End - C_Start;
            return C_Start + (Difference * C_Percent);
        }
        else
        {
            //Take indirect path
            float Difference = C_End > C_Start
                ? C_End + LoopStart - LoopEnd - C_Start
                : C_End + C_Start - LoopEnd - LoopStart;

            return RestrictNumberWithinBounds((Difference * C_Percent) + C_Start);
        }
    }

    public float RestrictNumberWithinBounds(float C_Input)
    {
        while (C_Input < LoopStart) { C_Input += LoopSize; }
        while (C_Input > LoopEnd) { C_Input -= LoopSize; }
        return C_Input;
    }
}
