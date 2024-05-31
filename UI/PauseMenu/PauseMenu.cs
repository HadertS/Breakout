using Godot;

public partial class PauseMenu : Control
{

    public override void _Ready(){
		CheckPause();
	}


    public override void _Process(double delta)
    {
		CheckPause();
	}

	private void CheckPause()
	{
		if (GetTree().Paused)
		{
			Show();
		}
		else
		{
			Hide();
		}    
	}
}

