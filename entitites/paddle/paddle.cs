using Godot;
using System;

public partial class paddle : CharacterBody2D
{
	private int speed = 800;
    private AnimatedSprite2D animatedSprite;
    public override void _Ready()
    {   
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play("default");    
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDir * speed;
        MoveAndCollide(Velocity * (float)delta);
    }
}
