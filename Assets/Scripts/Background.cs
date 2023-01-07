using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private BackgroundType Mode;

    //Boxy
    public float VelBoundry = 0.1f;
    public float ScaleRandom = 5f;
    private Vector3 RotationVelocity = new Vector3(0, 0, 0);
    private Vector3 RotationAcceleration = new Vector3(0, 0, 0);

    private Timer SwitchTimer = null;

    public void SetMode(BackgroundType C_NewMode)
    {
        if (Mode != C_NewMode)
        {
            SwitchTimer = new Timer(1f, 0.25f);
            Mode = C_NewMode;
        }
    }

    private void LateUpdate()
    {
        //Rotate Boxy
        RotationAcceleration += ScaleRandom * Time.deltaTime * new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);

        RotationVelocity += RotationAcceleration;

        if (RotationVelocity.x > VelBoundry) { RotationVelocity.x = VelBoundry; }
        if (RotationVelocity.x < -VelBoundry) { RotationVelocity.x = -VelBoundry; }

        if (RotationVelocity.y > VelBoundry) { RotationVelocity.y = VelBoundry; }
        if (RotationVelocity.y < -VelBoundry) { RotationVelocity.y = -VelBoundry; }

        if (RotationVelocity.z > VelBoundry) { RotationVelocity.z = VelBoundry; }
        if (RotationVelocity.z < -VelBoundry) { RotationVelocity.z = -VelBoundry; }
        transform.Rotate(RotationVelocity);

        //Fade Boxy
        if (SwitchTimer != null)
        {
            float Opacity = (Mode == BackgroundType.Boxy) ? SwitchTimer : 1 - SwitchTimer;

            Color NewColor = GetComponent<MeshRenderer>().material.color;
            NewColor.a = Opacity;
            GetComponent<MeshRenderer>().material.color = NewColor;

            if (SwitchTimer)
            {
                SwitchTimer = null;
            }
        }
    }
}
