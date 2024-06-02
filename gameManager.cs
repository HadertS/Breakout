using Godot;

public partial class GameManager : Node
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
		
		//start game paused
		GetTree().Paused = true;
	}

	public void LoadLevel(string path)
	{	
		if (levelInstance != null){
			levelInstance.Name = "OldLevel";
			levelInstance?.QueueFree();
		}

		GD.Print("Loading level: " + path);
		levelScene = GD.Load<PackedScene>(path);
		levelInstance = levelScene.Instantiate();
		AddChild(levelInstance);
		levelInstance.Name = "Level";
		
		GetTree().Paused = true;
	}

}
