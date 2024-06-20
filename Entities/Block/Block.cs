using System;
using Godot;

public partial class Block : Node2D
{
    [Export]
    private int Hitpoints { get; set; } = 1;

    [Export(
        PropertyHint.Enum,
        "None,PaddleSizeIncrease,PaddleSizeDecrease,StickyPaddle,SlowTime,BallPiercing,PaddleSpeedIncrease,PaddleSpeedDecrease"
    )]
    public string PowerUpType { get; set; } = "None";
    AnimatedSprite2D animatedSprite2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("default");
        animatedSprite2D.SetFrameAndProgress(Hitpoints - 1, 1);
        animatedSprite2D.Pause();
    }

    private void OnHit()
    {
        Hitpoints--;
        if (Hitpoints <= 0)
        {
            if (PowerUpType != "None")
            {
                if (PowerUpType == "PaddleSizeIncrease")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpPaddleSize/PowerUpPaddleSize.tscn"
                    );
                    PowerUpPaddleSize powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpPaddleSize;
                    powerUpInstance.IsIncrease = true;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "PaddleSizeDecrease")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpPaddleSize/PowerUpPaddleSize.tscn"
                    );
                    PowerUpPaddleSize powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpPaddleSize;
                    powerUpInstance.IsIncrease = false;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "StickyPaddle")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpStickyPaddle/PowerUpStickyPaddle.tscn"
                    );
                    PowerUpStickyPaddle powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpStickyPaddle;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "SlowTime")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpSlowTime/PowerUpSlowTime.tscn"
                    );
                    PowerUpSlowTime powerUpInstance = powerUpScene.Instantiate() as PowerUpSlowTime;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "BallPiercing")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpBallPiercing/PowerUpBallPiercing.tscn"
                    );
                    PowerUpBallPiercing powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpBallPiercing;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "PaddleSpeedIncrease")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpPaddleSpeed/PowerUpPaddleSpeed.tscn"
                    );
                    PowerUpPaddleSpeed powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpPaddleSpeed;
                    powerUpInstance.IsIncrease = true;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
                else if (PowerUpType == "PaddleSpeedDecrease")
                {
                    PackedScene powerUpScene = GD.Load<PackedScene>(
                        "res://Entities/PowerUp/PowerUpPaddleSpeed/PowerUpPaddleSpeed.tscn"
                    );
                    PowerUpPaddleSpeed powerUpInstance =
                        powerUpScene.Instantiate() as PowerUpPaddleSpeed;
                    powerUpInstance.IsIncrease = false;
                    GetParent().AddChild(powerUpInstance);
                    powerUpInstance.Position = Position;
                }
            }

            QueueFree();
        }
        else
        {
            animatedSprite2D.SetFrameAndProgress(Hitpoints - 1, 1);
        }

        animatedSprite2D.Pause();
    }
}
