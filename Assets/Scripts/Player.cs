using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject Board => GameObject.FindGameObjectWithTag("Move board");
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    private int RotateSpeed = 3;

    private Vector3 RotationChange;
    private Vector3 PositionChange;

    private Vector3 PredictedPosition => transform.position + PositionChange;

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
                transform.position = Vector3Int.RoundToInt(transform.position);
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
                Vector3 NewPosition = transform.position;
                transform.position = NewPosition;
                CheckStandingOn();
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
            if (!CheckCollision(Vector3Int.RoundToInt(PredictedPosition + new Vector3(0, 1, 0))))
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition - Board.transform.forward)))
                {
                    break;
                }
                else
                {
                    PositionChange -= Board.transform.forward;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RotationChange = Board.transform.right * 90;
            if (!CheckCollision(Vector3Int.RoundToInt(PredictedPosition + new Vector3(0, 1, 0))))
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition + Board.transform.forward)))
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.forward;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            RotationChange = Board.transform.forward * 90;
            if (!CheckCollision(Vector3Int.RoundToInt(PredictedPosition + new Vector3(0, 1, 0))))
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition - Board.transform.right)))
                {
                    break;
                }
                else
                {
                    PositionChange -= Board.transform.right;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RotationChange = Board.transform.forward * -90;
            if (!CheckCollision(Vector3Int.RoundToInt(PredictedPosition + new Vector3(0, 1, 0))))
            {
                PositionChange = new Vector3(0, 1, 0);
            }
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition + Board.transform.right)))
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.right;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition - Board.transform.forward)))
                {
                    break;
                }
                else
                {
                    PositionChange -= Board.transform.forward;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition + Board.transform.forward)))
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.forward;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition - Board.transform.right)))
                {
                    break;
                }
                else
                {
                    PositionChange -= Board.transform.right;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 1; i <= SideUp; i++)
            {
                if (CheckCollision(Vector3Int.RoundToInt(PredictedPosition + Board.transform.right)))
                {
                    break;
                }
                else
                {
                    PositionChange += Board.transform.right;
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
        if (LevelManager.LevelData.TileAtPosition.ContainsKey(Vector3Int.RoundToInt(PredictedPosition - new Vector3(0, 1, 0))))
        {
            //Tile beneath the player
            switch (LevelManager.LevelData.TileAtPosition[Vector3Int.RoundToInt(PredictedPosition - new Vector3(0, 1, 0))].Type)
            {
                case TileType.Scaffold:
                    BackToCheckpoint(false, false);
                    break;
            }
        }
        else if (LevelManager.LevelData.NodeAtPosition.ContainsKey(Vector3Int.RoundToInt(PredictedPosition - new Vector3(0, 1, 0))))
        {
            //Node beneath the player
            Debug.Log("Finished");
            LevelManager.LoadLevel(LevelManager.LevelData.NodeAtPosition[Vector3Int.RoundToInt(PredictedPosition - new Vector3(0, 1, 0))]);
        }
        else
        {
            //No tile or node beneath the player
            PositionChange -= new Vector3(0, 1, 0);
        }

        if (LevelManager.LevelData.NodeAtPosition.ContainsKey(Vector3Int.RoundToInt(PredictedPosition)))
        {

        }
    }

    /// <summary>
    /// Returns true if a physical tile exists at the position
    /// </summary>
    public bool CheckCollision(Vector3Int C_Position)
    {
        if (LevelManager.LevelData.TileAtPosition.ContainsKey(C_Position))
        {
            return LevelManager.LevelData.TileAtPosition[C_Position].Solid;
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
}
