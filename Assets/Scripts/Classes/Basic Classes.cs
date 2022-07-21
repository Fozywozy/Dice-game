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
        return C_Input > 0 ? 1 : C_Input < 0 ? -1 : 0;
    }

    public static bool VectorBetween(Vector3 C_Point, Vector3 C_Between1, Vector3 C_Between2)
    {
        return ((C_Point.x < C_Between1.x && C_Point.x > C_Between2.x) || (C_Point.x > C_Between1.x && C_Point.x < C_Between2.x)) &&
               ((C_Point.y < C_Between1.y && C_Point.y > C_Between2.y) || (C_Point.y > C_Between1.y && C_Point.y < C_Between2.y)) &&
               ((C_Point.z < C_Between1.z && C_Point.z > C_Between2.z) || (C_Point.z > C_Between1.z && C_Point.z < C_Between2.z));
    }
}
