# Godot Input Map Extension

A source generator, which provides an enum definition and helper class for mapped inputs.

It exposes `GodotInputMapExtension` namespace containing:

* `ActionInput` enum with elements corresponding to inputs specified in `Project > Input Map` tab.
* `ActionInputHelper` static class that contains:
  * Constants corresponding to mapped inputs.
  * `Names` dictionary with `ActionInput` keys and `string` values with raw input name.

All members of classes and enums are formatted in pascal case.
