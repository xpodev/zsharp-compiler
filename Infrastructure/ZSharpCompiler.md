# ZSharpCompiler
This document describes the [ZSharpCompiler project](https://github.com/xpodev/zsharp-compiler/tree/development/ZSharpCompiler/).
Since this project is what drives the whole compilation process, this document will describe the processing from a top-level view.


## Entry Point
The compilation begins at the program's [entry point](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L8). 
There it parses the command line arguments using the [coomandline parser library](https://github.com/commandlineparser/commandline).

After it successfully parsed the arguments, the compilation process begins by 
[creating a new compiler](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L29), 
[setting it up](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L31),
[getting a complete list of the files to compile](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L35),
[running the compiler](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L43)
and [finializing](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Program.cs#L45) (effectively writing an executable).

Currently, there's no explanation on how to use the compiler CLI, but when there will be, it'll be in the [main page](https://github.com/xpodev/zsharp-compiler/).


## Compiler
When the compiler is created, it is given the CLI options that were parsed. 
One of these options is a path to a .dll file which contains the language engine (`ZSharp.Engine.dll` by default).
It [loads it up](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L21)
and [creates an instance](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L24)
of the engine. If the engine could not be created, an [error is thrown](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L32)
and the compiler crashes.

The [setup](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L35)
stage is responsible for [adding all assembly references](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L40)
passed to the compiler in the command line arguments and 
[setting up the engine](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L47).

The compilation process is made of smaller processes, each one gets a list of `BuildResult`s and returns a list of `BuildResult`s.
`BuildResult` is a type that holds a single object (the result object, e.g a function) alongside with a list of errors. Errors are ususally message + file position.

So the compilation process starts with a `BuildResult` with no errors and a single node in a file, and runs the current processor on it. 
It then takes the result list and passes it into the next processor and so on...

At the end, where the compiler has no more processors left, it takes all the `BuildResult`s and prints their errors to the console.


## Finalizing
This is a very simple step. It actually just [calls the engine's `FinishCompilation` method](https://github.com/xpodev/zsharp-compiler/blob/5aaee7a4aa3fea5ef36dab633612be6dedbee361/ZSharpCompiler/Compiler.cs#L100)
, which should write an executable file somewhere on the disk according
to the command line arguments.
