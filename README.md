# Z# Compiler
This repository contains the source code of the Z# programming language.

Z# is a multi-paradigm, statically typed, dynamically compiled programming language.
It compiles to IL code which means it runs on the .NET ecosystem (currently .NET5 and .NET6) and so it can also interact with programs written in C# or F#.


## Requirements
To build the compiler you need:
 - .NET5/6 SDK
 - The Z# compiler [source code](https://github.com/xpodev/zsharp-compiler/tree/development) (currently on a different branch)

After you have the above requirements, navigate to the solution folder and execute the following command:
```cmd
dotnet build --project ZSharpCompiler
```
If you want to build for a specific target runtime, add one of the following options
```cmd
-f net5.0
-f net6.0
```

# Features:
❌ - Not implemented yet

✅ - Working on it

✔️ - Implemented

🚧 - Planned

| Feature               |                    |                  Notes                 |
|-----------------------|:------------------:|:--------------------------------------:|
| Basic language syntax | :heavy_check_mark: |                                        |
| Code generation       | :heavy_check_mark: |                                        |
| Debugging with PDB    |   :construction:   | Will only be available on Windows      |
| Static reflection     | :heavy_check_mark: |                                        |
| Inline IL code        | :white_check_mark: | Not all instructions implemented yet   |
| Types                 | :x:                | Classes, interfaces, enums and structs |
| Lambda/Currying       | :x:                |                                        |
| Data type             | :x:                |                                        |
| Better error messages | :x:                |                                        |
| Type inference        | :construction:     |                                        |
