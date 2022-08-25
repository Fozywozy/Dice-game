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

    public void SetMode(BackgroundType C_NewMode)
    {
        Mode = C_NewMode;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        switch (Mode)
        {
            case BackgroundType.Boxy:
                RotationAcceleration += ScaleRandom * Time.deltaTime * new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);

                RotationVelocity += RotationAcceleration;

                if (RotationVelocity.x > VelBoundry) { RotationVelocity.x = VelBoundry; }
                if (RotationVelocity.x < -VelBoundry) { RotationVelocity.x = -VelBoundry; }

                if (RotationVelocity.y > VelBoundry) { RotationVelocity.y = VelBoundry; }
                if (RotationVelocity.y < -VelBoundry) { RotationVelocity.y = -VelBoundry; }

                if (RotationVelocity.z > VelBoundry) { RotationVelocity.z = VelBoundry; }
                if (RotationVelocity.z < -VelBoundry) { RotationVelocity.z = -VelBoundry; }

                transform.Rotate(RotationVelocity);
                break;
            case BackgroundType.Clouds:

                break;
        }
    }
}
