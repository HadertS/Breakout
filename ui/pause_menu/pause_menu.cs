using Godot;
using System;

public partial class pause_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetTree().Paused)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}
	
}
