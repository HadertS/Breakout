using Godot;
using System;

public partial class ball : CharacterBody2D
{
	private int speed = 500;
	private Vector2 velocity;
	private Vector2 startPosition;
	private Vector2 previousPosition;


    public override void _Ready()
    {
		velocity = new Vector2(0,1*speed);
		startPosition = Position;
    }
    public override void _PhysicsProcess(double delta)
    {
		var collisionInfo = MoveAndCollide(velocity * (float)delta);
        if (collisionInfo != null){
			if ((collisionInfo.GetCollider().GetType())==typeof(paddle)){
				//forms a vector pointing from the paddle to the ball. The further out from the center of the paddle, the wider the ball will go.
				Vector2 relativeVector = (GlobalPosition - (Vector2)collisionInfo.GetCollider().GetIndexed("global_position")).Normalized()*speed;
				//mix of standard bounce collision and the relative vector. Adjust the mix to change bounce behaviour.
				velocity = ((velocity.Bounce(collisionInfo.GetNormal()))/2+(relativeVector)/2).Normalized()*speed;
			}
			else {
				velocity = velocity.Bounce(collisionInfo.GetNormal());
			}
			if ((collisionInfo.GetCollider().GetType())==typeof(block)){
				collisionInfo.GetCollider().CallDeferred("OnHit");
			}
		}
		
		if (Position == previousPosition)
		{
			//Ball is stuck
			ResetBall();
		}
		if (Position.Y > GetViewportRect().Size.Y){
			//Ball is out of bounds
			ResetBall();
		}
		previousPosition = Position;
	}

	private void ResetBall()
	{
		Position = startPosition;
		velocity = new Vector2(0,1*speed);
	}
}
