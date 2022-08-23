using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject Board => GameObject.FindGameObjectWithTag("Move board");
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    private int RotateSpeed = 3;

    private Vector3 RotationChange;
    private Vector3 PositionChange;

    private Vector3Int PredictedPosition => Vector3Int.RoundToInt(transform.position + PositionChange);

    public bool Updated => (PositionChange == Vector3.zero) && (RotationChange == Vector3.zero);

    public int SideUp = 1;

    private void Start()
    {
        CheckSide();
    }

    private void Update()
    {
        if (RotationChange != Vector3.zero)
        {
            Vector3 RotChange = RotateSpeed * 90 * Time.deltaTime * HelperClass.VectorToSign(RotationChange);

            if (Mathf.Abs(RotChange.x) > Mathf.Abs(RotationChange.x)) { RotChange.x = RotationChange.x; }
            if (Mathf.Abs(RotChange.y) > Mathf.Abs(RotationChange.y)) { RotChange.y = RotationChange.y; }
            if (Mathf.Abs(RotChange.z) > Mathf.Abs(RotationChange.z)) { RotChange.z = RotationChange.z; }

            RotationChange = new Vector3(RotationChange.x - RotChange.x, RotationChange.y - RotChange.y, RotationChange.z - RotChange.z);
            transform.Rotate(RotChange, Space.World);

            if (RotationChange == Vector3.zero)
            {
                Vector3 NewRotation = new Vector3(90 * Mathf.RoundToInt(transform.eulerAngles.x / 90f), 90 * Mathf.RoundToInt(transform.eulerAngles.y / 90f), 90 * Mathf.RoundToInt(transform.eulerAngles.z / 90f));
                transform.eulerAngles = NewRotation;
                CheckSide();
            }
        }

        if (PositionChange != Vector3.zero)
        {
            Vector3 PosChange = RotateSpeed * 4 * Time.deltaTime * HelperClass.VectorToSign(PositionChange);

            if (Mathf.Abs(PosChange.x) > Mathf.Abs(PositionChange.x)) { PosChange.x = PositionChange.x; }
            if (Mathf.Abs(PosChange.y) > Mathf.Abs(PositionChange.y)) { PosChange.y = PositionChange.y; }
            if (Mathf.Abs(PosChange.z) > Mathf.Abs(PositionChange.z)) { PosChange.z = PositionChange.z; }

            PositionChange = new Vector3(PositionChange.x - PosChange.x, PositionChange.y - PosChange.y, PositionChange.z - PosChange.z);
            transform.position += PosChange;
            Board.transform.position = transform.position - new Vector3(0, 0.9f, 0);

            if (PositionChange == Vector3.zero)
            {
                Vector3 NewPosition = Vector3Int.RoundToInt(transform.position);
                transform.position = NewPosition;
            }
        }

        if (Updated)
        {
            CheckInputs();
        }
    }


    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotationChange = Board.transform.right * -90;
            if (!CheckCollision(PredictedPosition + new Vector3Int(0, 1, 0)))
            {
                PositionChange = new Vector3(0, 1, 0);
            }

            PositionChange = Board.transform.right * -SideUp;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RotationChange = Board.transform.right * 90;
            if (!LevelManager.LevelData.TileAtPosition[transform.position + new Vector3(0, 1, 0)].Standable)
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i >= SideUp; i++)
            {
                if (LevelManager.LevelData.TileAtPosition[transform.position + (Board.transform.right * i) + new Vector3(0, PositionChange.y, 0)].Standable)
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.right;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RotationChange = Board.transform.forward * 90;
            if (!LevelManager.LevelData.TileAtPosition[transform.position + new Vector3(0, 1, 0)].Standable)
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i >= SideUp; i++)
            {
                if (LevelManager.LevelData.TileAtPosition[transform.position + (Board.transform.forward * i) + new Vector3(0, PositionChange.y, 0)].Standable)
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.forward;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RotationChange = Board.transform.forward * -90;
            if (!LevelManager.LevelData.TileAtPosition[transform.position + new Vector3(0, 1, 0)].Standable)
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i >= SideUp; i++)
            {
                if (LevelManager.LevelData.TileAtPosition[transform.position - (Board.transform.forward * i) + new Vector3(0, PositionChange.y, 0)].Standable)
                {
                    break;
                }
                else
                {
                    PositionChange -= Board.transform.forward;
                }
            }
        }
    }


    private void CheckSide()
    {
        foreach (Transform T in transform)
        {
            if (HelperClass.NumToSign(T.position.y - transform.position.y) == 1 && Mathf.Abs(T.position.y - transform.position.y) > 0.1f)
            {
                SideUp = int.Parse(T.name);
            }
        }
    }


    public void CheckStandingOn()
    {
        if (LevelManager.LevelData.TileAtPosition.ContainsKey(PredictedPosition - new Vector3(0, 1, 0)))
        {
            switch (LevelManager.LevelData.TileAtPosition[PredictedPosition - new Vector3(0, 1, 0)].Type)
            {
                case TileType.Scaffold:
                    BackToCheckpoint(false, false);
                    break;
            }
        }

        if (LevelManager.LevelData.NodeAtPosition.ContainsKey(PredictedPosition - new Vector3(0, 1, 0)))
        {
            LoadLevel(LevelManager.LevelData.NodeAtPosition[PredictedPosition - new Vector3(0, 1, 0)]);
        }

        else
        {
            Vector3Int YPositionChange = new Vector3Int();
            for (int YMod = 0; YMod != 10; YMod++)
            {
                if (CheckCollision(Vector3Int.FloorToInt(PredictedPosition) - new Vector3Int(0, YMod, 0)))
                {
                    //Tile
                    return;
                }
                else
                {
                    //No tile
                    YPositionChange -= new Vector3Int(0, 1, 0);
                }
            }
            BackToCheckpoint(true, false);
        }
    }


    public bool CheckCollision(Vector3Int C_Position)
    {
        if (LevelManager.LevelData.TileAtPosition.ContainsKey(C_Position))
        {
            return LevelManager.LevelData.TileAtPosition[C_Position].Standable;
        }

        if (LevelManager.LevelData.NodeAtPosition.ContainsKey(C_Position))
        {
            return true;
        }
        return false;
    }





    public void BackToCheckpoint(bool C_Wait, bool C_FadeToBlack)
    {

    }

    public void LoadLevel(IONode C_SteppedOn)
    {

    }
}
