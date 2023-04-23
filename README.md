# GeometryDashAPI [![Build Status](https://travis-ci.org/Folleach/GeometryDashAPI.svg?branch=master)](https://travis-ci.org/Folleach/GeometryDashAPI)
Library for working with game data.


### Such as:
- Levels (Colors, blocks and other).
- Game data in files.
- Game process.
- Remote server data.

## Download and use 
1. Install [`GeometryDashAPI`](https://www.nuget.org/packages/GeometryDashAPI/) from Nuget
2. The end. You can use it :)

## Examples
Now you can see examples [here](https://github.com/Folleach/GeometryDashAPI/tree/master/Examples).


## Introduce

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