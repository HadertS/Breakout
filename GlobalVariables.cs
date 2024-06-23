using Godot;

public partial class GlobalVariables : Node
{
    // Player variables //

    // EnergyBar is the player's energy bar. It is used to power the player's abilities.
    public double EnergyBar = 100;

    // EnergyBarRegenRate is the rate at which the energy bar regenerates. Not currently implimented
    public double EnergyBarRegenRate = 0.5;

    //EnergyBarRegenRate is the threhold at which powers cannot be used if the energy bar is drained to empty
    public double EnergyBarEmptyThrehold = 50;

    // BallPiercingLevel is the level of piercing the ball has. The higher the level, the more blocks the ball can pierce through.
    public int BallPiercingLevel = 0;

    // BallSpeedLevel is the level of speed the ball has. The higher the level, the faster the ball moves. Not currently implimented.
    public float BallSpeedLevel = 1;

    // BallSizeLevel is the level of size the ball has. The higher the level, the larger the ball is. Not currently implimented
    public float BallSizeLevel = 1;

    // PaddleSpeedLevel is the level of speed the paddle has. The higher the level, the faster the paddle moves. Not currently implimented
    public float PaddleSpeedLevel = 1;

    // PaddleSizeLevel is the level of size the paddle has. The higher the level, the larger the paddle is.
    public float PaddleSizeLevel = 1;

    // Effect variables //

    // SlowTimeFactor is the factor at which time is "slowed down". The higher the factor, the slower things will move.
    public float SlowTimeFactor = 1;

    public bool SlowTimeActive = false;
}
