using Godot;

public partial class Power : Node
{
	public string DescriptionName { get; set; }
	public string DescriptionText { get; set; }
	public float EnergyCost { get; set; }
	public float Threshold { get; set; }
	//needs to be refactored out of EnergyBar.cs
	public bool IsActive { get; set; }
	public override void _Ready()
	{
		GetNode<EnergyBar>("/root/GUI/EnergyBar").Connect("EnergyBarEmpty", new Godot.Callable(this, "OnEnergyBarEmpty")); 
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsActive){
			GetNode<EnergyBar>("/root/GUI/EnergyBar").Drain(EnergyCost*delta);
		}      
	}
	public virtual void ActivatePower()
	{
		IsActive = true;
	}

	public virtual void DeactivatePower()
	{
		GetNode<EnergyBar>("/root/GUI/EnergyBar").StopDrain();
		IsActive = false;
	}

	public virtual void OnEnergyBarEmpty()
	{
		DeactivatePower();
	}
}
