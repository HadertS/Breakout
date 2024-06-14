using Godot;

public partial class PowerUp : CharacterBody2D
{
	[Export]
	public int Speed = 300;

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = new(0,Speed*GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor);

		KinematicCollision2D collisionInfo = MoveAndCollide(velocity * (float)delta);

		if (collisionInfo != null)
		{
			if (collisionInfo.GetCollider().GetType()==typeof(Paddle))
			{
				Collected();
				QueueFree();
			}
		}
		if (Position.Y > GetViewportRect().Size.Y){
			QueueFree();
		}
	}

	public virtual void Collected()
	{
	}

}
