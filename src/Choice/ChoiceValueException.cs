using System;

namespace Moonad
{
    public sealed class ChoiceValueException()
        : NullReferenceException("Choice value unavailable. Please check another choice.");
}