using Godot;
using System;

public partial class block : Node
{
	[Export]
	private int Hitpoints { get; set; } = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		switch (Hitpoints){
			case 1:
				animatedSprite2D.Play("red");
				animatedSprite2D.SetFrameAndProgress(1,1);
			break;
			case 2:
				animatedSprite2D.Play("red");
				animatedSprite2D.SetFrameAndProgress(0,1);
			break;
			case 3:
				animatedSprite2D.Play("blue_dark");
				animatedSprite2D.SetFrameAndProgress(1,1);
			break;
			case 4:
				animatedSprite2D.Play("blue_dark");
				animatedSprite2D.SetFrameAndProgress(0,1);

			break;
			default:
				animatedSprite2D.Play("grey");
			break;
		}
		animatedSprite2D.Pause();
	}

	private void OnHit(){
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Hitpoints--;

		switch(Hitpoints){
			case 0:
				QueueFree();
			break;
			case 1:
				animatedSprite2D.SetFrameAndProgress(1,1);
			break;
			case 2:
				animatedSprite2D.Play("red");
				animatedSprite2D.SetFrameAndProgress(0,1);
			break;
			case 3:
				animatedSprite2D.SetFrameAndProgress(1,1);
			break;
		}
		animatedSprite2D.Pause();

	}
}
