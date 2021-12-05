public class PlayerModel
{
    public float oldSpeed;
    public float moveSpeed;
    public float turnSpeed;
    public float moveSpeedBonusTime;
    public float oldMoveSpeedBonusTime;

    public string HORIZONTAL = "Horizontal";
    public string VERTICAL = "Vertical";

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public float MoveSpeedBonusTime
    {
        get => moveSpeedBonusTime;
        set => moveSpeedBonusTime = value;
    }

    public PlayerModel()
    {
        moveSpeed = 5f;
        oldSpeed = moveSpeed;
        turnSpeed = 2f;
        moveSpeedBonusTime = 5f;
        oldMoveSpeedBonusTime = moveSpeedBonusTime;
    }
}