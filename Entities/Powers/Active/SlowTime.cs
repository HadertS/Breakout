using Godot;

public partial class SlowTime : ActivatablePower
{

	[Signal]
	public delegate void SlowTimeActivatedEventHandler();
	[Signal]
	public delegate void SlowTimeDeactivatedEventHandler();


	public override void _Ready()
	{
		base._Ready();
		DescriptionName = "Slow Time";
		DescriptionText = "Slows down time while button is held.";
		EnergyCost = 50;
		Threshold = 50;
		KeySlot = "ui_select";

		Connect("SlowTimeActivated", new Godot.Callable(GetNode<Ball>("/root/Level/Ball"), "OnSlowTimeActivated")); 
		Connect("SlowTimeDeactivated", new Godot.Callable(GetNode<Ball>("/root/Level/Ball"), "OnSlowTimeDeactivated")); 

	}

    public override void ActivatePower()
    {
        base.ActivatePower();
		GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor = 0.5f;
		GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeActive = true;
		EmitSignal(SignalName.SlowTimeActivated);	
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
		EmitSignal(SignalName.SlowTimeDeactivated);
		GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor = 1;
		GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeActive = false;
    }
}
