using Godot;

public partial class Paddle : CharacterBody2D
{
    [Export]
	private int Speed { get; set; } = 800;
    [Export]
    private float Piercing { get; set; } = 0;
    
    
    private AnimatedSprite2D animatedSprite;
    public override void _Ready()
    {   
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play("default");
        //this will need to be changed eventually once the powerups are properly implemented
        Piercing = GetNode<OptionButton>("/root/GameManager/GUI/PauseMenu/Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").GetSelectedId();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDir = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 0);
        Velocity = inputDir * Speed;
        MoveAndCollide(Velocity * (float)delta);
    }
}
