using System;

namespace EfCoreTemporalTable.Test.SampleModel
{
  public class ExampleEntity1
  {
    public int ExampleEntityId { get; private set; }
    public string FirstProperty { get; set; }
    public string SecondProperty { get; set; }
    public DateTime ThirdProperty { get; set; }
    public Guid FourthProperty { get; private set; }
  }
}
