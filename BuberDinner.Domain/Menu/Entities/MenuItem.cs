using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    public string Name { get; }
    public string Description { get; }

    private MenuItem(string name, string description)
        : base(MenuItemId.Create(name))
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string description)
    {
        // TODO: enforce invariants
        return new MenuItem(name, description);
    }
}