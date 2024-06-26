using System;
using Godot;

public partial class PowerUp : CharacterBody2D
{
    [Export]
    public int Speed = 300;
    public virtual Sprite2D Sprite { get; set; }

    public override void _Ready()
    {
        Sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity =
            new(0, Speed * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor);

        KinematicCollision2D collisionInfo = MoveAndCollide(velocity * (float)delta);

        if (collisionInfo != null)
        {
            if (collisionInfo.GetCollider().GetType() == typeof(Paddle))
            {
                Collected();
            }
        }
        if (Position.Y > GetViewportRect().Size.Y)
        {
            QueueFree();
        }
    }

    public virtual void Collected()
    {
        QueueFree();
    }
}
