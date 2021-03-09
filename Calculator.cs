using System;

namespace xUnit
{
  public class Calculator
  {
    // private CalculatorState _state = CalculatorState.Cleared;

    public decimal Value { get; set; } = 0;

    public bool Activated { get; set; } = false;

    public decimal Add(decimal value)
    {
      // _state = CalculatorState.Active;
      return Value += value;
    }

    public decimal Subtract(decimal value)
    {
      // _state = CalculatorState.Active;
      return Value -= value;
    }

    public decimal Multiply(decimal value)
    {
      if (Value == 0 & !Activated) {
        Activated = true;
        return Value = value;
      }
      return Value *= value;
    }

    public decimal Divide(decimal value)
    {
      if (Value == 0 & !Activated) {
        Activated = true;
        return Value = value;
      }
      return Value /= value;
    }
  }
}