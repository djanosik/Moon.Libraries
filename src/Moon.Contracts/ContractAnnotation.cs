﻿using System;

namespace JetBrains.Annotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    sealed class ContractAnnotationAttribute : Attribute
    {
        public ContractAnnotationAttribute(string contract)
            : this(contract, false)
        {
        }

        public ContractAnnotationAttribute(string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        public string Contract { get; private set; }

        public bool ForceFullStates { get; private set; }
    }
}