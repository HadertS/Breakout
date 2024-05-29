using Godot;
using System;

public partial class gameManager : Node
{
	[Export]
	private string LevelAddress { get; set; }
	private PackedScene levelScene;
	private Node levelInstance;
	public override void _Ready()
	{
		if (LevelAddress != null)
		{
			levelScene = GD.Load<PackedScene>(LevelAddress);
			levelInstance = levelScene.Instantiate();
			AddChild(levelInstance);
		}
		

		//Debug menu event connections
		var debugMenu = GetNode<Control>("PauseMenu/Panel/VBoxContainer/DebugMenu");

		debugMenu.GetNode<Button>("DebugLevelDialogButton").Pressed += OnDebugLevelLoadPressed;
		debugMenu.GetNode<FileDialog>("DebugLevelFileDialog").FileSelected += OnDebugLevelFileDialogFileSelected;
		debugMenu.GetNode<OptionButton>("HBoxContainer/DebugPiercing").ItemSelected += OnDebugPiercingSelected;
		
		//start game paused
		GetTree().Paused = true;

	}
    private void OnDebugPiercingSelected(long index)
    {
		levelInstance.GetNode<CharacterBody2D>("Paddle").SetIndexed("Piercing", GetNode<OptionButton>("PauseMenu/Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").GetSelectedId());
    }
	private void OnDebugLevelLoadPressed()
    {
		var levelFileDialog = GetNode<FileDialog>("PauseMenu/Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog");
		levelFileDialog.Show();
    }
	public void OnDebugLevelFileDialogFileSelected(string path)
	{
		GD.Print("Loading level: " + path);
		LevelAddress = path;
		levelScene = GD.Load<PackedScene>(LevelAddress);
		levelInstance.QueueFree();
		levelInstance = levelScene.Instantiate();
		AddChild(levelInstance);
		GetTree().Paused = true;
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
