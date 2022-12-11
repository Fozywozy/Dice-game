using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBrick
{
    public float Time;
    public float Alpha;
    public Vector3 Scale;
    public Vector3 Position;
    public Vector3 Rotation;

    public AnimationBrick(float C_Time, float C_Alpha, float C_XPos, float C_YPos, float C_ZPos, float C_XRotation, float C_YRotation, float C_ZRotation, float C_XScale = 1, float C_YScale = 1, float C_ZScale = 1)
    {
        Time = C_Time;
        Alpha = C_Alpha;
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Rotation = new Vector3(C_XRotation, C_YRotation, C_ZRotation);
        Scale = new Vector3(C_XScale, C_YScale, C_ZScale);
    }
}

public class AnimationBrickList
{
    private List<AnimationBrick> Bricks = new List<AnimationBrick>();
    private float TimeScale = 1;

    public AnimationBrickList(List<AnimationBrick> C_Bricks, float C_TimeScale = 1)
    {
        Bricks = C_Bricks;
        TimeScale = C_TimeScale;
    }

    public Vector3 Position(float C_Time)
    {
        C_Time *= TimeScale;
        AnimationBrick Min = Bricks[0];
        AnimationBrick Max = Bricks[0];

        int Index = 0;
        foreach (AnimationBrick Brick in Bricks)
        {
            if (Brick.Time > C_Time)
            {
                Max = Bricks[Index];
                Min = Index - 1 >= 0 ? Bricks[Index - 1] : Bricks[Index];
                break;
            }
            Index++;
        }

        float CurrentValue = C_Time - Min.Time;
        float TotalValue = Max.Time == Min.Time ? CurrentValue : (Max.Time - Min.Time);
        float Percentage = CurrentValue / TotalValue;
        return Vector3.Lerp(Min.Position, Max.Position, Percentage);
    }

    public Vector3 Rotation(float C_Time)
    {
        C_Time *= TimeScale;
        AnimationBrick Min = Bricks[0];
        AnimationBrick Max = Bricks[0];

        int Index = 0;
        foreach (AnimationBrick Brick in Bricks)
        {
            if (Brick.Time > C_Time)
            {
                Max = Bricks[Index];
                Min = Index - 1 >= 0 ? Bricks[Index - 1] : Bricks[Index];
                break;
            }
            Index++;
        }

        float CurrentValue = C_Time - Min.Time;
        float TotalValue = Max.Time == Min.Time ? CurrentValue : (Max.Time - Min.Time);
        float Percentage = CurrentValue / TotalValue;
        return Vector3.Lerp(Min.Rotation, Max.Rotation, Percentage);
    }

    public Vector3 Scale(float C_Time)
    {
        C_Time *= TimeScale;
        AnimationBrick Min = Bricks[0];
        AnimationBrick Max = Bricks[0];

        int Index = 0;
        foreach (AnimationBrick Brick in Bricks)
        {
            if (Brick.Time > C_Time)
            {
                Max = Bricks[Index];
                Min = Index - 1 >= 0 ? Bricks[Index - 1] : Bricks[Index];
                break;
            }
            Index++;
        }

        float CurrentValue = C_Time - Min.Time;
        float TotalValue = Max.Time == Min.Time ? CurrentValue : (Max.Time - Min.Time);
        float Percentage = CurrentValue / TotalValue;
        return Vector3.Lerp(Min.Scale, Max.Scale, Percentage);
    }

    public float Alpha(float C_Time)
    {
        C_Time *= TimeScale;
        AnimationBrick Min = Bricks[0];
        AnimationBrick Max = Bricks[0];

        int Index = 0;
        foreach (AnimationBrick Brick in Bricks)
        {
            if (Brick.Time > C_Time)
            {
                Max = Bricks[Index];
                Min = Index - 1 >= 0 ? Bricks[Index - 1] : Bricks[Index];
                break;
            }
            Index++;
        }

        float CurrentValue = C_Time - Min.Time;
        float TotalValue = Max.Time == Min.Time ? CurrentValue : (Max.Time - Min.Time);
        float Percentage = CurrentValue / TotalValue;
        return Mathf.Lerp(Min.Alpha, Max.Alpha, Percentage);
    }

    public void SetValues(GameObject C_Object, float C_Time, bool C_Position, bool C_Rotation, bool C_Scale, bool C_Alpha)
    {
        if (C_Position) { C_Object.transform.localPosition = Position(C_Time); }
        if (C_Rotation) { C_Object.transform.localEulerAngles = Rotation(C_Time); }
        if (C_Scale) { C_Object.transform.localScale = Scale(C_Time); }

        if (C_Alpha)
        {
            for (int I = 0; I != C_Object.GetComponent<MeshRenderer>().materials.Length; I++)
            {
                Color NewColor = C_Object.GetComponent<MeshRenderer>().materials[I].color;
                NewColor.a = Alpha(C_Time);
                C_Object.GetComponent<MeshRenderer>().materials[I].color = NewColor;
            }
        }
    }
}
