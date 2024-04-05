using Godot;
using System;

public partial class paddle : CharacterBody2D
{
	private int speed = 800;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = inputDir * speed;
        MoveAndCollide(Velocity * (float)delta);
    }
}
