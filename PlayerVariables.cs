using Godot;

public partial class PlayerVariables : Node
{	
	// Player variables
	public double EnergyBar = 100;
	//threhold at which powers cannot be used if the energy bar is drained to empty
	public double EnergyBarEmptyThrehold = 50;
	public int BallPiercingLevel = 0;
	public int BallSpeedLevel = 1;
	public int BallSizeLevel = 1;
	public float PaddleSpeedLevel = 1;
	public float PaddleSizeLevel = 1;
	public bool PaddleStickyUnlocked = true;
	public bool SlowTimeUnlocked = false;
}
