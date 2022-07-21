using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryExitNode : MonoBehaviour
{
    public SceneTile TileData;
    public int Level;
    public bool CanEnter;

    [SerializeField]
    private Material Ring;

    void Update()
    {
        float Percent = Time.fixedUnscaledTime * 0.8f % 1;

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
