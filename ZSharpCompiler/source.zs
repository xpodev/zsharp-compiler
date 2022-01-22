static func A: () -> B {
	infoof(ZSharp.Engine.IType)
	IL { ret }
}

static func C(x): i64 -> A {
	IL {
		ldarg x
		call GetType(i64)
	}
	infoof(System.Object)
	IL { ret }
}

static func B: () -> C()() {
	infoof(ZSharp.Unit)
	IL { ret }
}
