using Godot;
using System;

public partial class Block : Node
{
	[Export]
	private int Hitpoints { get; set; } = 1;
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
			QueueFree();
		}
		else{
			animatedSprite2D.SetFrameAndProgress(Hitpoints - 1,1);
		}

		animatedSprite2D.Pause();

	}
}
