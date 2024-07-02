using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Godot;

public partial class Ball : CharacterBody2D
{
    private int speed = 400;
    private int BallPiercing = 0;
    private Vector2 velocity;
    private Vector2 previousPosition;
    public bool isStuckToPaddle = false;
    private bool collisionTimout = false;

    public override void _Ready()
    {
        velocity = new Vector2(
            0,
            -1 //* speed * GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
        );
        Position = GetNode<Paddle>("/root/Level/Paddle").Position + new Vector2(0, -100);
        this.Scale = new Vector2(
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel,
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel
        );
    }

    public override void _PhysicsProcess(double delta)
    {
        if (isStuckToPaddle)
        {
            //moves the ball with the paddle
            velocity = GetNode<Paddle>("/root/Level/Paddle").Velocity;
            MoveAndCollide(velocity * (float)delta);
        }
        else
        {
            KinematicCollision2D collisionInfo = MoveAndCollide(velocity * (float)delta);
            if (collisionInfo != null)
            {
                if (collisionInfo.GetCollider().GetType() == typeof(Paddle))
                {
                    OnHitPaddle((Paddle)collisionInfo.GetCollider(), collisionInfo.GetNormal());
                }
                else if (collisionInfo.GetCollider().GetType() == typeof(Block))
                {
                    OnHitBlock((Block)collisionInfo.GetCollider(), collisionInfo.GetNormal());
                }
                else
                {
                    velocity = velocity.Bounce(collisionInfo.GetNormal());
                    collisionTimout = false;
                }
            }
        }

        if ((Position == previousPosition && !isStuckToPaddle))
        {
            //Ball is stuck in place
            ResetBall();
        }
        else if (Position.Y > GetViewportRect().Size.Y)
        {
            //Ball is out of bounds, if more than 1 ball is in play, destroy this ball. Otherwise, reset the ball.
            if (GetTree().GetNodesInGroup("Balls").Count > 1)
            {
                QueueFree();
            }
            else
            {
                ResetBall();
            }
        }

        previousPosition = Position;
    }

    private void ResetBall()
    {
        Position = GetNode<Paddle>("/root/Level/Paddle").Position + new Vector2(0, -100);
        velocity =
            new Vector2(0, -1)
            * speed
            //* GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
            * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
        collisionTimout = false;
    }

    public void Launch()
    {
        if (isStuckToPaddle)
        {
            velocity =
                new Vector2(0, -1)
                * speed
                //* GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
                * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
            isStuckToPaddle = false;
        }
    }

    public void OnSlowTimeActivated()
    {
        velocity *= GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
    }

    public void OnSlowTimeDeactivated()
    {
        velocity /= GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
    }

    public void OnBallSizeIncrease()
    {
        this.Scale = new Vector2(
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel,
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel
        );
    }

    public void OnBallSizeDecrease()
    {
        this.Scale = new Vector2(
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel,
            0.1f * GetNode<GlobalVariables>("/root/GlobalVariables").BallSizeLevel
        );
    }

    public void OnHitPaddle(Paddle Paddle, Vector2 CollisionNormal)
    {
        BallPiercing = GetNode<GlobalVariables>("/root/GlobalVariables").BallPiercingLevel;

        if (!collisionTimout)
        {
            if (
                GetNode<Paddle>("/root/Level/Paddle").CurrentPaddlestate
                == Paddle.PaddleState.Sticky
            )
            {
                velocity = new Vector2(0, 0);
                isStuckToPaddle = true;
                collisionTimout = true;
            }
            else
            {
                if (CollisionNormal.Y == -1)
                {
                    //forms a vector pointing from the paddle to the ball. The further out from the center of the paddle, the wider the ball will go.
                    Vector2 relativeVector =
                        (GlobalPosition - Paddle.GlobalPosition).Normalized()
                        * speed
                        //* GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
                        * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
                    //mix of standard bounce collision and the relative vector. Adjust the mix to change bounce behaviour.
                    velocity =
                        (velocity.Bounce(CollisionNormal) / 2 + relativeVector / 2).Normalized()
                        * speed
                        //* GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
                        * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
                    collisionTimout = true;
                }
                else
                {
                    velocity =
                        velocity.Bounce(CollisionNormal).Normalized() * speed
                        + Paddle.Velocity
                            //* GetNode<GlobalVariables>("/root/GlobalVariables").BallSpeedLevel
                            * GetNode<GlobalVariables>("/root/GlobalVariables").SlowTimeFactor;
                    collisionTimout = true;
                }
            }
        }
    }

    public void OnHitBlock(Block Block, Vector2 CollisionNormal)
    {
        Block.Call("OnHit");
        if (BallPiercing >= 1)
        {
            BallPiercing -= 1;
        }
        else
        {
            velocity = velocity.Bounce(CollisionNormal);
        }

        collisionTimout = false;
    }

    public void OnBallSpeedIncrease()
    {
        //velocity *= 0.5f;
    }

    public void OnBallSpeedDecrease()
    {
        //velocity /= 0.5f;
    }
}
