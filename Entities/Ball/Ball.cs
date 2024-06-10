using Godot;

public partial class Ball : CharacterBody2D
{
	private int speed = 500;
	private int BallPiercing = 0;
	private Vector2 velocity;
	private Vector2 startPosition;
	private Vector2 previousPosition;
	private bool isStuckToPaddle = false;

    public override void _Ready()
    {
		velocity = new Vector2(0,1*speed);
		startPosition = Position;
		GetNode<EnergyBar>("/root/GUI/EnergyBar").Connect("EnergyBarEmpty", new Godot.Callable(this, "OnEnergyBarEmpty"));
    }
    public override void _PhysicsProcess(double delta)
    {
		if (isStuckToPaddle){
			//moves the ball with the paddle	
			MoveAndSlide();
		}
		else{
			KinematicCollision2D collisionInfo = MoveAndCollide(velocity * (float)delta);

			if (collisionInfo != null){
			if (collisionInfo.GetCollider().GetType()==typeof(Paddle)){
				BallPiercing = GetNode<PlayerVariables>("/root/PlayerVariables").BallPiercingLevel;

				if (GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate == Paddle.PaddleState.Sticky){
					velocity = new Vector2(0,0);
					isStuckToPaddle = true;
				}
				else {
					//forms a vector pointing from the paddle to the ball. The further out from the center of the paddle, the wider the ball will go.
					Vector2 relativeVector = (GlobalPosition - (Vector2)collisionInfo.GetCollider().GetIndexed("global_position")).Normalized()*speed*GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
					//mix of standard bounce collision and the relative vector. Adjust the mix to change bounce behaviour.
					velocity = (velocity.Bounce(collisionInfo.GetNormal())/2+relativeVector/2).Normalized()*speed*GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
				}
				
			}
			else if (collisionInfo.GetCollider().GetType()==typeof(Block)){
				//Piercing logic
				if (true){
					collisionInfo.GetCollider().Call("OnHit");
					if (BallPiercing >= 1){
						BallPiercing -= 1;
					}
					else{
						velocity = velocity.Bounce(collisionInfo.GetNormal());
					}
				}
			}
			else {
				velocity = velocity.Bounce(collisionInfo.GetNormal());
			}
		}
	}
		
		if (Position == previousPosition && !isStuckToPaddle){
			//Ball is stuck in place
			ResetBall();
		}
		if (Position.Y > GetViewportRect().Size.Y){
			//Ball is out of bounds
			ResetBall();
		}
		
		previousPosition = Position;
		
		if (Input.IsActionPressed("ui_select")){
			if (GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeUnlocked){

				if (GetNode<EnergyBar>("/root/GUI/EnergyBar").CurrentState != EnergyBar.State.EMPTY){
					GetNode<EnergyBar>("/root/GUI/EnergyBar").Drain(50*delta);
					if (!GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive){
						GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor = 0.5f;
						velocity *= GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
						GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive = true;
					}
				}
			}	
		}
		else if (Input.IsActionJustReleased("ui_select") && GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive){

			GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive = false;
			GetNode<EnergyBar>("/root/GUI/EnergyBar").StopDrain();
			velocity /= GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
			GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor = 1;
		}
	}

	private void ResetBall()
	{
		Position = startPosition;
		velocity = new Vector2(0,1)*speed*GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
	}

	public void Launch()
	{
		if (isStuckToPaddle){
			GD.Print("Firing ball");
			velocity = new Vector2(0,-1)*speed*GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
			isStuckToPaddle = false;
		}
	}

	public void OnEnergyBarEmpty()
    {
        if (GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive){
			velocity /= GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor;
			GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeFactor = 1;
			GetNode<PlayerVariables>("/root/PlayerVariables").SlowTimeActive = false;
        }
    }
}
