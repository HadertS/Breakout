using Godot;

public partial class GameManager : Node2D
{
	[Export]
	private string StartLevelPath { get; set; }
	private PackedScene levelScene;
	private Node levelInstance;
	public override void _Ready()
	{
		if (StartLevelPath != null)
		{
			LoadLevel(StartLevelPath);
		}
		

		//Debug menu event connections
		var debugMenu = GetNode<Control>("PauseMenu/Panel/VBoxContainer/DebugMenu");

		debugMenu.GetNode<Button>("DebugLevelDialogButton").Pressed += OnDebugLevelLoadPressed;
		debugMenu.GetNode<FileDialog>("DebugLevelFileDialog").FileSelected += LoadLevel;
		debugMenu.GetNode<OptionButton>("HBoxContainer/DebugPiercing").ItemSelected += OnDebugPiercingSelected;
		debugMenu.GetNode<OptionButton>("HBoxContainer2/DebugTime").ItemSelected += OnDebugDebugTimeSelected;
		
		//start game paused
		GetTree().Paused = true;

	}
    private void OnDebugPiercingSelected(long index)
    {
		levelInstance.GetNode<CharacterBody2D>("Paddle").SetIndexed("Piercing", GetNode<OptionButton>("PauseMenu/Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").GetSelectedId());
    }
	private void OnDebugDebugTimeSelected(long index){
		Engine.TimeScale = index*0.25;
	}

	private void OnDebugLevelLoadPressed()
    {
		var levelFileDialog = GetNode<FileDialog>("PauseMenu/Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog");
		levelFileDialog.Show();
    }
	public void LoadLevel(string path)
	{
		GD.Print("Loading level: " + path);
		levelScene = GD.Load<PackedScene>(path);
		levelInstance?.QueueFree();
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
