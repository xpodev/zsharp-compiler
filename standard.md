# Compilation Process
A Z# project is compiled to an intermediate format called ZPL (Z# program library). 
This format can then be converted to other executable formats by a matching compiler.

The only supported intermediate compiler is from ZPL to IL (CLR).


A ZPL file contains an exact representation of the Z# project used to generate it.
ZPL files cam be referenced/imported by other projects to get access to items defined in it.
A ZPL file may define any of:
- Constants
- Types
- Custom Modifiers
- Functions
- Interfaces
- Type classes
- Type aliases
- Enums
- Custom parsers
- Custom operators
- Namespaces
When a ZPL file is referenced/imported, it automatically imports all other referenced ZPL files.



