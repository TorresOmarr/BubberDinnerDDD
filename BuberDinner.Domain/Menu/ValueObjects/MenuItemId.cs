using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public sealed class MenuItemId : ValueObject
{
    public string Value { get; }

    private MenuItemId(string itemName)
    {
        Value = $"Item_{itemName}";
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static MenuItemId Create(string itemName)
    {
        return new MenuItemId(itemName);
    }
}