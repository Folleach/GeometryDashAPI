# GeometryDashAPI
Library for working with game data.


### Such as:
- Levels (Colors, blocks and other).
- Game data in files.
- Game process.
- Remote server data.

## Download and use 
1. Install [`GeometryDashAPI`](https://www.nuget.org/packages/GeometryDashAPI/) from Nuget
2. The end. You can use it :)

## Versions convention

The main version pattern is `0.x.y`  
Where `x` is this some new feature or a big rework  
Where `y` is this a small fix [bug]  

Sometimes there are `-alpha` suffix appears.  
This is means experiment release for include this library to other projects

Major 0 will be change to 1 when library will support most features of game

Every version should contains a tag, with prefix `v`.  
Example: `v1.2.3-alpha`

## Examples
Now you can see examples [here](https://github.com/Folleach/GeometryDashAPI/tree/master/Examples).


## Introduce

### LLParserSpan
A zero-allocation alternative for [Split](https://learn.microsoft.com/ru-ru/dotnet/api/system.string.split?view=net-8.0) method on string

For example
```cs
var input = "x1,x2,x3,x4";
var separator = ",";
var parser = new LLParserSpan(separator, input);
```

There are a few things you can do here
#### 1. Find the number of array count
```csharp
Console.WriteLine(parser.GetCountOfValues());
```
Output:
```
4
```

#### 2. Enumerate a values of input
```csharp
ReadOnlySpan<char> value;
while ((value = parser.Next()) != null)
    Console.WriteLine(value.ToString());
```
Output:
```
x1
x2
x3
x4
```

#### 3. Enumerate a values as key value pairs
```csharp
while (parser.TryParseNext(out var key, out var value))
    Console.WriteLine($"{key} = {value}");
```
Output:
```
x1 = x2
x3 = x4
```

Benchmark

|                       Method | ItemsCount | ValueLength |      Mean |     Error |    StdDev | Code Size |   Gen0 |   Gen1 | Allocated |
|----------------------------- |----------- |------------ |----------:|----------:|----------:|----------:|-------:|-------:|----------:|
| LLParserSpan_EnumerateValues |      10000 |           1 |  63.53 us |  1.240 us |  1.967 us |     341 B |      - |      - |         - |
|        Split_EnumerateValues |      10000 |           1 | 293.70 us |  5.780 us |  8.289 us |     880 B | 3.4180 | 0.9766 |  320025 B |
| LLParserSpan_EnumerateValues |      10000 |           5 | 176.33 us |  2.805 us |  2.624 us |     341 B |      - |      - |         - |
|        Split_EnumerateValues |      10000 |           5 | 296.32 us |  5.674 us | 10.516 us |     880 B | 4.3945 | 1.9531 |  400025 B |
| LLParserSpan_EnumerateValues |      10000 |          20 | 688.86 us | 10.436 us |  9.252 us |     341 B |      - |      - |       1 B |
|        Split_EnumerateValues |      10000 |          20 | 542.46 us | 10.635 us | 18.904 us |     880 B | 7.8125 | 2.9297 |  720025 B |

### TypeDescriptor\<T\>
Uses for serialize and deserialize classes in GeometryDash _csv like_ format.  
Made to convert any contracts as quickly as possible. Uses the expression inside compilation

#### Example
If you want to create your custom contract. You can define a class, like this
```cs
[Sense(":")]
public class MyGameContract : GameObject
{
    [GameProperty("1", Order = 1)]
    public string Name { get; set; }

    [GameProperty("2", Order = 2)]
    public PlayerSpeed Speed { get; set; }

    [GameProperty("4", Order = 3)]
    [ArraySense(",")]
    public int[] Groups { get; set; }
}
```
There are several points here  
1. Defined class should be inherited from `GameObject`.  
   This base class guarantees the safety of undefined fields in contact.
   If you define a property with a non-existent key `3`, then deserialize and serialize back, then the property will remain, despite the fact that it is missing in the definition.
2. Should add `SenseAttribute` to the defined class.  
   This attribute tells which separator is used for the contract in _csv like_ format.  
   e.g. current contract will be serialized as `1:Player:2:1:3:NULL`
3. Each property used in contract will must contain the `GamePropertyAttribute`.  
   This attribute tells, how serialize and deserialize a property.  
   Applies to properties and fields
4. Some arrays or `List<T>` can define as GameProperty too.  
   But you should add `ArraySenseAttribute` to it. Because _csv like_ format nedded separator for serialize each item in your collection

#### Uses  
Create a new instance of `TypeDescriptor<T>`.  
_not recomended way, because **each descriptor creates for a long time and has caches**_
```cs
var descriptor = new TypeDescriptor<MyGameContract>();

// Create a new instance of MyGameContract
var instance = descriptor.Create();

// Or create a new instance of `MyGameContract` from the string representation (deserialize)
instance = descriptor.Create("1:Player:2:1:3:NULL");

// some changes...
instance.Name = "Folleach";

// Serialize to the string
var builder = new StringBuilder();
instance.CopyTo(instance, builder);
Console.WriteLine(builder.ToString()); // will return 1:Folleach:2:1:3:NULL
```

Also you can define some arrays or `List<T>` in your contract.

## Testing
There are GeometryDashAPI.Tests project for testing, it's contains

todo: set a links to files with tests

- Unit tests (**runs in CI**)
- Integration test for server response on a large data set (**do not runs in CI**)  
  It's needs to set `GDAPI_TESTS_CONTENTS` env variable that points to the folder with the `levels` file.  
  `levels` should contains a geometry dash response from server in each line
- Integration test for game resources (**do not runs in CI**)  
  It's needs to set `GDAPI_TESTS_RESOURCES` env variable that points to the `Resources` folder of game


## Used libraries
| Name        | Link                                                 |
|-------------|------------------------------------------------------|
| SharpZipLib | [GitHub](https://github.com/icsharpcode/SharpZipLib) |
| csFastFloat | [GitHub](https://github.com/CarlVerret/csFastFloat)  |
| UrlBase64   | [GitHub](https://github.com/neosmart/UrlBase64)      |


