using Godot;

public partial class PowerUp : CharacterBody2D
{
	[Export]
	public int Speed = 300;
	[Export(PropertyHint.Enum, "PaddleSize, BallSize")]
	public string PowerUpType { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = new(0,Speed);

		var collisionInfo = MoveAndCollide(velocity * (float)delta);

		if (collisionInfo != null)
		{
			if (collisionInfo.GetCollider().GetType()==typeof(Paddle))
			{
				//add powerup effect to paddle here

				QueueFree();
			}
		}
		if (Position.Y > GetViewportRect().Size.Y){
			QueueFree();
		}
	}
}
