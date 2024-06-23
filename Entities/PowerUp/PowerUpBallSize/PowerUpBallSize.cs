using Godot;

public partial class PowerUpBallSize : PowerUp
{
    [Signal]
    public delegate void BallSizeIncreaseEventHandler();

    [Signal]
    public delegate void BallSizeDecreaseEventHandler();

    [Export]
    public bool IsIncrease { get; set; } = true;

    public override void _Ready()
    {
        base._Ready();
        Connect(
            "BallSizeIncrease",
            new Godot.Callable(GetNode<Ball>("/root/Level/Ball"), "OnBallSizeIncrease")
        );
        Connect(
            "BallSizeDecrease",
            new Godot.Callable(GetNode<Ball>("/root/Level/Ball"), "OnBallSizeDecrease")
        );

        if (IsIncrease)
        {
            Sprite.Modulate = Colors.Green;
        }
        else
        {
            Sprite.Modulate = Colors.Red;
        }
    }

    public override void Collected()
    {
        if (IsIncrease)
        {
            IncreaseBallSize();
        }
        else
        {
            DecreaseBallSize();
        }
        base.Collected();
    }

    public void IncreaseBallSize()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel += 0.1f;
        EmitSignal(SignalName.BallSizeDecrease);

    }

    public void DecreaseBallSize()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel -= 0.1f;
        EmitSignal(SignalName.BallSizeDecrease);

    }
}
