# Proof System
This system is designed so that users can express some conditions that must hold for an input value of a function.
The checks are done at compile time. The system uses facts and proofs in order to validate data.

For example, let's define a factorial function which is only valid for integers >= 0:
```
fun factorial(n: i32) where n >= 0 {
    if (n <= 1) n
    else n * factorial(n - 1)
}
```

There are 2 things in play here. 
1. Other functions that call this function must prove that the input argument for `n` is `>= 0`.
2. The compiler must prove that the recursive `factorial` function call is valid for all valid input values of `n`.


Let's first show the first point.
```
let x = input<i32>();
print(factorial(x)); // compile-time error! cannot prove that n >= 0
```

```
let x = input<i32>();
if (x >= 0) print(factorial(x)); // ok. can prove that x >= 0.
else print("Input number must be above or equal to 0");
```

In the first snippet, since the user can enter any number of their choice, the compiler can't prove that `x` will contain a value that matches the requirement for the `factorial` function.
In the second snippet however, since it is only possible for the function to be called when `x >= 0`, the compiler now has its proof so it compiles the program as it should.


For the second point, the compiler first starts with this set of facts for `n`:
```
{ "n >= 0" }
```
In the `else` block, another fact is added (since the `if` condition proved that `n <= 1`, `else` must prove otherwise.):
```
{ "n >= 0", "!(n <= 1)" }
```
When introducing new facts to the fact list, the compiler will try to merge the facts together. In this case, it'll try and merge both facts to get:
```
{ "n > 1" }
```
It becomes this fact because:
```
!(n <= 1) -> n > 1
n > 1 && n >= 0 -> n > 1
```

When it compiles the recursive call, it tries to prove that `n - 1` also meets the requirements.

So it applies the `n - 1` expression to the other proofs to get:
```
{ "n > 0" }
```
Since `n > 0` also intersects `n >= 0` for all values, the compiler will allow the recursive call and compile it.


Another example:
```
fun static_assert(c: bool) where c {}
```

Arguments for the `c` parameter must be proven by the compiler to be truthy.

These line will not compile:
```
static_assert(random() > 0.5);
```
But this will:
```
static_assert(1 < 2);
```
And also this (as long as `some_value` can't change between the condition check and the body)
```
if (some_value) static_assert(some_value);
```

Note that for blocks such as `if`, the condition must be `pure` because an impure condition may not always be the same and so the proof system cannot prove anything about it. Impure values (i.e. values that originate from impure sources) always start without facts about their value.
```
let x = 2; // { "x == 2", "x is i32" }. also, this can't change because it is declared as 'let'.
```
```
let y = random(); // { "y is random.return_type" }. no other facts as we can't know anything about the value 'y' at compile-time except for its type.
```
Other problems can arise when code is running concurrently and can change state of objects. This must also be checked by the proof system.


## Overloading over the proof system
Using the proof system, it is possible to find all the methods in an overload that match the set of facts for the arguments as long as at the end there's exactly 1 overload match.

## Implementation Proposal
This system is also used for proving constrains on types since types are also expressions. This way there's no need to implement 2 very similar systems twice. This also makes RT-CT-Duality much better.

## Implementation Proposal
When a value `v` has a set of facts `S` and an expression `E(v)` needs to be proved, apply the expression on each fact `F` in `S` to get a new set of facts `S'` for the expression `E(v)`:
```
S' = {E(F) for F in S}
```

## Notes
* The proof system looks like it uses some of the same method as some optimizations (or the other way around)
* The proof system can be used for many things related to compile-time checking such as:
    - Type safety
    - `null` safety (nullable in C#)
    - Object state validation (e.g. accessing an array at a valid index)
    - Optimizations (e.g. by proving a condition is `false` we can remove a conditional block)
    - Correct argument passing
    - Compile-time assertions (by requiring the argument to be `true`)
    - Resolving ambiguities in overloading

