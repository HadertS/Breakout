using Godot;

public partial class PowerUpPaddleSpeed : PowerUp
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
            IncreasePaddleSpeed();
        }
        else
        {
            DecreasePaddleSpeed();
        }
        base.Collected();
    }

    public void IncreasePaddleSpeed()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSpeedLevel += 0.1f;
    }

    public void DecreasePaddleSpeed()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSpeedLevel -= 0.1f;
    }
}
