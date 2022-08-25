using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float VelBoundry = 0.1f;
    public float ScaleRandom = 5f;

    private Vector3 RotationVelocity = new Vector3(0, 0, 0);
    private Vector3 RotationAcceleration = new Vector3(0, 0, 0);

    void Update()
    {
        RotationAcceleration += ScaleRandom * Time.deltaTime * new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);

        RotationVelocity += RotationAcceleration;

        if (RotationVelocity.x > VelBoundry) { RotationVelocity.x = VelBoundry; }
        if (RotationVelocity.x < -VelBoundry) { RotationVelocity.x = -VelBoundry; }

        if (RotationVelocity.y > VelBoundry) { RotationVelocity.y = VelBoundry; }
        if (RotationVelocity.y < -VelBoundry) { RotationVelocity.y = -VelBoundry; }

        if (RotationVelocity.z > VelBoundry) { RotationVelocity.z = VelBoundry; }
        if (RotationVelocity.z < -VelBoundry) { RotationVelocity.z = -VelBoundry; }

        transform.Rotate(RotationVelocity);
    }
}
