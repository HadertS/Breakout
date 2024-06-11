using Godot;

public partial class SlowTime : Power
{
	public override void _Ready()
	{
		base._Ready();
		DescriptionName = "Slow Time";
		DescriptionText = "Slows down time while button is held.";
		EnergyCost = 50;
		Threshold = 50;
	}


}
