using Godot;

public partial class ActivatablePower : Power
{
	public float EnergyCost { get; set; }
	public float Threshold { get; set; }
	//needs to be refactored out of EnergyBar.cs
	public bool IsActive { get; set; }
	public string KeySlot { get; set; }

	public override void _Ready()
	{
		GetNode<EnergyBar>("/root/GUI/EnergyBar").Connect("EnergyBarEmpty", new Godot.Callable(this, "OnEnergyBarEmpty")); 
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed(KeySlot) && !IsActive){
            if (GetNode<EnergyBar>("/root/GUI/EnergyBar").CurrentState != EnergyBar.State.EMPTY){
                ActivatePower();
            }    
        }
        else if (Input.IsActionJustReleased(KeySlot) && IsActive){
            DeactivatePower();
        }

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
