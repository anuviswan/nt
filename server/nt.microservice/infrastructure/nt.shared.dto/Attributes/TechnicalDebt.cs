﻿namespace nt.shared.dto.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
public sealed class TechnicalDebt:Attribute
{
    public DebtType DebtType { get; }
    public string Description { get; }
    public TechnicalDebt(DebtType debtType, string description) 
    {
        (DebtType, Description) = (debtType, description);
    }
}

public enum DebtType
{
    BadDesign,
    ComplexDesign,
    MissingFeature
}
