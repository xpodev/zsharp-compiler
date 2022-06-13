# Operators
In Z#, operators are functions or actions that can be performed on 0 or more values and optionally produce another value as a result.


## Operator Names
Operators are stored internally as functions with the following format:

**Prefix:**
```
{OP}_
```

**Postfix:**
```
_{OP}
```

**Infix:**
```
_{OP}_
```

Where you replace `{OP}` with the operator symbol.
For example, this is the name for the binary plus (`+`) operator:
```
_+_
```

Due to the use of it as part of their name, the underscore (`_`) symbol cannot be used as an operator symbol.

## Operator Functions
To define an operator function, use string literal naming:
```
// define the prefix ':>' operator for strings
fun :>_(msg: string) {
    print(msg);
}
```

And to use it:
```
:> "Hello"; // prints "Hello"
```

You can get a reference to an operator function with the operator function notation:
```
let opFunc = ({OP});
```

For example, let's get a reference to the print operator we defined above and use it on a list of values using the `map` method.
```
let list = {1, 2, 3};
let opFunc = (:>);
list.map(opFunc);
```

Or in a single line:
```
{1, 2, 3}.map((:>)); // note the extra parenthesese
```