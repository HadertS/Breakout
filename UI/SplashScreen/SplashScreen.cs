using System;
using Godot;

public partial class SplashScreen : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<GameManager>("/root/GameManager").LoadLevel("res://Levels/TestLevel3.tscn");
        QueueFree();
    }
}
