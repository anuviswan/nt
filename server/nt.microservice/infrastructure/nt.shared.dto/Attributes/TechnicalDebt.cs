namespace nt.shared.dto.Attributes;

public class TechnicalDebt:Attribute
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
    CodeDesign
}
