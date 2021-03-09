using System;

namespace xUnit
{
  public class GuidGenerator
  {
    public Guid RandomGuid { get; } = Guid.NewGuid();
  }
}