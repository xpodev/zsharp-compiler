namespace ZSharp.Cil
{
    /// <summary>
    /// Cil opcode names.
    /// </summary>
    public enum OpCode
    {
        /// <summary>
        /// Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        Add,
        /// <summary>
        /// Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Add_Ovf,
        /// <summary>
        /// Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Add_Ovf_Un,
        /// <summary>
        /// Computes the bitwise AND of two values and pushes the result onto the evaluation stack.
        /// </summary>
        And,
        /// <summary>
        /// Returns an unmanaged pointer to the argument list of the current method.
        /// </summary>
        Arglist,
        /// <summary>
        /// Transfers control to a target instruction if two values are equal.
        /// </summary>
        Beq,
        /// <summary>
        /// Transfers control to a target instruction (short form) if two values are equal.
        /// </summary>
        Beq_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than or equal to the second value.
        /// </summary>
        Bge,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.
        /// </summary>
        Bge_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Bge_Un,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Bge_Un_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value.
        /// </summary>
        Bgt,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value.
        /// </summary>
        Bgt_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Bgt_Un,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Bgt_Un_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value.
        /// </summary>
        Ble,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.
        /// </summary>
        Ble_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Ble_Un,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Ble_Un_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value.
        /// </summary>
        Blt,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than the second value.
        /// </summary>
        Blt_S,
        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Blt_Un,
        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        Blt_Un_S,
        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        Bne_Un,
        /// <summary>
        /// Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        Bne_Un_S,
        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        Box,
        /// <summary>
        /// Unconditionally transfers control to a target instruction.
        /// </summary>
        Br,
        /// <summary>
        /// Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.
        /// </summary>
        Break,
        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference (Nothing in Visual Basic), or zero.
        /// </summary>
        Brfalse,
        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference, or zero.
        /// </summary>
        Brfalse_S,
        /// <summary>
        /// Transfers control to a target instruction if value is true, not null, or non-zero.
        /// </summary>
        Brtrue,
        /// <summary>
        /// Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
        /// </summary>
        Brtrue_S,
        /// <summary>
        /// Unconditionally transfers control to a target instruction (short form).
        /// </summary>
        Br_S,
        /// <summary>
        /// Calls the method indicated by the passed method descriptor.
        /// </summary>
        Call,
        /// <summary>
        /// Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.
        /// </summary>
        Calli,
        /// <summary>
        /// Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        /// </summary>
        Callvirt,
        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        Castclass,
        /// <summary>
        /// Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        Ceq,
        /// <summary>
        /// Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        Cgt,
        /// <summary>
        /// Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        Cgt_Un,
        /// <summary>
        /// Throws <see cref="ArithmeticException"/> if value is not a finite number.
        /// </summary>
        Ckfinite,
        /// <summary>
        /// Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        Clt,
        /// <summary>
        /// Compares the unsigned or unordered values value1 and value2. If value1 is less than value2, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        Clt_Un,
        /// <summary>
        /// Constrains the type on which a virtual method call is made.
        /// </summary>
        Constrained,
        /// <summary>
        /// Converts the value on top of the evaluation stack to native int.
        /// </summary>
        Conv_I,
        /// <summary>
        /// Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.
        /// </summary>
        Conv_I1,
        /// <summary>
        /// Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.
        /// </summary>
        Conv_I2,
        /// <summary>
        /// Converts the value on top of the evaluation stack to int32.
        /// </summary>
        Conv_I4,
        /// <summary>
        /// Converts the value on top of the evaluation stack to int64.
        /// </summary>
        Conv_I8,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I1,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I1_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I2,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I2_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I4,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I4_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I8,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I8_Un,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_I_Un,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U1,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U1_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U2,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U2_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U4,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U4_Un,
        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U8,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U8_Un,
        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing OverflowException on overflow.
        /// </summary>
        Conv_Ovf_U_Un,
        /// <summary>
        /// Converts the value on top of the evaluation stack to float32.
        /// </summary>
        Conv_R4,
        /// <summary>
        /// Converts the value on top of the evaluation stack to float64.
        /// </summary>
        Conv_R8,
        /// <summary>
        /// Converts the unsigned integer value on top of the evaluation stack to float32.
        /// </summary>
        Conv_R_Un,
        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.
        /// </summary>
        Conv_U,
        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.
        /// </summary>
        Conv_U1,
        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.
        /// </summary>
        Conv_U2,
        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.
        /// </summary>
        Conv_U4,
        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.
        /// </summary>
        Conv_U8,
        /// <summary>
        /// Copies a specified number bytes from a source address to a destination address.
        /// </summary>
        Cpblk,
        /// <summary>
        /// Copies the value type located at the address of an object (type &amp;, or native int) to the address of the destination object (type &amp;, or native int).
        /// </summary>
        Cpobj,
        /// <summary>
        /// Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        Div,
        /// <summary>
        /// Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        Div_Un,
        /// <summary>
        /// Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        Dup,
        /// <summary>
        /// Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        Endfilter,
        /// <summary>
        /// Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        Endfinally,
        /// <summary>
        /// Initializes a specified block of memory at a specific address to a given size and initial value.
        /// </summary>
        Initblk,
        /// <summary>
        /// Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
        /// </summary>
        Initobj,
        /// <summary>
        /// Tests whether an object reference (type O) is an instance of a particular class.
        /// </summary>
        Isinst,
        /// <summary>
        /// Exits current method and jumps to specified method.
        /// </summary>
        Jmp,
        /// <summary>
        /// Loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        Ldarg,
        /// <summary>
        /// Load an argument address onto the evaluation stack.
        /// </summary>
        Ldarga,
        /// <summary>
        /// Load an argument address, in short form, onto the evaluation stack.
        /// </summary>
        Ldarga_S,
        /// <summary>
        /// Loads the argument at index 0 onto the evaluation stack.
        /// </summary>
        Ldarg_0,
        /// <summary>
        /// Loads the argument at index 1 onto the evaluation stack.
        /// </summary>
        Ldarg_1,
        /// <summary>
        /// Loads the argument at index 2 onto the evaluation stack.
        /// </summary>
        Ldarg_2,
        /// <summary>
        /// Loads the argument at index 3 onto the evaluation stack.
        /// </summary>
        Ldarg_3,
        /// <summary>
        /// Loads the argument (referenced by a specified short form index) onto the evaluation stack.
        /// </summary>
        Ldarg_S,
        /// <summary>
        /// Pushes a supplied value of type int32 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4,
        /// <summary>
        /// Pushes the integer value of 0 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_0,
        /// <summary>
        /// Pushes the integer value of 1 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_1,
        /// <summary>
        /// Pushes the integer value of 2 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_2,
        /// <summary>
        /// Pushes the integer value of 3 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_3,
        /// <summary>
        /// Pushes the integer value of 4 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_4,
        /// <summary>
        /// Pushes the integer value of 5 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_5,
        /// <summary>
        /// Pushes the integer value of 6 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_6,
        /// <summary>
        /// Pushes the integer value of 7 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_7,
        /// <summary>
        /// Pushes the integer value of 8 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_8,
        /// <summary>
        /// Pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        Ldc_I4_M1,
        /// <summary>
        /// Pushes the supplied int8 value onto the evaluation stack as an int32, short form.
        /// </summary>
        Ldc_I4_S,
        /// <summary>
        /// Pushes a supplied value of type int64 onto the evaluation stack as an int64.
        /// </summary>
        Ldc_I8,
        /// <summary>
        /// Pushes a supplied value of type float32 onto the evaluation stack as type F (float).
        /// </summary>
        Ldc_R4,
        /// <summary>
        /// Pushes a supplied value of type float64 onto the evaluation stack as type F (float).
        /// </summary>
        Ldc_R8,
        /// <summary>
        /// Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction.
        /// </summary>
        Ldelem,
        /// <summary>
        /// Loads the address of the array element at a specified array index onto the top of the evaluation stack as type &amp; (managed pointer).
        /// </summary>
        Ldelema,
        /// <summary>
        /// Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.
        /// </summary>
        Ldelem_I,
        /// <summary>
        /// Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_I1,
        /// <summary>
        /// Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_I2,
        /// <summary>
        /// Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_I4,
        /// <summary>
        /// Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.
        /// </summary>
        Ldelem_I8,
        /// <summary>
        /// Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        Ldelem_R4,
        /// <summary>
        /// Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        Ldelem_R8,
        /// <summary>
        /// Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).
        /// </summary>
        Ldelem_Ref,
        /// <summary>
        /// Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_U1,
        /// <summary>
        /// Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_U2,
        /// <summary>
        /// Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        Ldelem_U4,
        /// <summary>
        /// Finds the value of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        Ldfld,
        /// <summary>
        /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        Ldflda,
        /// <summary>
        /// Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.
        /// </summary>
        Ldftn,
        /// <summary>
        /// Loads a value of type native int as a native int onto the evaluation stack indirectly.
        /// </summary>
        Ldind_I,
        /// <summary>
        /// Loads a value of type int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_I1,
        /// <summary>
        /// Loads a value of type int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_I2,
        /// <summary>
        /// Loads a value of type int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_I4,
        /// <summary>
        /// Loads a value of type int64 as an int64 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_I8,
        /// <summary>
        /// Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        Ldind_R4,
        /// <summary>
        /// Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        Ldind_R8,
        /// <summary>
        /// Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
        /// </summary>
        Ldind_Ref,
        /// <summary>
        /// Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_U1,
        /// <summary>
        /// Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_U2,
        /// <summary>
        /// Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        Ldind_U4,
        /// <summary>
        /// Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.
        /// </summary>
        Ldlen,
        /// <summary>
        /// Loads the local variable at a specific index onto the evaluation stack.
        /// </summary>
        Ldloc,
        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        Ldloca,
        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        Ldloca_S,
        /// <summary>
        /// Loads the local variable at index 0 onto the evaluation stack.
        /// </summary>
        Ldloc_0,
        /// <summary>
        /// Loads the local variable at index 1 onto the evaluation stack.
        /// </summary>
        Ldloc_1,
        /// <summary>
        /// Loads the local variable at index 2 onto the evaluation stack.
        /// </summary>
        Ldloc_2,
        /// <summary>
        /// Loads the local variable at index 3 onto the evaluation stack.
        /// </summary>
        Ldloc_3,
        /// <summary>
        /// Loads the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        Ldloc_S,
        /// <summary>
        /// Pushes a null reference (type O) onto the evaluation stack.
        /// </summary>
        Ldnull,
        /// <summary>
        /// Copies the value type object pointed to by an address to the top of the evaluation stack.
        /// </summary>
        Ldobj,
        /// <summary>
        /// Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        Ldsfld,
        /// <summary>
        /// Pushes the address of a static field onto the evaluation stack.
        /// </summary>
        Ldsflda,
        /// <summary>
        /// Pushes a new object reference to a string literal stored in the metadata.
        /// </summary>
        Ldstr,
        /// <summary>
        /// Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
        /// </summary>
        Ldtoken,
        /// <summary>
        /// Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.
        /// </summary>
        Ldvirtftn,
        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a specific target instruction.
        /// </summary>
        Leave,
        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        Leave_S,
        /// <summary>
        /// Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.
        /// </summary>
        Localloc,
        /// <summary>
        /// Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        Mkrefany,
        /// <summary>
        /// Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        Mul,
        /// <summary>
        /// Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Mul_Ovf,
        /// <summary>
        /// Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Mul_Ovf_Un,
        /// <summary>
        /// Negates a value and pushes the result onto the evaluation stack.
        /// </summary>
        Neg,
        /// <summary>
        /// Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
        /// </summary>
        Newarr,
        /// <summary>
        /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        Newobj,
        /// <summary>
        /// Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.
        /// </summary>
        Nop,
        /// <summary>
        /// Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        Not,
        /// <summary>
        /// Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.
        /// </summary>
        Or,
        /// <summary>
        /// Removes the value currently on top of the evaluation stack.
        /// </summary>
        Pop,
        /// <summary>
        /// Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.
        /// </summary>
        Readonly,
        /// <summary>
        /// Retrieves the type token embedded in a typed reference.
        /// </summary>
        Refanytype,
        /// <summary>
        /// Retrieves the address (type &amp;) embedded in a typed reference.
        /// </summary>
        Refanyval,
        /// <summary>
        /// Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        Rem,
        /// <summary>
        /// Divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        Rem_Un,
        /// <summary>
        /// Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
        /// </summary>
        Ret,
        /// <summary>
        /// Rethrows the current exception.
        /// </summary>
        Rethrow,
        /// <summary>
        /// Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        Shl,
        /// <summary>
        /// Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        Shr,
        /// <summary>
        /// Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        Shr_Un,
        /// <summary>
        /// Pushes the size, in bytes, of a supplied value type onto the evaluation stack.
        /// </summary>
        Sizeof,
        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        Starg,
        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.
        /// </summary>
        Starg_S,
        /// <summary>
        /// Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.
        /// </summary>
        Stelem,
        /// <summary>
        /// Replaces the array element at a given index with the native int value on the evaluation stack.
        /// </summary>
        Stelem_I,
        /// <summary>
        /// Replaces the array element at a given index with the int8 value on the evaluation stack.
        /// </summary>
        Stelem_I1,
        /// <summary>
        /// Replaces the array element at a given index with the int16 value on the evaluation stack.
        /// </summary>
        Stelem_I2,
        /// <summary>
        /// Replaces the array element at a given index with the int32 value on the evaluation stack.
        /// </summary>
        Stelem_I4,
        /// <summary>
        /// Replaces the array element at a given index with the int64 value on the evaluation stack.
        /// </summary>
        Stelem_I8,
        /// <summary>
        /// Replaces the array element at a given index with the float32 value on the evaluation stack.
        /// </summary>
        Stelem_R4,
        /// <summary>
        /// Replaces the array element at a given index with the float64 value on the evaluation stack.
        /// </summary>
        Stelem_R8,
        /// <summary>
        /// Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
        /// </summary>
        Stelem_Ref,
        /// <summary>
        /// Replaces the value stored in the field of an object reference or pointer with a new value.
        /// </summary>
        Stfld,
        /// <summary>
        /// Stores a value of type native int at a supplied address.
        /// </summary>
        Stind_I,
        /// <summary>
        /// Stores a value of type int8 at a supplied address.
        /// </summary>
        Stind_I1,
        /// <summary>
        /// Stores a value of type int16 at a supplied address.
        /// </summary>
        Stind_I2,
        /// <summary>
        /// Stores a value of type int32 at a supplied address.
        /// </summary>
        Stind_I4,
        /// <summary>
        /// Stores a value of type int64 at a supplied address.
        /// </summary>
        Stind_I8,
        /// <summary>
        /// Stores a value of type float32 at a supplied address.
        /// </summary>
        Stind_R4,
        /// <summary>
        /// Stores a value of type float64 at a supplied address.
        /// </summary>
        Stind_R8,
        /// <summary>
        /// Stores a object reference value at a supplied address.
        /// </summary>
        Stind_Ref,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at a specified index.
        /// </summary>
        Stloc,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 0.
        /// </summary>
        Stloc_0,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 1.
        /// </summary>
        Stloc_1,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 2.
        /// </summary>
        Stloc_2,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 3.
        /// </summary>
        Stloc_3,
        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index (short form).
        /// </summary>
        Stloc_S,
        /// <summary>
        /// Copies a value of a specified type from the evaluation stack into a supplied memory address.
        /// </summary>
        Stobj,
        /// <summary>
        /// Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        Stsfld,
        /// <summary>
        /// Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        Sub,
        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Sub_Ovf,
        /// <summary>
        /// Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        Sub_Ovf_Un,
        /// <summary>
        /// Implements a jump table.
        /// </summary>
        Switch,
        /// <summary>
        /// Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.
        /// </summary>
        Tailcall,
        /// <summary>
        /// Throws the exception object currently on the evaluation stack.
        /// </summary>
        Throw,
        /// <summary>
        /// Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.
        /// </summary>
        Unaligned,
        /// <summary>
        /// Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        Unbox,
        /// <summary>
        /// Converts the boxed representation of a type specified in the instruction to its unboxed form.
        /// </summary>
        Unbox_Any,
        /// <summary>
        /// Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.
        /// </summary>
        Volatile,
        /// <summary>
        /// Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.
        /// </summary>
        Xor
    }
}
