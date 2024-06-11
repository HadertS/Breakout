using Godot;

public partial class StickyPaddle : Power
{
	public override void _Ready()
	{
		base._Ready();
		DescriptionName = "Sticky Paddle";
		DescriptionText = "The ball will stick to the paddle while button is held. Release to launch upwards.";
		EnergyCost = 10;
		Threshold = 50;
	}

	public override void _PhysicsProcess(double delta)
	{
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("ui_select") && !IsActive){
            if (GetNode<EnergyBar>("/root/GUI/EnergyBar").CurrentState != EnergyBar.State.EMPTY){
                ActivatePower();
            }    
        }
        else if (Input.IsActionJustReleased("ui_select") && IsActive){
            DeactivatePower();
        }

	}

    public override void ActivatePower()
    {
        base.ActivatePower();
        GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate = Paddle.PaddleState.Sticky;
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        if (GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate == Paddle.PaddleState.Sticky){
            GD.Print("Deactivating Sticky Paddle");
            GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate = Paddle.PaddleState.Default;
            GetNode<Ball>("/root/Level/Ball").CallDeferred("Launch");
    
        }
    }

}
