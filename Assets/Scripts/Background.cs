using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private BackgroundType Mode;

    private GameObject BoxyObject => transform.GetChild(0).gameObject;
    private GameObject MainCamera => GameObject.FindGameObjectWithTag("MainCamera");

    //Boxy
    public float VelBoundry = 0.1f;
    public float ScaleRandom = 5f;
    private Vector3 RotationVelocity = new Vector3(0, 0, 0);
    private Vector3 RotationAcceleration = new Vector3(0, 0, 0);

    public void SetMode(BackgroundType C_NewMode)
    {
        Mode = C_NewMode;
        switch (C_NewMode)
        {
            case BackgroundType.Boxy:
                transform.SetParent(MainCamera.transform);

                break;
            case BackgroundType.Sky:
                transform.SetParent(null);

                break;
        }
    }

    private void LateUpdate()
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
                BoxyObject.transform.Rotate(RotationVelocity);

                break;
            case BackgroundType.Sky:

                break;
        }
    }
}
