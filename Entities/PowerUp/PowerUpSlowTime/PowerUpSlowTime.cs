using Godot;

public partial class PowerUpSlowTime : PowerUp
{
    public override void Collected()
    {
        PackedScene PowerScene = GD.Load<PackedScene>("res://Entities/Powers/Active/SlowTime.tscn");
        Node PowerInstance = PowerScene.Instantiate();
        GetTree().Root.AddChild(PowerInstance);
    }

}
