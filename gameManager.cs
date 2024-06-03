using Godot;

public partial class GameManager : Node
{
	private Node levelInstance;
	private Node GUIInstance;
	public override void _Ready()
	{
		CallDeferred(MethodName.LoadGUI);
	}

	private void LoadGUI(){
		GD.Print("Loading GUI");
		PackedScene GUIScene = GD.Load<PackedScene>("res://UI/GUI/GUI.tscn");
		GUIInstance = GUIScene.Instantiate();
		GetTree().Root.AddChild(GUIInstance);
		GUIInstance.Name = "GUI";
	}

	public void LoadLevel(string path)
	{	
		CallDeferred(MethodName.LoadLevelDeferred, path);
	}

	private void LoadLevelDeferred(string path)
	{	
		levelInstance?.Free();

		GD.Print("Loading level: " + path);
		PackedScene levelScene = GD.Load<PackedScene>(path);
		levelInstance = levelScene.Instantiate();
		GetTree().Root.AddChild(levelInstance);
		levelInstance.Name = "Level";
		GetTree().Paused = true;
	}

}
