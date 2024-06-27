using Godot;

public partial class PowerUpBallSpeed : PowerUp
{
    [Export]
    public bool IsIncrease { get; set; } = true;

    public override void _Ready()
    {
        base._Ready();

        if (IsIncrease)
        {
            Sprite.Modulate = Colors.Purple;
        }
        else
        {
            Sprite.Modulate = Colors.Blue;
        }
    }

    public override void Collected()
    {
        if (IsIncrease)
        {
            IncreaseBallSpeed();
        }
        else
        {
            DecreaseBallSpeed();
        }
        base.Collected();
    }

    public void IncreaseBallSpeed()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel += 0.5f;
        GetTree().CallGroup("Balls", "OnBallSpeedIncrease");
    }

    public void DecreaseBallSpeed()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel -= 0.5f;
        GetTree().CallGroup("Balls", "OnBallSpeedDecrease");
    }
}
