using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using Secyud.EntityFrameworkCore.Bulker.Tests;

namespace Secyud;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var a = new TestObject()
        {
            Value = 1
        };
        var id = FieldIdentifier.Create(() => a.Value);
        ValidationContext c = new ValidationContext(id.Model)
        {
            MemberName = id.FieldName
        };
        List<ValidationResult> results = [];
        try
        {
           var result = Validator.TryValidateObject(a, c, results, true);
        }
        catch (ValidationException ve)
        {
        }
    }
}