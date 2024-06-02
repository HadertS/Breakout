using Godot;

public partial class PauseMenu : Control
{
    public override void _Ready(){
		CheckPause();
		
		//Debug menu event connections

		GetNode<Button>("Panel/VBoxContainer/DebugMenu/DebugLevelDialogButton").Pressed += OnDebugLevelLoadPressed;
		GetNode<FileDialog>("Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog").FileSelected += GetNode<GameManager>("/root/GameManager").LoadLevel;
		GetNode<OptionButton>("Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").ItemSelected += OnDebugPiercingSelected;
		GetNode<OptionButton>("Panel/VBoxContainer/DebugMenu/HBoxContainer2/DebugTime").ItemSelected += OnDebugDebugTimeSelected;
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
		GetNode<CharacterBody2D>("/root/Level/Paddle").SetIndexed("Piercing", GetNode<OptionButton>("Panel/VBoxContainer/DebugMenu/HBoxContainer/DebugPiercing").GetSelectedId());
    }
	private void OnDebugDebugTimeSelected(long index){
		Engine.TimeScale = index*0.25;
	}
	private void OnDebugLevelLoadPressed(){
		GetNode<FileDialog>("Panel/VBoxContainer/DebugMenu/DebugLevelFileDialog").Show();
    }
}
