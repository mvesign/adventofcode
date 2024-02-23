using System;

namespace AdventOfCode.Year2015.Days.Day15;

public class Ingredient(int capacity, int durability, int flavor, int texture, int calories)
{
    public int Capacity { get; set; } = capacity;
    public int Durability { get; set; } = durability;
    public int Flavor { get; set; } = flavor;
    public int Texture { get; set; } = texture;
    public int Calories { get; set; } = calories;
    public int Score => Math.Max(0, Capacity) * Math.Max(0, Durability) * Math.Max(0, Flavor) * Math.Max(0, Texture);

    public static Ingredient operator *(Ingredient ingredient, int number) =>
        new(
            ingredient.Capacity * number,
            ingredient.Durability * number,
            ingredient.Flavor * number,
            ingredient.Texture * number,
            ingredient.Calories * number
        );

    public static Ingredient operator +(Ingredient x, Ingredient y) =>
        new(
            x.Capacity + y.Capacity,
            x.Durability + y.Durability,
            x.Flavor + y.Flavor,
            x.Texture + y.Texture,
            x.Calories + y.Calories
        );
}