using System;
using Godot;

public partial class Block : Node2D
{
	[Export]
	private int Hitpoints { get; set; } = 1;
	[Export(PropertyHint.Enum, "None, PaddleSize, BallSize")]
	public String PowerUpType { get; set; } = "None";
	AnimatedSprite2D animatedSprite2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("default");
		animatedSprite2D.SetFrameAndProgress(Hitpoints - 1,1);
		animatedSprite2D.Pause();
	}

	private void OnHit(){
		Hitpoints--;
		if (Hitpoints <= 0){
			if (PowerUpType != "None"){
				PackedScene powerUpScene = GD.Load<PackedScene>("res://Entities/PowerUp/PowerUp.tscn");
				PowerUp powerUpInstance = powerUpScene.Instantiate() as PowerUp;
				GetParent().AddChild(powerUpInstance);
				powerUpInstance.Position = Position;
				powerUpInstance.PowerUpType = PowerUpType;
			}
			
			QueueFree();
		}
		else{
			animatedSprite2D.SetFrameAndProgress(Hitpoints - 1,1);
		}

		animatedSprite2D.Pause();

	}
}
