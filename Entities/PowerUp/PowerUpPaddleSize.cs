using Godot;

public partial class PowerUpPaddleSize : PowerUp
{

    [Signal]
	public delegate void PaddleSizeIncreaseEventHandler();
    [Signal]
	public delegate void PaddleSizeDecreaseEventHandler();
    [Export]
    public bool IsPaddleSizeIncrease {get; set;} = true;

	public override void _Ready(){
		base._Ready();
		Connect("PaddleSizeIncrease", new Godot.Callable(GetNode<Paddle>("/root/Level/Paddle"), "OnPaddleSizeIncrease")); 
		Connect("PaddleSizeDecrease", new Godot.Callable(GetNode<Paddle>("/root/Level/Paddle"), "OnPaddleSizeDecrease")); 
	}
	
	public override void Collected(){
		if (IsPaddleSizeIncrease)
		{
			IncreasePaddleSize();
		}
		else
		{
			DecreasePaddleSize();
		}
	}

	public void IncreasePaddleSize()
    {
		GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel+=0.1f;
        EmitSignal(SignalName.PaddleSizeIncrease);
    }

    public void DecreasePaddleSize()
    {
		GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel-=0.1f;
        EmitSignal(SignalName.PaddleSizeDecrease);
    }

}
