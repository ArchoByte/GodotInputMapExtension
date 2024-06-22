using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GodotInputMapExtension;

[Generator]
public class InputMapExtensionGenerator : IIncrementalGenerator
{
    public const string ProjectFile = "project.godot";
    public const string InputDelimeter = "={";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<string>> textProvider = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith(ProjectFile, StringComparison.OrdinalIgnoreCase))
            .Select((file, token) => file.GetText(token)?.ToString())
            .Where(text => text is not null)
            .Collect()!;

        context.RegisterSourceOutput(textProvider, Execute);
    }

    public void Execute(SourceProductionContext context, ImmutableArray<string> args)
    {
        // Validation
        // TODO: Add diagnostics
        if (args.Length < 1)
            throw new FileNotFoundException($"{ProjectFile} file was not found");

        string content = args[0];
        var lines = content.Split('\n');

        // Get input actions names
        var enumNames = lines.Select(s => s.Before(InputDelimeter))
            .Where(s => s != string.Empty);
        var enumNamesPascalCase = enumNames.Select(s => s.SnakeCaseToPascalCase());

        // Generate enum code
        string enumCode = $$"""
            namespace GodotInputMapExtension;
            public enum InputAction
            {
                {{string.Join(",\n    ", enumNamesPascalCase)}}
            }
            """;
        context.AddSource("InputAction.g.cs", SourceText.From(enumCode, Encoding.UTF8));

        // Generate helper class code
        var zippedNames = enumNames.Zip(enumNamesPascalCase, (sc, pc) => new { sc, pc });
        var helperConsts = zippedNames.Select(s => $"""public const string {s.pc} = "{s.sc}";""");
        var helperDictElems = zippedNames.Select(s => $$"""{ InputAction.{{s.pc}}, {{s.pc}} }""");
        string helperCode = $$"""
            using System.Collections.Immutable;
            namespace GodotInputMapExtension;
            public static class InputActionHelper
            {
                {{string.Join("\n    ", helperConsts)}}

                private static readonly Dictionary<InputAction, string> _names = new() {
                    {{string.Join(",\n        ", helperDictElems)}}
                };

                public static readonly ImmutableDictionary<InputAction, string> Names = _names.ToImmutableDictionary();
            }
            """;
        context.AddSource("InputActionHelper.g.cs", SourceText.From(helperCode, Encoding.UTF8));
    }
}
