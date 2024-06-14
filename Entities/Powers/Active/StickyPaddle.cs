using Godot;

public partial class StickyPaddle : ActivatablePower
{
	public override void _Ready()
	{
		base._Ready();
		DescriptionName = "Sticky Paddle";
		DescriptionText = "The ball will stick to the paddle while button is held. Release to launch upwards.";
		EnergyCost = 10;
		Threshold = 50;
        KeySlot = "ui_select";
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
            GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate = Paddle.PaddleState.Default;
            GetNode<Ball>("/root/Level/Ball").CallDeferred("Launch");
        }
    }

}
