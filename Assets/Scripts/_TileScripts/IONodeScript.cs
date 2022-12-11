using UnityEngine;

public class IONodeScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public IONode TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (IONode)C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();
        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }

    private void Update()
    {
        transform.GetChild(1).transform.localPosition -= new Vector3(0, Time.deltaTime * 0.2f, 0);

        if (transform.GetChild(1).transform.localPosition.y < 0.2f)
        {
            transform.GetChild(1).transform.localPosition = new Vector3(0, 1.2f, 0);
        }

        float Percent = 1.2f - transform.GetChild(1).transform.localPosition.y;

        int Index = 0;
        foreach (Transform T in transform.GetChild(1))
        {
            Color NewColor = new Color(1, 1, 1);
            float NewA = (Index * 0.1f) + (Percent * 0.5f) - 0.5f;
            if (NewA > 1) { NewA = 1; }
            if (NewA < 0) { NewA = 0; }
            NewColor.a = NewA;
            T.GetComponent<MeshRenderer>().material.color = NewColor;

            Index++;
        }

        if (transform.GetComponent<TileLoader>().LoadingOut)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }

        if (!transform.GetComponent<TileLoader>().LoadingOut)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void PlayerOn()
    {

    }

    public void PlayerOff()
    {

    }

    public void PlayerIn()
    {

    }

    public void PlayerOut()
    {

    }
}
