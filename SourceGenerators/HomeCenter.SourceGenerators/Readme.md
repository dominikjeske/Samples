# SourceGenerators

## Links
C:\Users\domin\AppData\Local\Temp\VisualStudioSourceGeneratedDocuments

https://github.com/dotnet/roslyn/blob/master/docs/features/source-generators.cookbook.md
https://devblogs.microsoft.com/dotnet/new-c-source-generator-samples/
https://www.cazzulino.com/source-generators.html


https://github.com/KathleenDollard/StarFruit2
https://github.com/b-straub/BlazorSourceGeneratorTests/blob/master/Tests/GeneratorTestFactory.cs
https://github.com/dotnet/roslyn/issues/48522
https://github.com/dotnet/roslyn/discussions/47517#discussioncomment-64145

##Debug:
https://jaylee.org/archive/2020/10/10/profiling-csharp-9-source-generators.html
https://nicksnettravels.builttoroam.com/debug-code-gen/
https://github.com/chsienki/kittitas

#Scriban
https://github.com/lunet-io/scriban/blob/master/doc/language.md#93-loops


TODO:
System.CodeDom.Compiler.GeneratedCode()
System.Runtime.CompilerServices.CompilerGenerated




Is the project consuming the source generator using <LangVersion>preview</LangVersion>? I ran into a similar issue yesterday and it turned out that my generator was being loaded but wasn't actually running since it seems source generators are still locked behind the preview gate in RC1.
Glad you figured this out. The change is because previously <TargetFramework>net5.0</TargetFramework> was implicitly targeting <LangVersion>preview</LangVersion> so you didn't have to opt into that.

We've now updated net5.0 to target lang version 9, meaning to use source generators you have to explicitly opt in with lang version Preview.

We'll be removing this restriction in an upcoming release.


var diagnostics = compilation.GetDiagnostics();
if (!VerifyDiagnostics(diagnostics, new[] { "CS0012", "CS0616", "CS0246" }))
{
    // this will make the test fail, check the input source code!
    return diagnostics;
}


public static bool VerifyDiagnostics(ImmutableArray<Diagnostic> actual, string [] expected)
{
    return actual.Where(d => d.Severity == DiagnosticSeverity.Error)
            .Select(d => d.Id.ToString())
            .All(id => expected.Contains(id)); ;
}