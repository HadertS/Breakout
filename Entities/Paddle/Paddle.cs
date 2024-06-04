using Godot;

public partial class Paddle : CharacterBody2D
{
    [Export]
	private int Speed { get; set; } = 800;
    
    private AnimatedSprite2D AnimatedSprite;
    private CollisionShape2D CollisionShape2D;

    public override void _Ready()
    {   
        AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        AnimatedSprite.Play("default");
        this.Scale = new Vector2(0.25f*GetNode<PlayerVariables>("/root/PlayerVariables").PaddleSizeLevel, 0.25f);
    }

    public override void _PhysicsProcess(double delta)
    {
        this.Scale = new Vector2(0.25f*GetNode<PlayerVariables>("/root/PlayerVariables").PaddleSizeLevel, 0.25f);
        Vector2 inputDir = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 0);
        Velocity = inputDir * Speed;
        MoveAndCollide(Velocity * (float)delta);
    }
}
