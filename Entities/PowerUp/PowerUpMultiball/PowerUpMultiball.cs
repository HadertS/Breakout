using Godot;

public partial class PowerUpMultiball : PowerUp
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void Collected()
    {
        PackedScene BallScene = GD.Load<PackedScene>("res://Entities/Ball/Ball.tscn");
        Ball BallInstance = BallScene.Instantiate() as Ball;
        GetParent().AddChild(BallInstance);
        BallInstance.Position =
            GetNode<Paddle>("/root/Level/Paddle").Position + new Vector2(0, -100);

        base.Collected();
    }
}
