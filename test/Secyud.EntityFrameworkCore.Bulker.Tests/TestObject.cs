using System.ComponentModel.DataAnnotations;

namespace Secyud.EntityFrameworkCore.Bulker.Tests;

public class TestObject
{
    [Range(1, 2)]
    [Display(Name = "b")]
    public int Value { get; set; }
}