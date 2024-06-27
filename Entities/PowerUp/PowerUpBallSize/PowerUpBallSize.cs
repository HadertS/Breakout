using Godot;

public partial class PowerUpBallSize : PowerUp
{
    [Export]
    public bool IsIncrease { get; set; } = true;

    public override void _Ready()
    {
        base._Ready();

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
        GetTree().CallGroup("Balls", "OnBallSizeIncrease");
    }

    public void DecreaseBallSize()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel -= 0.1f;
        GetTree().CallGroup("Balls", "OnBallSizeDecrease");
    }
}
