using Godot;

public partial class PauseMenu : Control
{
    public override void _Ready(){
		CheckPause();
		
		//Debug menu event connections

		GetNode<Button>("Panel/VBoxContainer/DebugMenu/DebugLevelDialogButton").Pressed += OnDebugLevelLoadPressed;
		GetNode<FileDialog>("Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog").FileSelected += GetNode<GameManager>("/root/GameManager").LoadLevel;
		GetNode<OptionButton>("Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").ItemSelected += OnDebugPiercingSelected;
		GetNode<OptionButton>("Panel/VBoxContainer/DebugMenu/HBoxContainer3/DebugPaddleSize").ItemSelected += OnDebugPaddleSize;

	}

    public override void _Process(double delta){
		if (Input.IsActionJustPressed("ui_cancel")){
			GetTree().Paused = !GetTree().Paused;
		}
		CheckPause();
	}

	private void CheckPause(){
		if (GetTree().Paused){
			Show();
		}
		else{
			Hide();
		}    
	}
	private void OnDebugPiercingSelected(long index){
		GetNode<GlobalVariables>("/root/GlobalVariables").BallPiercingLevel = (int)index;
    }
	private void OnDebugPaddleSize(long index){
		GetNode<GlobalVariables>("/root/GlobalVariables").PaddleSizeLevel = (float)((index*0.05)+1);
    }

	private void OnDebugLevelLoadPressed(){
		GetNode<FileDialog>("Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog").Show();
    }
}
