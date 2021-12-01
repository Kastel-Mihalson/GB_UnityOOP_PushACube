public abstract class PlayerSettings
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";

    public const float SPEED_BONUS_TIME = 3f;
    public const float MOVE_SPEED = 5f;

    protected float moveSpeed = MOVE_SPEED;
    protected float moveSpeedBonusTime = SPEED_BONUS_TIME;
    protected float turnSpeed = 2f;
    protected float maxHealth = 100;
}
