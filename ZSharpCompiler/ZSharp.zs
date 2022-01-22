using keyword func(OperatorType.Prefix);

keyword func func: (Pair<Expression, Expression> pair) {
	
}

// registers a new keyword
using keyword newobj(OperatorType.Prefix);

keyword func auto {
	new Typing.AutoType()
}

keyword func newobj: auto
keyword func newobj(function: Function) {
	new ILInstruction(OpCodes.newobj, function);
	(do() -> 2 -> 3);
	do(() -> 2 -> 3);
	a: (b -> c);
	(a: b) -> c;
	-> > :
}

keyword func IL {
	return new IL;
}

func IL.InitializerCall: (Expression[] expressions) -> IL {
	foreach (Expression expression in expressions) {
		if (expression is ILExpression il) {
			Instructions.AddRange(il.GetIL());
		} else throw;
	}
	return this;
}

keyword func new: (FunctionCall call) {
	call.Arguments.GetIL() + IL {
		newobj GetClass(call.Callable).GetConstructor(call.Arguments.Types)
	}
}

// registers a new binary operator
using operator::(20, OperatorType.Infix);

func operator::(string ns, string item) {
	return new 
}

using keyword class(30, OperatorType.Prefix);

// defining prefix keyword 'field'
keyword func field(Pair sig) -> FieldDefinition {
	return new FieldDefinition(sig.Left, new TypeName(sig.Right));
}

// defining prefix keyword 'class'
keyword func class(Pair sig) -> TypeDefinition {
	NamespaceItem name;
	TypeName meta;
	if (sig.Left is FunctionCall call) {
		name = call.Callable.Name;
		meta = new TypeName(call.Arguments.Items[0].Name); // verify there's exactly 1 argument of type INameProvider
	} else {
		name = sig.Left.Name; // should be an identifier literal
		meta = null;
	}
	return new TypeDefinition(name.Item, meta, name.Namespace);
}

class TypeDefinition {
	public TypeDefinition(string name, TypeName meta, string @namespace) {
		// constructor
	}
}

class FieldDefinition {
	public FieldDefinition(string name, TypeName type) {
		// constructor
	}
}

------------------Class------------------
-KWD- ---------ExpressionWithBody---------
-KWD- ---Pair<Call, List<TypeName>---			   {  }
-KWD- ---call---- : -List<TypeName>--			   {  }
-KWD- -ID-(-cls-) : TpNm-, TpNm-, ...			   {  }
class Name(class) : Base1, Base2, ... (where ...)* {  }


Precedence:
operator(), operator<>
operator,
operator:
operator{}
keyword class, keyword func
