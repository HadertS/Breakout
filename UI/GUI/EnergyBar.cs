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
        Modulate = new Color(0, 1, 0);
        // Set the energy bar's value to the player's EnergyBar value
        Value = GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar;
        if (
            GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar
            < GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBarEmptyThrehold
        )
        {
            CurrentState = State.EMPTY;
            Modulate = new Color(1, 0, 0);
        }
        if (GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar == 100)
        {
            CurrentState = State.FULL;
        }
        else
        {
            CurrentState = State.IDLE;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Set the energy bar's value to the player's EnergyBar value
        Value = GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar;

        if (CurrentState == State.EMPTY || CurrentState == State.IDLE)
        {
            // If the energy bar is empty or idle, refill it by 1 every tick
            Refill(5 * delta);
        }
    }

    public void Drain(double value)
    {
        //Drain the energy bar by the value passed in, capped at 0
        if (CurrentState != State.EMPTY)
        {
            CurrentState = State.DRAINING;
            GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar =
                GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar - value;
        }
        if (GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar <= 0)
        {
            CurrentState = State.EMPTY;
            GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar = 0;
            EmitSignal(SignalName.EnergyBarEmpty);
            Modulate = new Color(1, 0, 0);
        }
    }

    public void StopDrain()
    {
        //Stop draining the energy bar
        if (CurrentState == State.DRAINING)
        {
            CurrentState = State.IDLE;
        }
    }

    public void Refill(double value)
    {
        //If not full refill the energy bar by the value passed in, capped at 100
        //If the energy bar was empty, change state to idle if above EnergyBarEmptyThrehold, reactivating it
        GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar =
            GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar + value;

        if (CurrentState == State.EMPTY)
        {
            if (
                GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar
                >= GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBarEmptyThrehold
            )
            {
                CurrentState = State.IDLE;
                Modulate = new Color(0, 1, 0);
            }
        }
        if (GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar >= 100)
        {
            CurrentState = State.FULL;
            GetNode<GlobalVariables>("/root/GlobalVariables").EnergyBar = 100;
        }
    }
}
