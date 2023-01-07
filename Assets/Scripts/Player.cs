using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject Board => GameObject.FindGameObjectWithTag("Move board");
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();
    private ExitBeam ExitBeam => GameObject.FindGameObjectWithTag("Exit beam").GetComponent<ExitBeam>();
    private TextboxManager TextboxManager => GameObject.FindGameObjectWithTag("Textbox Manager").GetComponent<TextboxManager>();

    private PlayerMovementManager MovementManager;

    private List<SceneTile> CurrentlyOn = new List<SceneTile>();
    private List<SceneTile> CurrentlyIn = new List<SceneTile>();

    public bool Updated => MovementManager.Movements.Count == 0;
    public int SideUp = 1;


    private void Start()
    {
        MovementManager = new PlayerMovementManager();
        CheckSide();
    }


    private void Update()
    {
        if (Updated)
        {
            if (GetComponent<MeshRenderer>().enabled)
            {
                CheckInputs();
            }
        }
        else
        {
            //Move
            Board.GetComponent<MeshRenderer>().enabled = false;
            PlayerMovement Movement = MovementManager.Movements[0];

            if (!Movement.Started)
            {
                Movement.Start(transform.position, transform.eulerAngles);
            }

            if (Movement.Progress >= 1)
            {
                transform.position = Movement.StartPosition + Movement.PositionChange;

                transform.eulerAngles = Movement.StartRotation;
                transform.Rotate(Movement.RotationChange, Space.World);

                if (MovementManager.Movements.Count > 1)
                {
                    MovementManager.Movements[1].Start(transform.position, transform.eulerAngles);
                    MovementManager.Movements.RemoveAt(0);
                    Movement = MovementManager.Movements[0];
                }
                else
                {
                    MovementManager.Movements.RemoveAt(0);
                    Board.transform.position = transform.position - (Vector3.up * 0.9f);
                    Board.GetComponent<MeshRenderer>().enabled = true;
                    CheckSide();
                    ManagePlayerOnIn();
                    return;
                }
            }

            switch (Movement.Type)
            {
                case MovementType.Slide:
                    if (Movement.PositionChange != null && Movement.PositionChange != Vector3Int.zero)
                    {
                        transform.position = Movement.StartPosition;
                        transform.position += (Vector3)Movement.PositionChange * Movement.Progress;
                    }
                    break;

                case MovementType.Dash:
                    if (Movement.PositionChange != null && Movement.PositionChange != Vector3Int.zero)
                    {
                        transform.position = Movement.StartPosition;
                        transform.position += (Vector3)Movement.PositionChange * Movement.Progress;
                    }
                    //Do particles here as well
                    break;

                case MovementType.VoidJump:
                    transform.position = Movement.StartPosition;
                    transform.position = Movement.JumpFunction();

                    transform.eulerAngles = Movement.StartRotation;
                    transform.Rotate(Movement.RotationChange * Movement.Progress, Space.World);
                    break;

                case MovementType.VoidSlide:
                    transform.position = Movement.StartPosition;
                    transform.position = Movement.VoidSlideFunction();

                    transform.eulerAngles = Movement.StartRotation;
                    transform.Rotate(Movement.RotationChange * Movement.Progress, Space.World);
                    break;

                case MovementType.Restart:
                    MovementManager.Movements = new List<PlayerMovement>();
                    BackToCheckpoint();
                    break;

                case MovementType.RotateAround:
                    transform.position = Movement.StartPosition;
                    transform.RotateAround(Movement.RotateAround, Vector3.up, Movement.RotationChange.y * Movement.Progress);
                    transform.position += new Vector3(0, (Movement.PositionChange.y * Movement.Progress) + Movement.StartPosition.y - transform.position.y, 0);

                    transform.eulerAngles = Movement.StartRotation;
                    transform.Rotate(90 * Movement.Progress * Vector3.up, Space.World);
                    break;
            }
        }
    }


    /// <summary>
    /// Checks keyboard inputs
    /// </summary>
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BackToCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.forward * -SideUp), Vector3Int.RoundToInt(Board.transform.right * -90));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.forward * SideUp), Vector3Int.RoundToInt(Board.transform.right * 90));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.right * -SideUp), Vector3Int.RoundToInt(Board.transform.forward * 90));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.right * SideUp), Vector3Int.RoundToInt(Board.transform.forward * -90));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.forward * -SideUp));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.forward * SideUp));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.right * -SideUp));
            ManagePlayerOffOut();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementManager.NewActions(Vector3Int.RoundToInt(Board.transform.right * SideUp));
            ManagePlayerOffOut();
        }
    }


    /// <summary>
    /// Checks the side that is up of the dice
    /// </summary>
    private void CheckSide()
    {
        foreach (Transform T in transform)
        {
            if ((T.position.y - transform.position.y) > 0 && Mathf.Abs(T.position.y - transform.position.y) > 0.1f)
            {
                SideUp = int.Parse(T.name);
            }
        }
    }


    /// <summary>
    /// Manages player on and in functions
    /// </summary>
    public void ManagePlayerOnIn()
    {
        List<SceneTile> StandingOn = LevelManager.LevelData.TilesAt(Vector3Int.RoundToInt(transform.position - new Vector3(0, 1, 0)));
        List<SceneTile> StandingIn = LevelManager.LevelData.TilesAt(Vector3Int.RoundToInt(transform.position));

        //Switch through the tile the player is in
        if (StandingIn != null)
        {
            foreach (SceneTile Tile in StandingIn)
            {
                switch (Tile.Type)
                {
                    case TileType.Scaffold:
                        BackToCheckpoint();
                        return;
                    case TileType.Collectable:
                        CurrentlyIn.Add(Tile);
                        Tile.Parent.GetComponent<CollectableScript>().PlayerIn();
                        break;
                    case TileType.DangerTile:
                        CurrentlyIn.Add(Tile);
                        Tile.Parent.GetComponent<TimerScript>().PlayerIn();
                        break;
                    case TileType.PushTile:
                        CurrentlyIn.Add(Tile);
                        Tile.Parent.GetComponent<TimerScript>().PlayerIn();
                        break;
                    case TileType.TextPoint:
                        TextboxManager.UpdateVisuals(((TextPoint)Tile).Text, ((TextPoint)Tile).TextImage);
                        break;
                    case TileType.Checkpoint:
                        LevelManager.LastPosition = Vector3Int.RoundToInt(transform.position);
                        break;
                }
            }
        }

        //Switch through tile beneth the player
        if (StandingOn != null)
        {
            foreach (SceneTile Tile in StandingOn)
            {
                switch (Tile.Type)
                {
                    case TileType.PushPiston:
                        MovementManager.Movements.Add(new PlayerMovement(MovementType.Slide, Vector3Int.up * Tile.Scale.y));
                        Tile.Parent.GetComponent<PistonScript>().PlayerOn();
                        CurrentlyOn.Add(Tile);
                        break;

                    case TileType.PivotPiston:
                        transform.RotateAround(Tile.Position, Vector3.up, 90);
                        Vector3Int Position = Vector3Int.RoundToInt(transform.position);
                        Position.y += Tile.Scale.y;
                        transform.RotateAround(Tile.Position, Vector3.up, -90);
                        MovementManager.Movements.Add(new PlayerMovement(Position - Vector3Int.RoundToInt(transform.position), Vector3.up * 90, Tile.Position));
                        Tile.Parent.GetComponent<PistonScript>().PlayerOn();
                        CurrentlyOn.Add(Tile);
                        break;

                    case TileType.IONode:
                        CurrentlyOn.Add(Tile);

                        LevelManager.LoadLevel(((IONode)Tile).LevelTo, ((IONode)Tile).PositionTo);
                        break;

                    case TileType.SuperNode:

                        CurrentlyOn.Add(Tile);
                        break;
                }
            }
        }
    }


    /// <summary>
    /// Manages player off and out functions 
    /// </summary>
    public void ManagePlayerOffOut()
    {
        foreach (SceneTile Tile in CurrentlyOn)
        {
            switch (Tile.Type)
            {
                case TileType.PushPiston:
                    Tile.Parent.GetComponent<PistonScript>().PlayerOff();
                    break;
                case TileType.PivotPiston:
                    Tile.Parent.GetComponent<PistonScript>().PlayerOff();
                    break;
                case TileType.IONode:
                    break;
                case TileType.SuperNode:
                    break;
            }
        }

        foreach (SceneTile Tile in CurrentlyIn)
        {
            switch (Tile.Type)
            {
                case TileType.Collectable:
                    Tile.Parent.GetComponent<CollectableScript>().PlayerOut();
                    break;
                case TileType.DangerTile:
                    Tile.Parent.GetComponent<TimerScript>().PlayerOut();
                    break;
                case TileType.PushTile:
                    Tile.Parent.GetComponent<TimerScript>().PlayerOut();
                    break;
            }
        }

        CurrentlyOn = new List<SceneTile>();
        CurrentlyIn = new List<SceneTile>();
    }


    public void TimerCompleted(TriggerableTimerComplete C_Trigger)
    {
        switch (C_Trigger.TileData.Type)
        {
            case TileType.DangerTile:
                //C_Trigger.TileData.Parent.transform.position += Vector3.up;
                BackToCheckpoint();
                break;
            case TileType.PushTile:
                MovementManager.Movements.Add(new PlayerMovement(MovementType.Slide, ((PushTile)C_Trigger.TileData).Directions[C_Trigger.Index]));
                ManagePlayerOffOut();
                break;
        }
    }


    /// <summary>
    /// Returns true if a physical tile exists at the position
    /// </summary>
    public bool CheckCollision(Vector3Int C_Position)
    {
        if (LevelManager.LevelData.TileAtPosition.Dictionary.ContainsKey(C_Position))
        {
            List<SceneTile> StandingOn = LevelManager.LevelData.TilesAt(Vector3Int.RoundToInt(C_Position));

            foreach (SceneTile Data in StandingOn)
            {
                if (Data.Solid())
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void BackToCheckpoint(bool C_LongWait = false)
    {
        ExitBeam.ExitAt(transform.position, C_LongWait);
        SideUp = 1;
        transform.eulerAngles = Vector3.zero;

        ManagePlayerOffOut();
        CurrentlyOn = new List<SceneTile>();
        CurrentlyIn = new List<SceneTile>();
    }


    private class PlayerMovementManager
    {
        private Vector3Int PlayerPosition => Vector3Int.RoundToInt(Player.transform.position);
        private Player Player => GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        public List<PlayerMovement> Movements = new List<PlayerMovement>();


        public void NewActions(Vector3Int C_PositionChange, Vector3Int? C_RotationChange = null)
        {

            Movements = new List<PlayerMovement>();

            Vector3Int CurrentPosition = PlayerPosition;
            Vector3Int EndPosition = PlayerPosition + C_PositionChange;
            Vector3Int MoveDirection = Vector3Int.RoundToInt(((Vector3)ToMove()).normalized);

            Vector3Int ToMove()
            {
                return new Vector3Int(EndPosition.x - CurrentPosition.x, 0, EndPosition.z - CurrentPosition.z);
            }

            bool Jump = C_RotationChange != null;

        MovementBeginning:
            if (ToMove().magnitude == 0)
            {
                //No movement left
                goto Falling;
            }
            else
            {
                if (Jump)
                {
                    int NearestPit = int.MaxValue;
                    for (int I = 1; I <= ToMove().magnitude; I++)
                    {
                        if (!Player.CheckCollision(CurrentPosition + I * MoveDirection + Vector3Int.down))
                        {
                            NearestPit = I;
                            break;
                        }
                    }

                    int NearestWall = int.MaxValue;
                    for (int I = 1; I <= ToMove().magnitude; I++)
                    {
                        if (Player.CheckCollision(CurrentPosition + I * MoveDirection))
                        {
                            NearestWall = I;
                            break;
                        }
                    }

                    int NearestCeil = int.MaxValue;
                    for (int I = 1; I <= ToMove().magnitude; I++)
                    {
                        if (Player.CheckCollision(CurrentPosition + I * MoveDirection + Vector3Int.up))
                        {
                            NearestCeil = I;
                            break;
                        }
                    }

                    //Check for long jump
                    if (NearestPit == int.MaxValue && NearestWall == int.MaxValue && NearestCeil == int.MaxValue)
                    {
                        Movements.Add(new PlayerMovement(MovementType.VoidJump, ToMove(), C_RotationChange.Value));
                        CurrentPosition += ToMove();
                        goto Falling;
                    }

                    //Check for jump into a ceiling
                    if ((NearestCeil != int.MaxValue) && (NearestWall >= NearestCeil) && ((NearestPit == 1) || (NearestPit > NearestCeil)))
                    {
                        Movements.Add(new PlayerMovement(MovementType.VoidJump, MoveDirection * (NearestCeil - 1) + Vector3Int.up, C_RotationChange.Value));
                        CurrentPosition += MoveDirection * (NearestCeil - 1) + Vector3Int.up;
                        goto Falling;
                    }

                    //Check for jump onto wall
                    if (((NearestPit == 1) || (NearestPit > NearestWall)) && (NearestCeil > NearestWall) && (NearestWall != int.MaxValue))
                    {
                        Movements.Add(new PlayerMovement(MovementType.VoidJump, MoveDirection * NearestWall + Vector3Int.up, C_RotationChange.Value));
                        CurrentPosition += MoveDirection * NearestWall + Vector3Int.up;
                        Jump = false;
                        goto MovementBeginning;
                    }

                    //Check for move forward
                    if (NearestPit != 1 && NearestWall != 1)
                    {
                        Movements.Add(new PlayerMovement(MovementType.Slide, MoveDirection));
                        CurrentPosition += MoveDirection;
                        goto MovementBeginning;
                    }

                    //Check for long jump
                    if (NearestPit == 1 && NearestWall == int.MaxValue && NearestCeil == int.MaxValue)
                    {
                        Movements.Add(new PlayerMovement(MovementType.VoidJump, ToMove(), C_RotationChange.Value));
                        CurrentPosition += ToMove();
                        goto Falling;
                    }

                    //Void jump
                    Movements.Add(new PlayerMovement(MovementType.VoidJump, Vector3Int.down * 5 + ToMove(), C_RotationChange.Value));
                    Movements.Add(new PlayerMovement(MovementType.Restart));
                    return;
                }
                else
                {
                    //Check for wall
                    if (Player.CheckCollision(CurrentPosition + MoveDirection))
                    {
                        //Wall in the way
                        goto Falling;
                    }

                    //Check for pit
                    if (Player.CheckCollision(CurrentPosition + MoveDirection + Vector3Int.down))
                    {
                        //No pit
                        Movements.Add(new PlayerMovement(MovementType.Dash, MoveDirection));
                        CurrentPosition += MoveDirection;
                        goto MovementBeginning;
                    }

                    //Pit in the way, check for tile or wall to slide to
                    for (int I = 1; I <= ToMove().magnitude; I++)
                    {
                        //Hit a wall
                        if (Player.CheckCollision(CurrentPosition + I * MoveDirection))
                        {
                            Movements.Add(new PlayerMovement(MovementType.Dash, MoveDirection * (I - 1)));
                            CurrentPosition += MoveDirection * (I - 1);
                            goto Falling;
                        }

                        //Found a spot to slide to
                        if (Player.CheckCollision(CurrentPosition + I * MoveDirection + Vector3Int.down))
                        {
                            Movements.Add(new PlayerMovement(MovementType.Dash, MoveDirection * I));
                            CurrentPosition += MoveDirection * I;
                            goto MovementBeginning;
                        }
                    }

                    //Void slide (or death void slide)
                    for (int IDown = 1; IDown <= 10; IDown++)
                    {
                        if (Player.CheckCollision(CurrentPosition + ToMove() + Vector3Int.down * IDown))
                        {
                            if (IDown != 1)
                            {
                                Movements.Add(new PlayerMovement(MovementType.Slide, MoveDirection));
                                Movements.Add(new PlayerMovement(MovementType.VoidSlide, MoveDirection * Mathf.RoundToInt(ToMove().magnitude - 1) + Vector3Int.down * (IDown - 1)));
                            }
                            return;
                        }
                    }

                    Movements.Add(new PlayerMovement(MovementType.Slide, MoveDirection));
                    Movements.Add(new PlayerMovement(MovementType.VoidSlide, Mathf.RoundToInt(((Vector3)ToMove()).magnitude - 1) * MoveDirection + Vector3Int.down * 5));
                    Movements.Add(new PlayerMovement(MovementType.Restart));
                    return;
                }
            }

        Falling:
            for (int I = 1; I <= 10; I++)
            {
                if (Player.CheckCollision(CurrentPosition + Vector3Int.down * I))
                {
                    if (I != 1)
                    {
                        Movements.Add(new PlayerMovement(MovementType.Slide, Vector3Int.down * (I - 1)));
                    }
                    return;
                }
            }
            Movements.Add(new PlayerMovement(MovementType.Slide, Vector3Int.down * 5));
            Movements.Add(new PlayerMovement(MovementType.Restart));
        }
    }


    private class PlayerMovement
    {
        public Vector3 StartPosition;
        public Vector3 StartRotation;
        private float StartTime;
        private float TimeScale = 5;

        public bool Started;
        public float Progress => (Time.fixedUnscaledTime - StartTime) * TimeScale;
        public MovementType Type;
        public Vector3Int PositionChange = Vector3Int.zero;
        public Vector3Int SideDirection => new Vector3Int(PositionChange.x, 0, PositionChange.z);
        public Vector3 RotationChange = Vector3.zero;
        public Vector3 RotateAround = Vector3.zero;

        public PlayerMovement(Vector3Int C_Pos, Vector3 C_Rot, Vector3 C_RotAround)
        {
            Type = MovementType.RotateAround;
            PositionChange = C_Pos;
            RotationChange = C_Rot;
            RotateAround = C_RotAround;
        }

        public PlayerMovement(MovementType C_Type, Vector3Int C_Pos, Vector3 C_Rot)
        {
            Type = C_Type;
            PositionChange = C_Pos;
            RotationChange = C_Rot;
            TimeScale = 5f / Mathf.RoundToInt(C_Pos.magnitude);
        }

        public PlayerMovement(MovementType C_Type, Vector3Int C_Pos)
        {
            Type = C_Type;
            PositionChange = C_Pos;
            TimeScale = 5f / Mathf.RoundToInt(C_Pos.magnitude);
        }

        public PlayerMovement(MovementType C_Type)
        {
            Type = C_Type;
        }

        public void Start(Vector3 C_StartPosition, Vector3 C_StartRotation)
        {
            StartPosition = C_StartPosition;
            StartRotation = C_StartRotation;
            StartTime = Time.fixedUnscaledTime;
            Started = true;
        }

        public Vector3 JumpFunction()
        {
            return (Vector3.up * (((4 + Mathf.Pow(16 - 16 * PositionChange.y, 0.5f)) / 2) * Progress - 0.25f * Mathf.Pow(((4 + Mathf.Pow(16 - 16 * PositionChange.y, 0.5f)) / 2) * Progress, 2))) + ((Vector3)SideDirection * Progress) + StartPosition;
        }

        public Vector3 VoidSlideFunction()
        {
            bool SideDirectionExists = float.IsNaN((((Vector3)SideDirection) * Progress).x);
            bool DownDirectionExists = float.IsNaN(Mathf.Pow(Progress, 2) * PositionChange.y);
            Vector3 Output = (DownDirectionExists ? Vector3.zero : (Vector3.up * (Mathf.Pow(Progress, 2) * PositionChange.y))) + StartPosition + (SideDirectionExists ? Vector3.zero : ((Vector3)SideDirection) * Progress);
            return Output;
        }
    }


    private enum MovementType
    {
        Slide,
        Dash,
        VoidJump,
        VoidSlide,
        Restart,
        RotateAround,
    }
}
