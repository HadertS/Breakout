using Godot;
using System;

public partial class gameManager : Node
{
	public override void _Ready()
	{
		GetTree().Paused = false;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			GetNode<Control>("PauseMenu").Visible = !GetNode<Control>("PauseMenu").Visible;
		}
	}
}
