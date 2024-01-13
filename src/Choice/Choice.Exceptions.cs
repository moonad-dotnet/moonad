using System;

namespace Moonad
{
    public sealed class ChoiceValueException : NullReferenceException
    { 
        public ChoiceValueException() : base("Choice value unavailable. Please check another choice.") { }
    }
}
