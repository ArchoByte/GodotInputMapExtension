# Godot Input Map Extension

A source generator, which provides an enum definition and helper class for mapped inputs.

## Installation

In order to use it, you need to specify `project.godot` in your `.csproj` file.

```xml
<ItemGroup>
  <AdditionalFiles Include="project.godot" />
</ItemGroup>
```

## Usage

This generator exposes `GodotInputMapExtension` namespace containing:

* `ActionInput` enum with elements corresponding to inputs specified in `Project > Input Map` tab.
* `ActionInputHelper` static class that contains:
  * Constants corresponding to mapped inputs.
  * `Names` dictionary with `ActionInput` keys and `string` values with raw input name.

All members of classes and enums are formatted in pascal case.
