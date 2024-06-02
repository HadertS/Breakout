using Godot;

public partial class Level : Node2D
{
	[Export]
	private string NextLevelPath { get; set; }
	
	public override void _Process(double delta)
	{
			if (!GetTree().HasGroup("Blocks"))
			{
				if(NextLevelPath != null){
					GetNode<GameManager>("/root/GameManager").CallDeferred("LoadLevel", NextLevelPath);
				}
				else{
					GetTree().Quit();
				}
			}
	}
}
