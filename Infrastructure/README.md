# Compiler Infrastructure
This document describes the projects in the ZSharp solution and the part they take in the compiler.

## Projects
There are currently 4 projects (hopefully, in the next version, there will be more. see [#1](https://github.com/xpodev/zsharp-compiler/issues/1))

| Name           | Description                                                                                                                                                                      |
|----------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ZSharpCompiler | This is the project that builds main executable. It is responsible of loading a language engine and driving.                                                                     |
| ZSharp.Core    | This project holds common types for all language engines. They are used by the ZSharpCompiler project to drive the compilation process.                                          |
| ZSharp.Parser  | This project holds the default parser for Z#. The parser is a dynamic parser. This project is only referenced by ZSharp.Engine.                                                  |
| ZSharp.Engine  | This is the actual language engine for Z#. This project contains all the objects required in order to compile Z# programs and is also responsible of generating the output file. |


### ZSharpCompiler
[This project](https://github.com/xpodev/zsharp-compiler/tree/wiki-infrastructure-1/Infrastructure/ZSharpCompiler.md) produces the compiler executable. 
It contains only a few files. 
It is responsible to:
 * Parse command line arguments
 * Load the language engine (according to the command line arguments) and initialize it.
 * Read all the files (from command line arguments) and parse them with the engine's parser.
 * Process the files until no more processing is done.
 * Finalize the compilation.
 * Print out the all the errors that were accumulated in the compilation process.


### ZSharp.Core
This project holds all the common types and interfaces. 
It basically defines what a language engine is.


### ZSharp.Parser
This project defines the default parser for Z#. 
This parser is used by ZSharp.Engine. 
The parser is a dynamic parser and it supports defining custom keywords, operators and keyword literals.

Currently, the parser is only built once so defining custom keywords/operators is not allowed at compile time.


### ZSharp.Engine
This is the core of the language. 
It provides the engine that is responsible of compiling Z# programs.
The engine is quite complex and will be described in another document.
