#

Resume:
* *sut*: System Under Test
* Each test is running in their own instance of the class
* `Theory` and `InlineData()` decorators
* `Skip` property (`[Fact(Skip = "This test is broken")]`)
* `MemberData(nameof())` decorator
* `ClassData(typeof())` decorator
* Collections


### Using `Theory` to don't repeat code

For tets the same thing but with different values, like Add two positive numbers and then two negative numbers:

``` C#
  [Theory]
  [InlineData(13, 5, 8)]
  [InlineData(0, -5, 5)]
  [InlineData(0, 0, 0)]
  public void AddTwoNumbersShouldEqualTheirEqualTheory(
    decimal expected, decimal firstToAdd, decimal secondToAdd
  )
  {
    _sut.Add(firstToAdd);
    _sut.Add(secondToAdd);
    Assert.Equal(expected, _sut.Value);
  }
```

### Get the input more complex

``` C#
  public static IEnumerable<object[]> TestData()
  {
    yield return new object[](15, new decimal[]{ 10, 5 });
    yield return new object[](15, new decimal[]{ 5, 5, 5 });
    yield return new object[](-10, new decimal[]{ -30, 20 });
  }
```

And then, in the tets method, use the `MemberData` decorator:

``` C#
  [Theory]
  [MemberData(nameof(TestData))]
  public void AddManyNumbersShouldEqualTheirEqualTheory(
    decimal expected, params decimal[] valuesToAdd)
  {
    foreach (var value in valuesToAdd)
    {
      _sut.Add(value);
    }

    Assert.Equal(expected, _sut.Value);
  }
```

Other way to do that is using a class:

``` C#
public class DivisionTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    yield return new object[] {30, new decimal[] { 60, 2 }};
    yield return new object[] {1, new decimal[] { 30, 30 }};
    yield return new object[] {0, new decimal[] { 0, 30 }};
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}
```

The method keeps like when using `MemberData`, going through the object[]. The only change in this case is the decorator `ClassData(typeof())`.

### Behavior of xUnit

By default, xUnit run all tests in paralel.

For change that behavior, use an override class method of the own xUnit:

``` C#
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
```

### Collections

We can use collections to run many tests using the same context.

* `[CollectionDefinition("name")]`
* `public class GuidGeneratorDefinition : ICollectionFixture<GuidGenerator> {}`
* `[Collection("name")]`

This is really important for integrated tests.