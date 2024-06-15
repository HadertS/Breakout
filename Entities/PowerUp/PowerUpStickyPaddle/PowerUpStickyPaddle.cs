using Godot;

public partial class PowerUpStickyPaddle : PowerUp
{
    public override void Collected()
    {
        PackedScene PowerScene = GD.Load<PackedScene>(
            "res://Entities/Powers/Active/StickyPaddle.tscn"
        );
        Node PowerInstance = PowerScene.Instantiate();
        GetTree().Root.AddChild(PowerInstance);
    }
}
