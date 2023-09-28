using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public sealed class MenuSectionId : ValueObject
{
    public MenuSectionId(string sectionName)
    {
        Value = $"Section_{sectionName}";
    }

    public string Value { get; }

    public static MenuSectionId Create(string sectionName)
    {
        // TODO: enforce invariants
        return new MenuSectionId(sectionName);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}