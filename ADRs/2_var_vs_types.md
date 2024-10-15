# ADR_2 - 10/11/2024 - var vs strong types

_Status_: Accepted

_Context_: Should we use the keyword `var`? There is an argument to be made about readability in both cases. On the one hand `var` means less words and is more compact. Especially when the type is obvious in the declaration. (ie. `var compteur = 1`). On the other hand, var is not explicit. And there is also an argument to be made that explicit should be prioritised. In any case, it doesn't change much. As the team (me), does not have a strong preference, habits should be prioritised. As such, we will use explicit types.

_Decision_: No. We will use explicit types.

_Consequences_: Automatic MS generated code will stay as is. All human code will use strongly explicitely typed variable (except when not applicable)