using Godot;

public partial class PowerUpPaddleSize : PowerUp
{
    [Signal]
    public delegate void PaddleSizeIncreaseEventHandler();

    [Signal]
    public delegate void PaddleSizeDecreaseEventHandler();

    [Export]
    public bool IsIncrease { get; set; } = true;

    public override void _Ready()
    {
        base._Ready();
        Connect(
            "PaddleSizeIncrease",
            new Godot.Callable(GetNode<Paddle>("/root/Level/Paddle"), "OnPaddleSizeIncrease")
        );
        Connect(
            "PaddleSizeDecrease",
            new Godot.Callable(GetNode<Paddle>("/root/Level/Paddle"), "OnPaddleSizeDecrease")
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
            IncreasePaddleSize();
        }
        else
        {
            DecreasePaddleSize();
        }
    }

    public void IncreasePaddleSize()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSpeedLevel += 0.1f;
        EmitSignal(SignalName.PaddleSizeIncrease);
    }

    public void DecreasePaddleSize()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSpeedLevel -= 0.1f;
        EmitSignal(SignalName.PaddleSizeDecrease);
    }
}
