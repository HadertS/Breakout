using Godot;

public partial class PowerUpBallPiercing : PowerUp
{
    public override void Collected()
    {
        GetNode<GlobalVariables>("/root/GlobalVariables").BallPiercingLevel++;
        base.Collected();
    }
}
