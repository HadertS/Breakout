using Godot;

public partial class PlayerVariables : Node
{	
	// Player variables
	public double EnergyBar = 100;
	public int BallPiercingLevel = 0;
	public int BallSpeedLevel = 1;
	public int BallSizeLevel = 1;
	public float PaddleSpeedLevel = 1;
	public float PaddleSizeLevel = 1;
	public bool PaddleStickyUnlocked = false;
	public bool SlowTimeUnlocked = true;
}
