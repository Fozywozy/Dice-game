using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryExitNode : MonoBehaviour
{
    public IONode TileData;

    [SerializeField]
    private Material Ring;

    private void Start()
    {
        transform.position = TileData.Position;
        transform.localScale = TileData.Scale;
    }

    private void Update()
    {
        float Percent = Time.fixedUnscaledTime * 0.2f % 1;

        int index = 0;
        foreach (Transform T in transform)
        {
            T.localPosition = new Vector3(0, index * 0.2f + 0.475f - (0.8f * Percent), 0);

            if (T.localPosition.y < 0.45f)
            {
                T.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                T.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            Color NewColor = new Color(1, 1, 1);
            float NewA = (1.275f - T.localPosition.y) * 0.4f;
            if (NewA < 0) { NewA = 0; }
            NewColor.a = NewA;
            T.GetComponent<MeshRenderer>().material.color = NewColor;

            index++;
        }
    }
}
