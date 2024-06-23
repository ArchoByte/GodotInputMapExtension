# Godot Input Map Extension

A source generator, which provides an enum definition and helper class for mapped inputs.

It exposes `GodotInputMapExtension` namespace containing:

* `ActionInput` is a enum with elements corresponding to inputs specified in `Project > Input Map` tab.
* `ActionInputHelper` is a helper class that contains:
  * Constants corresponding to mapped inputs.
  * `Names` dictionary with `ActionInput` keys and `string` values with raw input names.

All members of classes and enums are formatted in pascal case.
