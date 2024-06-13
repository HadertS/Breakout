using Godot;

public partial class GlobalVariables : Node
{	
	// Player variables //
	public double EnergyBar = 100;
	// EnergyBar is the player's energy bar. It is used to power the player's abilities.
	public double EnergyBarRegenRate = 0.5;
	// EnergyBarRegenRate is the rate at which the energy bar regenerates. Not currently implimented
	public double EnergyBarEmptyThrehold = 50;
	//EnergyBarRegenRate is the threhold at which powers cannot be used if the energy bar is drained to empty
	public int BallPiercingLevel = 0;
	// BallPiercingLevel is the level of piercing the ball has. The higher the level, the more blocks the ball can pierce through.
	public int BallSpeedLevel = 1;
	// BallSpeedLevel is the level of speed the ball has. The higher the level, the faster the ball moves. Not currently implimented.
	public int BallSizeLevel = 1;
	// BallSizeLevel is the level of size the ball has. The higher the level, the larger the ball is. Not currently implimented
	public float PaddleSpeedLevel = 1;
	// PaddleSpeedLevel is the level of speed the paddle has. The higher the level, the faster the paddle moves. Not currently implimented
	public float PaddleSizeLevel = 1;
	// PaddleSizeLevel is the level of size the paddle has. The higher the level, the larger the paddle is.


	// Effect variables //	
	public float SlowTimeFactor = 1;
	public bool SlowTimeActive = false;
	// SlowTimeFactor is the factor at which time is "slowed down". The higher the factor, the slower things will move.
}
