using System;
using MSIL = System.Reflection.Emit;
using MCIL = Mono.Cecil.Cil;

namespace ZSharp.Engine.Cil
{
    public static class OpCodes
    {
        /// <summary>
        /// Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Add = new(MSIL.OpCodes.Add, MCIL.OpCodes.Add);

        /// <summary>
        /// Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Add_Ovf = new(MSIL.OpCodes.Add_Ovf, MCIL.OpCodes.Add_Ovf);

        /// <summary>
        /// Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Add_Ovf_Un = new(MSIL.OpCodes.Add_Ovf_Un, MCIL.OpCodes.Add_Ovf_Un);

        /// <summary>
        /// Computes the bitwise AND of two values and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode And = new(MSIL.OpCodes.And, MCIL.OpCodes.And);

        /// <summary>
        /// Returns an unmanaged pointer to the argument list of the current method.
        /// </summary>
        public static readonly OpCode Arglist = new(MSIL.OpCodes.Arglist, MCIL.OpCodes.Arglist);

        /// <summary>
        /// Transfers control to a target instruction if two values are equal.
        /// </summary>
        public static readonly OpCode Beq = new(MSIL.OpCodes.Beq, MCIL.OpCodes.Beq);

        /// <summary>
        /// Transfers control to a target instruction (short form) if two values are equal.
        /// </summary>
        public static readonly OpCode Beq_S = new(MSIL.OpCodes.Beq_S, MCIL.OpCodes.Beq_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than or equal to the second value.
        /// </summary>
        public static readonly OpCode Bge = new(MSIL.OpCodes.Bge, MCIL.OpCodes.Bge);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.
        /// </summary>
        public static readonly OpCode Bge_S = new(MSIL.OpCodes.Bge_S, MCIL.OpCodes.Bge_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Bge_Un = new(MSIL.OpCodes.Bge_Un, MCIL.OpCodes.Bge_Un);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Bge_Un_S = new(MSIL.OpCodes.Bge_Un_S, MCIL.OpCodes.Bge_Un_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value.
        /// </summary>
        public static readonly OpCode Bgt = new(MSIL.OpCodes.Bgt, MCIL.OpCodes.Bgt);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value.
        /// </summary>
        public static readonly OpCode Bgt_S = new(MSIL.OpCodes.Bgt_S, MCIL.OpCodes.Bgt_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Bgt_Un = new(MSIL.OpCodes.Bgt_Un, MCIL.OpCodes.Bgt_Un);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Bgt_Un_S = new(MSIL.OpCodes.Bgt_Un_S, MCIL.OpCodes.Bgt_Un_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value.
        /// </summary>
        public static readonly OpCode Ble = new(MSIL.OpCodes.Ble, MCIL.OpCodes.Ble);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.
        /// </summary>
        public static readonly OpCode Ble_S = new(MSIL.OpCodes.Ble_S, MCIL.OpCodes.Ble_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Ble_Un = new(MSIL.OpCodes.Ble_Un, MCIL.OpCodes.Ble_Un);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Ble_Un_S = new(MSIL.OpCodes.Ble_Un_S, MCIL.OpCodes.Ble_Un_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value.
        /// </summary>
        public static readonly OpCode Blt = new(MSIL.OpCodes.Blt, MCIL.OpCodes.Blt);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than the second value.
        /// </summary>
        public static readonly OpCode Blt_S = new(MSIL.OpCodes.Blt_S, MCIL.OpCodes.Blt_S);

        /// <summary>
        /// Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Blt_Un = new(MSIL.OpCodes.Blt_Un, MCIL.OpCodes.Blt_Un);

        /// <summary>
        /// Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.
        /// </summary>
        public static readonly OpCode Blt_Un_S = new(MSIL.OpCodes.Blt_Un_S, MCIL.OpCodes.Blt_Un_S);

        /// <summary>
        /// Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        public static readonly OpCode Bne_Un = new(MSIL.OpCodes.Bne_Un, MCIL.OpCodes.Bne_Un);

        /// <summary>
        /// Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.
        /// </summary>
        public static readonly OpCode Bne_Un_S = new(MSIL.OpCodes.Bne_Un_S, MCIL.OpCodes.Bne_Un_S);

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        public static readonly OpCode Box = new(MSIL.OpCodes.Box, MCIL.OpCodes.Box);

        /// <summary>
        /// Unconditionally transfers control to a target instruction.
        /// </summary>
        public static readonly OpCode Br = new(MSIL.OpCodes.Br, MCIL.OpCodes.Br);

        /// <summary>
        /// Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.
        /// </summary>
        public static readonly OpCode Break = new(MSIL.OpCodes.Break, MCIL.OpCodes.Break);

        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference (Nothing in Visual Basic), or zero.
        /// </summary>
        public static readonly OpCode Brfalse = new(MSIL.OpCodes.Brfalse, MCIL.OpCodes.Brfalse);

        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference, or zero.
        /// </summary>
        public static readonly OpCode Brfalse_S = new(MSIL.OpCodes.Brfalse_S, MCIL.OpCodes.Brfalse_S);

        /// <summary>
        /// Transfers control to a target instruction if value is true, not null, or non-zero.
        /// </summary>
        public static readonly OpCode Brtrue = new(MSIL.OpCodes.Brtrue, MCIL.OpCodes.Brtrue);

        /// <summary>
        /// Transfers control to a target instruction (short form) if value is true, not null, or non-zero.
        /// </summary>
        public static readonly OpCode Brtrue_S = new(MSIL.OpCodes.Brtrue_S, MCIL.OpCodes.Brtrue_S);

        /// <summary>
        /// Unconditionally transfers control to a target instruction (short form).
        /// </summary>
        public static readonly OpCode Br_S = new(MSIL.OpCodes.Br_S, MCIL.OpCodes.Br_S);

        /// <summary>
        /// Calls the method indicated by the passed method descriptor.
        /// </summary>
        public static readonly OpCode Call = new(MSIL.OpCodes.Call, MCIL.OpCodes.Call);

        /// <summary>
        /// Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.
        /// </summary>
        public static readonly OpCode Calli = new(MSIL.OpCodes.Calli, MCIL.OpCodes.Calli);

        /// <summary>
        /// Calls a late-bound method on an object, pushing the return value onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Callvirt = new(MSIL.OpCodes.Callvirt, MCIL.OpCodes.Callvirt);

        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        public static readonly OpCode Castclass = new(MSIL.OpCodes.Castclass, MCIL.OpCodes.Castclass);

        /// <summary>
        /// Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ceq = new(MSIL.OpCodes.Ceq, MCIL.OpCodes.Ceq);

        /// <summary>
        /// Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Cgt = new(MSIL.OpCodes.Cgt, MCIL.OpCodes.Cgt);

        /// <summary>
        /// Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Cgt_Un = new(MSIL.OpCodes.Cgt_Un, MCIL.OpCodes.Cgt_Un);

        /// <summary>
        /// Throws <see cref="ArithmeticException"/> if value is not a finite number.
        /// </summary>
        public static readonly OpCode Ckfinite = new(MSIL.OpCodes.Ckfinite, MCIL.OpCodes.Ckfinite);

        /// <summary>
        /// Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Clt = new(MSIL.OpCodes.Clt, MCIL.OpCodes.Clt);

        /// <summary>
        /// Compares the unsigned or unordered values value1 and value2. If value1 is less than value2, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Clt_Un = new(MSIL.OpCodes.Clt_Un, MCIL.OpCodes.Clt_Un);

        /// <summary>
        /// Constrains the type on which a virtual method call is made.
        /// </summary>
        public static readonly OpCode Constrained = new(MSIL.OpCodes.Constrained, MCIL.OpCodes.Constrained);

        /// <summary>
        /// Converts the value on top of the evaluation stack to native int.
        /// </summary>
        public static readonly OpCode Conv_I = new(MSIL.OpCodes.Conv_I, MCIL.OpCodes.Conv_I);

        /// <summary>
        /// Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.
        /// </summary>
        public static readonly OpCode Conv_I1 = new(MSIL.OpCodes.Conv_I1, MCIL.OpCodes.Conv_I1);

        /// <summary>
        /// Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.
        /// </summary>
        public static readonly OpCode Conv_I2 = new(MSIL.OpCodes.Conv_I2, MCIL.OpCodes.Conv_I2);

        /// <summary>
        /// Converts the value on top of the evaluation stack to int32.
        /// </summary>
        public static readonly OpCode Conv_I4 = new(MSIL.OpCodes.Conv_I4, MCIL.OpCodes.Conv_I4);

        /// <summary>
        /// Converts the value on top of the evaluation stack to int64.
        /// </summary>
        public static readonly OpCode Conv_I8 = new(MSIL.OpCodes.Conv_I8, MCIL.OpCodes.Conv_I8);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I = new(MSIL.OpCodes.Conv_Ovf_I, MCIL.OpCodes.Conv_Ovf_I);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I1 = new(MSIL.OpCodes.Conv_Ovf_I1, MCIL.OpCodes.Conv_Ovf_I1);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I1_Un = new(MSIL.OpCodes.Conv_Ovf_I1_Un, MCIL.OpCodes.Conv_Ovf_I1_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I2 = new(MSIL.OpCodes.Conv_Ovf_I2, MCIL.OpCodes.Conv_Ovf_I2);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I2_Un = new(MSIL.OpCodes.Conv_Ovf_I2_Un, MCIL.OpCodes.Conv_Ovf_I2_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I4 = new(MSIL.OpCodes.Conv_Ovf_I4, MCIL.OpCodes.Conv_Ovf_I4);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I4_Un = new(MSIL.OpCodes.Conv_Ovf_I4_Un, MCIL.OpCodes.Conv_Ovf_I4_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I8 = new(MSIL.OpCodes.Conv_Ovf_I8, MCIL.OpCodes.Conv_Ovf_I8);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I8_Un = new(MSIL.OpCodes.Conv_Ovf_I8_Un, MCIL.OpCodes.Conv_Ovf_I8_Un);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed native int, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_I_Un = new(MSIL.OpCodes.Conv_Ovf_I_Un, MCIL.OpCodes.Conv_Ovf_I_Un);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to signed int64, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U = new(MSIL.OpCodes.Conv_Ovf_U, MCIL.OpCodes.Conv_Ovf_U);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U1 = new(MSIL.OpCodes.Conv_Ovf_U1, MCIL.OpCodes.Conv_Ovf_U1);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U1_Un = new(MSIL.OpCodes.Conv_Ovf_U1_Un, MCIL.OpCodes.Conv_Ovf_U1_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U2 = new(MSIL.OpCodes.Conv_Ovf_U2, MCIL.OpCodes.Conv_Ovf_U2);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U2_Un = new(MSIL.OpCodes.Conv_Ovf_U2_Un, MCIL.OpCodes.Conv_Ovf_U2_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U4 = new(MSIL.OpCodes.Conv_Ovf_U4, MCIL.OpCodes.Conv_Ovf_U4);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U4_Un = new(MSIL.OpCodes.Conv_Ovf_U4_Un, MCIL.OpCodes.Conv_Ovf_U4_Un);

        /// <summary>
        /// Converts the signed value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U8 = new(MSIL.OpCodes.Conv_Ovf_U8, MCIL.OpCodes.Conv_Ovf_U8);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U8_Un = new(MSIL.OpCodes.Conv_Ovf_U8_Un, MCIL.OpCodes.Conv_Ovf_U8_Un);

        /// <summary>
        /// Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing OverflowException on overflow.
        /// </summary>
        public static readonly OpCode Conv_Ovf_U_Un = new(MSIL.OpCodes.Conv_Ovf_U_Un, MCIL.OpCodes.Conv_Ovf_U_Un);

        /// <summary>
        /// Converts the value on top of the evaluation stack to float32.
        /// </summary>
        public static readonly OpCode Conv_R4 = new(MSIL.OpCodes.Conv_R4, MCIL.OpCodes.Conv_R4);

        /// <summary>
        /// Converts the value on top of the evaluation stack to float64.
        /// </summary>
        public static readonly OpCode Conv_R8 = new(MSIL.OpCodes.Conv_R8, MCIL.OpCodes.Conv_R8);

        /// <summary>
        /// Converts the unsigned integer value on top of the evaluation stack to float32.
        /// </summary>
        public static readonly OpCode Conv_R_Un = new(MSIL.OpCodes.Conv_R_Un, MCIL.OpCodes.Conv_R_Un);

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.
        /// </summary>
        public static readonly OpCode Conv_U = new(MSIL.OpCodes.Conv_U, MCIL.OpCodes.Conv_U);

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.
        /// </summary>
        public static readonly OpCode Conv_U1 = new(MSIL.OpCodes.Conv_U1, MCIL.OpCodes.Conv_U1);

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.
        /// </summary>
        public static readonly OpCode Conv_U2 = new(MSIL.OpCodes.Conv_U2, MCIL.OpCodes.Conv_U2);

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.
        /// </summary>
        public static readonly OpCode Conv_U4 = new(MSIL.OpCodes.Conv_U4, MCIL.OpCodes.Conv_U4);

        /// <summary>
        /// Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.
        /// </summary>
        public static readonly OpCode Conv_U8 = new(MSIL.OpCodes.Conv_U8, MCIL.OpCodes.Conv_U8);

        /// <summary>
        /// Copies a specified number bytes from a source address to a destination address.
        /// </summary>
        public static readonly OpCode Cpblk = new(MSIL.OpCodes.Cpblk, MCIL.OpCodes.Cpblk);

        /// <summary>
        /// Copies the value type located at the address of an object (type &amp;, or native int) to the address of the destination object (type &amp;, or native int).
        /// </summary>
        public static readonly OpCode Cpobj = new(MSIL.OpCodes.Cpobj, MCIL.OpCodes.Cpobj);

        /// <summary>
        /// Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Div = new(MSIL.OpCodes.Div, MCIL.OpCodes.Div);

        /// <summary>
        /// Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Div_Un = new(MSIL.OpCodes.Div_Un, MCIL.OpCodes.Div_Un);

        /// <summary>
        /// Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Dup = new(MSIL.OpCodes.Dup, MCIL.OpCodes.Dup);

        /// <summary>
        /// Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        public static readonly OpCode Endfilter = new(MSIL.OpCodes.Endfilter, MCIL.OpCodes.Endfilter);

        /// <summary>
        /// Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        public static readonly OpCode Endfinally = new(MSIL.OpCodes.Endfinally, MCIL.OpCodes.Endfinally);

        /// <summary>
        /// Initializes a specified block of memory at a specific address to a given size and initial value.
        /// </summary>
        public static readonly OpCode Initblk = new(MSIL.OpCodes.Initblk, MCIL.OpCodes.Initblk);

        /// <summary>
        /// Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.
        /// </summary>
        public static readonly OpCode Initobj = new(MSIL.OpCodes.Initobj, MCIL.OpCodes.Initobj);

        /// <summary>
        /// Tests whether an object reference (type O) is an instance of a particular class.
        /// </summary>
        public static readonly OpCode Isinst = new(MSIL.OpCodes.Isinst, MCIL.OpCodes.Isinst);

        /// <summary>
        /// Exits current method and jumps to specified method.
        /// </summary>
        public static readonly OpCode Jmp = new(MSIL.OpCodes.Jmp, MCIL.OpCodes.Jmp);

        /// <summary>
        /// Loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        public static readonly OpCode Ldarg = new(MSIL.OpCodes.Ldarg, MCIL.OpCodes.Ldarg);

        /// <summary>
        /// Load an argument address onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarga = new(MSIL.OpCodes.Ldarga, MCIL.OpCodes.Ldarga);

        /// <summary>
        /// Load an argument address, in short form, onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarga_S = new(MSIL.OpCodes.Ldarga_S, MCIL.OpCodes.Ldarga_S);

        /// <summary>
        /// Loads the argument at index 0 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarg_0 = new(MSIL.OpCodes.Ldarg_0, MCIL.OpCodes.Ldarg_0);

        /// <summary>
        /// Loads the argument at index 1 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarg_1 = new(MSIL.OpCodes.Ldarg_1, MCIL.OpCodes.Ldarg_1);

        /// <summary>
        /// Loads the argument at index 2 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarg_2 = new(MSIL.OpCodes.Ldarg_2, MCIL.OpCodes.Ldarg_2);

        /// <summary>
        /// Loads the argument at index 3 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarg_3 = new(MSIL.OpCodes.Ldarg_3, MCIL.OpCodes.Ldarg_3);

        /// <summary>
        /// Loads the argument (referenced by a specified short form index) onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldarg_S = new(MSIL.OpCodes.Ldarg_S, MCIL.OpCodes.Ldarg_S);

        /// <summary>
        /// Pushes a supplied value of type int32 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4 = new(MSIL.OpCodes.Ldc_I4, MCIL.OpCodes.Ldc_I4);

        /// <summary>
        /// Pushes the integer value of 0 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_0 = new(MSIL.OpCodes.Ldc_I4_0, MCIL.OpCodes.Ldc_I4_0);

        /// <summary>
        /// Pushes the integer value of 1 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_1 = new(MSIL.OpCodes.Ldc_I4_1, MCIL.OpCodes.Ldc_I4_1);

        /// <summary>
        /// Pushes the integer value of 2 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_2 = new(MSIL.OpCodes.Ldc_I4_2, MCIL.OpCodes.Ldc_I4_2);

        /// <summary>
        /// Pushes the integer value of 3 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_3 = new(MSIL.OpCodes.Ldc_I4_3, MCIL.OpCodes.Ldc_I4_3);

        /// <summary>
        /// Pushes the integer value of 4 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_4 = new(MSIL.OpCodes.Ldc_I4_4, MCIL.OpCodes.Ldc_I4_4);

        /// <summary>
        /// Pushes the integer value of 5 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_5 = new(MSIL.OpCodes.Ldc_I4_5, MCIL.OpCodes.Ldc_I4_5);

        /// <summary>
        /// Pushes the integer value of 6 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_6 = new(MSIL.OpCodes.Ldc_I4_6, MCIL.OpCodes.Ldc_I4_6);

        /// <summary>
        /// Pushes the integer value of 7 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_7 = new(MSIL.OpCodes.Ldc_I4_7, MCIL.OpCodes.Ldc_I4_7);

        /// <summary>
        /// Pushes the integer value of 8 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_8 = new(MSIL.OpCodes.Ldc_I4_8, MCIL.OpCodes.Ldc_I4_8);

        /// <summary>
        /// Pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldc_I4_M1 = new(MSIL.OpCodes.Ldc_I4_M1, MCIL.OpCodes.Ldc_I4_M1);

        /// <summary>
        /// Pushes the supplied int8 value onto the evaluation stack as an int32, short form.
        /// </summary>
        public static readonly OpCode Ldc_I4_S = new(MSIL.OpCodes.Ldc_I4_S, MCIL.OpCodes.Ldc_I4_S);

        /// <summary>
        /// Pushes a supplied value of type int64 onto the evaluation stack as an int64.
        /// </summary>
        public static readonly OpCode Ldc_I8 = new(MSIL.OpCodes.Ldc_I8, MCIL.OpCodes.Ldc_I8);

        /// <summary>
        /// Pushes a supplied value of type float32 onto the evaluation stack as type F (float).
        /// </summary>
        public static readonly OpCode Ldc_R4 = new(MSIL.OpCodes.Ldc_R4, MCIL.OpCodes.Ldc_R4);

        /// <summary>
        /// Pushes a supplied value of type float64 onto the evaluation stack as type F (float).
        /// </summary>
        public static readonly OpCode Ldc_R8 = new(MSIL.OpCodes.Ldc_R8, MCIL.OpCodes.Ldc_R8);

        /// <summary>
        /// Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction.
        /// </summary>
        public static readonly OpCode Ldelem = new(MSIL.OpCodes.Ldelem, MCIL.OpCodes.Ldelem_Any);

        /// <summary>
        /// Loads the address of the array element at a specified array index onto the top of the evaluation stack as type &amp; (managed pointer).
        /// </summary>
        public static readonly OpCode Ldelema = new(MSIL.OpCodes.Ldelema, MCIL.OpCodes.Ldelema);

        /// <summary>
        /// Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.
        /// </summary>
        public static readonly OpCode Ldelem_I = new(MSIL.OpCodes.Ldelem_I, MCIL.OpCodes.Ldelem_I);

        /// <summary>
        /// Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_I1 = new(MSIL.OpCodes.Ldelem_I1, MCIL.OpCodes.Ldelem_I1);

        /// <summary>
        /// Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_I2 = new(MSIL.OpCodes.Ldelem_I2, MCIL.OpCodes.Ldelem_I2);

        /// <summary>
        /// Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_I4 = new(MSIL.OpCodes.Ldelem_I4, MCIL.OpCodes.Ldelem_I4);

        /// <summary>
        /// Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.
        /// </summary>
        public static readonly OpCode Ldelem_I8 = new(MSIL.OpCodes.Ldelem_I8, MCIL.OpCodes.Ldelem_I8);

        /// <summary>
        /// Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        public static readonly OpCode Ldelem_R4 = new(MSIL.OpCodes.Ldelem_R4, MCIL.OpCodes.Ldelem_R4);

        /// <summary>
        /// Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        public static readonly OpCode Ldelem_R8 = new(MSIL.OpCodes.Ldelem_R8, MCIL.OpCodes.Ldelem_R8);

        /// <summary>
        /// Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).
        /// </summary>
        public static readonly OpCode Ldelem_Ref = new(MSIL.OpCodes.Ldelem_Ref, MCIL.OpCodes.Ldelem_Ref);

        /// <summary>
        /// Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_U1 = new(MSIL.OpCodes.Ldelem_U1, MCIL.OpCodes.Ldelem_U1);

        /// <summary>
        /// Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_U2 = new(MSIL.OpCodes.Ldelem_U2, MCIL.OpCodes.Ldelem_U2);

        /// <summary>
        /// Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        public static readonly OpCode Ldelem_U4 = new(MSIL.OpCodes.Ldelem_U4, MCIL.OpCodes.Ldelem_U4);

        /// <summary>
        /// Finds the value of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldfld = new(MSIL.OpCodes.Ldfld, MCIL.OpCodes.Ldfld);

        /// <summary>
        /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldflda = new(MSIL.OpCodes.Ldflda, MCIL.OpCodes.Ldflda);

        /// <summary>
        /// Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldftn = new(MSIL.OpCodes.Ldftn, MCIL.OpCodes.Ldftn);

        /// <summary>
        /// Loads a value of type native int as a native int onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_I = new(MSIL.OpCodes.Ldind_I, MCIL.OpCodes.Ldind_I);

        /// <summary>
        /// Loads a value of type int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_I1 = new(MSIL.OpCodes.Ldind_I1, MCIL.OpCodes.Ldind_I1);

        /// <summary>
        /// Loads a value of type int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_I2 = new(MSIL.OpCodes.Ldind_I2, MCIL.OpCodes.Ldind_I2);

        /// <summary>
        /// Loads a value of type int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_I4 = new(MSIL.OpCodes.Ldind_I4, MCIL.OpCodes.Ldind_I4);

        /// <summary>
        /// Loads a value of type int64 as an int64 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_I8 = new(MSIL.OpCodes.Ldind_I8, MCIL.OpCodes.Ldind_I8);

        /// <summary>
        /// Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_R4 = new(MSIL.OpCodes.Ldind_R4, MCIL.OpCodes.Ldind_R4);

        /// <summary>
        /// Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_R8 = new(MSIL.OpCodes.Ldind_R8, MCIL.OpCodes.Ldind_R8);

        /// <summary>
        /// Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_Ref = new(MSIL.OpCodes.Ldind_Ref, MCIL.OpCodes.Ldind_Ref);

        /// <summary>
        /// Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_U1 = new(MSIL.OpCodes.Ldind_U1, MCIL.OpCodes.Ldind_U1);

        /// <summary>
        /// Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_U2 = new(MSIL.OpCodes.Ldind_U2, MCIL.OpCodes.Ldind_U2);

        /// <summary>
        /// Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        public static readonly OpCode Ldind_U4 = new(MSIL.OpCodes.Ldind_U4, MCIL.OpCodes.Ldind_U4);

        /// <summary>
        /// Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldlen = new(MSIL.OpCodes.Ldlen, MCIL.OpCodes.Ldlen);

        /// <summary>
        /// Loads the local variable at a specific index onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloc = new(MSIL.OpCodes.Ldloc, MCIL.OpCodes.Ldloc);

        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloca = new(MSIL.OpCodes.Ldloca, MCIL.OpCodes.Ldloca);

        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        public static readonly OpCode Ldloca_S = new(MSIL.OpCodes.Ldloca_S, MCIL.OpCodes.Ldloca_S);

        /// <summary>
        /// Loads the local variable at index 0 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloc_0 = new(MSIL.OpCodes.Ldloc_0, MCIL.OpCodes.Ldloc_0);

        /// <summary>
        /// Loads the local variable at index 1 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloc_1 = new(MSIL.OpCodes.Ldloc_1, MCIL.OpCodes.Ldloc_1);

        /// <summary>
        /// Loads the local variable at index 2 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloc_2 = new(MSIL.OpCodes.Ldloc_2, MCIL.OpCodes.Ldloc_2);

        /// <summary>
        /// Loads the local variable at index 3 onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldloc_3 = new(MSIL.OpCodes.Ldloc_3, MCIL.OpCodes.Ldloc_3);

        /// <summary>
        /// Loads the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        public static readonly OpCode Ldloc_S = new(MSIL.OpCodes.Ldloc_S, MCIL.OpCodes.Ldloc_S);

        /// <summary>
        /// Pushes a null reference (type O) onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldnull = new(MSIL.OpCodes.Ldnull, MCIL.OpCodes.Ldnull);

        /// <summary>
        /// Copies the value type object pointed to by an address to the top of the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldobj = new(MSIL.OpCodes.Ldobj, MCIL.OpCodes.Ldobj);

        /// <summary>
        /// Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldsfld = new(MSIL.OpCodes.Ldsfld, MCIL.OpCodes.Ldsfld);

        /// <summary>
        /// Pushes the address of a static field onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldsflda = new(MSIL.OpCodes.Ldsflda, MCIL.OpCodes.Ldsflda);

        /// <summary>
        /// Pushes a new object reference to a string literal stored in the metadata.
        /// </summary>
        public static readonly OpCode Ldstr = new(MSIL.OpCodes.Ldstr, MCIL.OpCodes.Ldstr);

        /// <summary>
        /// Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldtoken = new(MSIL.OpCodes.Ldtoken, MCIL.OpCodes.Ldtoken);

        /// <summary>
        /// Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Ldvirtftn = new(MSIL.OpCodes.Ldvirtftn, MCIL.OpCodes.Ldvirtftn);

        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a specific target instruction.
        /// </summary>
        public static readonly OpCode Leave = new(MSIL.OpCodes.Leave, MCIL.OpCodes.Leave);

        /// <summary>
        /// Exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        public static readonly OpCode Leave_S = new(MSIL.OpCodes.Leave_S, MCIL.OpCodes.Leave_S);

        /// <summary>
        /// Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Localloc = new(MSIL.OpCodes.Localloc, MCIL.OpCodes.Localloc);

        /// <summary>
        /// Pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Mkrefany = new(MSIL.OpCodes.Mkrefany, MCIL.OpCodes.Mkrefany);

        /// <summary>
        /// Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        public static readonly OpCode Mul = new(MSIL.OpCodes.Mul, MCIL.OpCodes.Mul);

        /// <summary>
        /// Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Mul_Ovf = new(MSIL.OpCodes.Mul_Ovf, MCIL.OpCodes.Mul_Ovf);

        /// <summary>
        /// Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Mul_Ovf_Un = new(MSIL.OpCodes.Mul_Ovf_Un, MCIL.OpCodes.Mul_Ovf_Un);

        /// <summary>
        /// Negates a value and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Neg = new(MSIL.OpCodes.Neg, MCIL.OpCodes.Neg);

        /// <summary>
        /// Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Newarr = new(MSIL.OpCodes.Newarr, MCIL.OpCodes.Newarr);

        /// <summary>
        /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Newobj = new(MSIL.OpCodes.Newobj, MCIL.OpCodes.Newobj);

        /// <summary>
        /// Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.
        /// </summary>
        public static readonly OpCode Nop = new(MSIL.OpCodes.Nop, MCIL.OpCodes.Nop);

        /// <summary>
        /// Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        public static readonly OpCode Not = new(MSIL.OpCodes.Not, MCIL.OpCodes.Not);

        /// <summary>
        /// Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Or = new(MSIL.OpCodes.Or, MCIL.OpCodes.Or);

        /// <summary>
        /// Removes the value currently on top of the evaluation stack.
        /// </summary>
        public static readonly OpCode Pop = new(MSIL.OpCodes.Pop, MCIL.OpCodes.Pop);

        /// <summary>
        /// Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.
        /// </summary>
        public static readonly OpCode Readonly = new(MSIL.OpCodes.Readonly, MCIL.OpCodes.Readonly);

        /// <summary>
        /// Retrieves the type token embedded in a typed reference.
        /// </summary>
        public static readonly OpCode Refanytype = new(MSIL.OpCodes.Refanytype, MCIL.OpCodes.Refanytype);

        /// <summary>
        /// Retrieves the address (type &amp;) embedded in a typed reference.
        /// </summary>
        public static readonly OpCode Refanyval = new(MSIL.OpCodes.Refanyval, MCIL.OpCodes.Refanyval);

        /// <summary>
        /// Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Rem = new(MSIL.OpCodes.Rem, MCIL.OpCodes.Rem);

        /// <summary>
        /// Divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Rem_Un = new(MSIL.OpCodes.Rem_Un, MCIL.OpCodes.Rem_Un);

        /// <summary>
        /// Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
        /// </summary>
        public static readonly OpCode Ret = new(MSIL.OpCodes.Ret, MCIL.OpCodes.Ret);

        /// <summary>
        /// Rethrows the current exception.
        /// </summary>
        public static readonly OpCode Rethrow = new(MSIL.OpCodes.Rethrow, MCIL.OpCodes.Rethrow);

        /// <summary>
        /// Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Shl = new(MSIL.OpCodes.Shl, MCIL.OpCodes.Shl);

        /// <summary>
        /// Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Shr = new(MSIL.OpCodes.Shr, MCIL.OpCodes.Shr);

        /// <summary>
        /// Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Shr_Un = new(MSIL.OpCodes.Shr_Un, MCIL.OpCodes.Shr_Un);

        /// <summary>
        /// Pushes the size, in bytes, of a supplied value type onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Sizeof = new(MSIL.OpCodes.Sizeof, MCIL.OpCodes.Sizeof);

        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        public static readonly OpCode Starg = new(MSIL.OpCodes.Starg, MCIL.OpCodes.Starg);

        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.
        /// </summary>
        public static readonly OpCode Starg_S = new(MSIL.OpCodes.Starg_S, MCIL.OpCodes.Starg_S);

        /// <summary>
        /// Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.
        /// </summary>
        public static readonly OpCode Stelem = new(MSIL.OpCodes.Stelem, MCIL.OpCodes.Stelem_Any);

        /// <summary>
        /// Replaces the array element at a given index with the native int value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_I = new(MSIL.OpCodes.Stelem_I, MCIL.OpCodes.Stelem_I);

        /// <summary>
        /// Replaces the array element at a given index with the int8 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_I1 = new(MSIL.OpCodes.Stelem_I1, MCIL.OpCodes.Stelem_I1);

        /// <summary>
        /// Replaces the array element at a given index with the int16 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_I2 = new(MSIL.OpCodes.Stelem_I2, MCIL.OpCodes.Stelem_I2);

        /// <summary>
        /// Replaces the array element at a given index with the int32 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_I4 = new(MSIL.OpCodes.Stelem_I4, MCIL.OpCodes.Stelem_I4);

        /// <summary>
        /// Replaces the array element at a given index with the int64 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_I8 = new(MSIL.OpCodes.Stelem_I8, MCIL.OpCodes.Stelem_I8);

        /// <summary>
        /// Replaces the array element at a given index with the float32 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_R4 = new(MSIL.OpCodes.Stelem_R4, MCIL.OpCodes.Stelem_R4);

        /// <summary>
        /// Replaces the array element at a given index with the float64 value on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_R8 = new(MSIL.OpCodes.Stelem_R8, MCIL.OpCodes.Stelem_R8);

        /// <summary>
        /// Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.
        /// </summary>
        public static readonly OpCode Stelem_Ref = new(MSIL.OpCodes.Stelem_Ref, MCIL.OpCodes.Stelem_Ref);

        /// <summary>
        /// Replaces the value stored in the field of an object reference or pointer with a new value.
        /// </summary>
        public static readonly OpCode Stfld = new(MSIL.OpCodes.Stfld, MCIL.OpCodes.Stfld);

        /// <summary>
        /// Stores a value of type native int at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_I = new(MSIL.OpCodes.Stind_I, MCIL.OpCodes.Stind_I);

        /// <summary>
        /// Stores a value of type int8 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_I1 = new(MSIL.OpCodes.Stind_I1, MCIL.OpCodes.Stind_I1);

        /// <summary>
        /// Stores a value of type int16 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_I2 = new(MSIL.OpCodes.Stind_I2, MCIL.OpCodes.Stind_I2);

        /// <summary>
        /// Stores a value of type int32 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_I4 = new(MSIL.OpCodes.Stind_I4, MCIL.OpCodes.Stind_I4);

        /// <summary>
        /// Stores a value of type int64 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_I8 = new(MSIL.OpCodes.Stind_I8, MCIL.OpCodes.Stind_I8);

        /// <summary>
        /// Stores a value of type float32 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_R4 = new(MSIL.OpCodes.Stind_R4, MCIL.OpCodes.Stind_R4);

        /// <summary>
        /// Stores a value of type float64 at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_R8 = new(MSIL.OpCodes.Stind_R8, MCIL.OpCodes.Stind_R8);

        /// <summary>
        /// Stores a object reference value at a supplied address.
        /// </summary>
        public static readonly OpCode Stind_Ref = new(MSIL.OpCodes.Stind_Ref, MCIL.OpCodes.Stind_Ref);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at a specified index.
        /// </summary>
        public static readonly OpCode Stloc = new(MSIL.OpCodes.Stloc, MCIL.OpCodes.Stloc);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 0.
        /// </summary>
        public static readonly OpCode Stloc_0 = new(MSIL.OpCodes.Stloc_0, MCIL.OpCodes.Stloc_0);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 1.
        /// </summary>
        public static readonly OpCode Stloc_1 = new(MSIL.OpCodes.Stloc_1, MCIL.OpCodes.Stloc_1);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 2.
        /// </summary>
        public static readonly OpCode Stloc_2 = new(MSIL.OpCodes.Stloc_2, MCIL.OpCodes.Stloc_2);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index 3.
        /// </summary>
        public static readonly OpCode Stloc_3 = new(MSIL.OpCodes.Stloc_3, MCIL.OpCodes.Stloc_3);

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at index (short form).
        /// </summary>
        public static readonly OpCode Stloc_S = new(MSIL.OpCodes.Stloc_S, MCIL.OpCodes.Stloc_S);

        /// <summary>
        /// Copies a value of a specified type from the evaluation stack into a supplied memory address.
        /// </summary>
        public static readonly OpCode Stobj = new(MSIL.OpCodes.Stobj, MCIL.OpCodes.Stobj);

        /// <summary>
        /// Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        public static readonly OpCode Stsfld = new(MSIL.OpCodes.Stsfld, MCIL.OpCodes.Stsfld);

        /// <summary>
        /// Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Sub = new(MSIL.OpCodes.Sub, MCIL.OpCodes.Sub);

        /// <summary>
        /// Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Sub_Ovf = new(MSIL.OpCodes.Sub_Ovf, MCIL.OpCodes.Sub_Ovf);

        /// <summary>
        /// Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Sub_Ovf_Un = new(MSIL.OpCodes.Sub_Ovf_Un, MCIL.OpCodes.Sub_Ovf_Un);

        /// <summary>
        /// Implements a jump table.
        /// </summary>
        public static readonly OpCode Switch = new(MSIL.OpCodes.Switch, MCIL.OpCodes.Switch);

        /// <summary>
        /// Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.
        /// </summary>
        public static readonly OpCode Tailcall = new(MSIL.OpCodes.Tailcall, MCIL.OpCodes.Tail);

        /// <summary>
        /// Throws the exception object currently on the evaluation stack.
        /// </summary>
        public static readonly OpCode Throw = new(MSIL.OpCodes.Throw, MCIL.OpCodes.Throw);

        /// <summary>
        /// Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.
        /// </summary>
        public static readonly OpCode Unaligned = new(MSIL.OpCodes.Unaligned, MCIL.OpCodes.Unaligned);
        
        /// <summary>
        /// Converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        public static readonly OpCode Unbox = new(MSIL.OpCodes.Unbox, MCIL.OpCodes.Unbox);
        
        /// <summary>
        /// Converts the boxed representation of a type specified in the instruction to its unboxed form.
        /// </summary>
        public static readonly OpCode Unbox_Any = new(MSIL.OpCodes.Unbox_Any, MCIL.OpCodes.Unbox_Any);
        
        /// <summary>
        /// Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.
        /// </summary>
        public static readonly OpCode Volatile = new(MSIL.OpCodes.Volatile, MCIL.OpCodes.Volatile);
        
        /// <summary>
        /// Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.
        /// </summary>
        public static readonly OpCode Xor = new(MSIL.OpCodes.Xor, MCIL.OpCodes.Xor);
    }
}
