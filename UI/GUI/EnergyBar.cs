using Godot;

public partial class EnergyBar : ProgressBar
{
	public enum State
	{
		FULL,
		DRAINING,
		EMPTY,
		IDLE
	}
	public State CurrentState = State.FULL;

	[Signal]
	public delegate void EnergyBarEmptyEventHandler();
	
	public override void _Ready()
	{
		// Set the energy bar's value to the player's EnergyBar value
		Value = GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Set the energy bar's value to the player's EnergyBar value
		Value = GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar;

		if (CurrentState == State.EMPTY || CurrentState == State.IDLE)
		{
			// If the energy bar is empty or idle, refill it by 1 every tick
			Refill(5*delta);
		}
	}

	public void Drain(double value)
	{
		//Drain the energy bar by the value passed in, capped at 0
		if (CurrentState != State.EMPTY){
			CurrentState = State.DRAINING;
			GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar = GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar - value;
		}
		if (GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar <= 0)
		{
			CurrentState = State.EMPTY;
			GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar = 0;
			EmitSignal(nameof(EnergyBarEmpty));	
		}
	}

	public void StopDrain()
	{
		//Stop draining the energy bar
		if (CurrentState == State.DRAINING){
			CurrentState = State.IDLE;
		}
	}

	public void Refill(double value)
	{
		//If not full refill the energy bar by the value passed in, capped at 100
		if (CurrentState != State.FULL){
			CurrentState = State.IDLE;
			GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar = GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar + value;
		}
		if (GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar >= 100)
		{
			CurrentState = State.FULL;
			GetNode<PlayerVariables>("/root/PlayerVariables").EnergyBar = 100;
		}
	}


}
