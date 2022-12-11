using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    private Color StartColor = new Color(0.32f, 0.4f, 0.4f, 1);
    private Color EndColor;

    private void Update()
    {
        RenderSettings.fogColor = Color.Lerp(StartColor, EndColor, TimerManager.GetTimer("FogLerp"));

        if (TimerManager.GetTimer("FogLerp"))
        {
            enabled = false;
            StartColor = EndColor;
        }
    }

    public void LerpTo(Color C_NewColor, float C_TimeTaken)
    {
        TimerManager.NewTimer("FogLerp", C_TimeTaken);
        EndColor = C_NewColor;
        enabled = true;
    }
}
