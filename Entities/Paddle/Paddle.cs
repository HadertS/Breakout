using System;
using Godot;

public partial class Paddle : CharacterBody2D
{
    [Export]
    private int Speed { get; set; } = 800;

    private AnimatedSprite2D AnimatedSprite;
    private CollisionShape2D CollisionShape2D;

    public enum PaddleState
    {
        Default,
        Sticky
    }

    public PaddleState CurrentPaddlestate = PaddleState.Default;

    public override void _Ready()
    {
        AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        AnimatedSprite.Play("default");
        this.Scale = new Vector2(
            0.25f * GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel,
            0.25f
        );
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir =
            new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 0);
        Velocity = inputDir * Speed * GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSpeedLevel;
        KinematicCollision2D collisionInfo = MoveAndCollide(Velocity * (float)delta);

        if (collisionInfo != null)
        {
            if (collisionInfo.GetCollider().GetType() == typeof(Ball))
            {
                Ball ball = (Ball)collisionInfo.GetCollider();
                ball.OnHitPaddle(this,collisionInfo.GetNormal());
            }
            else if (collisionInfo.GetCollider().HasMethod("Collected"))
            {
                PowerUp powerUp = (PowerUp)collisionInfo.GetCollider();
                powerUp.Collected();
            }
        }
    }

    public void OnPaddleSizeIncrease()
    {
        this.Scale = new Vector2(
            0.25f * GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel,
            0.25f
        );
    }

    public void OnPaddleSizeDecrease()
    {
        this.Scale = new Vector2(
            0.25f * GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel,
            0.25f
        );
    }
}
