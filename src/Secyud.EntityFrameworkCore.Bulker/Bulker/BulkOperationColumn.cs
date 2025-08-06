using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Secyud.Database;

namespace Secyud.EntityFrameworkCore.Bulker;

public class BulkOperationColumn(IProperty property, string columnName) : IColumnInfo
{
    public IProperty Property { get; set; } = property;

    public string PropertyName { get; set; } = property.Name;

    public string ColumnType { get; set; } = property.GetColumnType();

    public string ColumnName { get; set; } = columnName;

    public bool IsPrimaryKey { get; set; } = property.IsPrimaryKey();

    public bool IsShadowProperty { get; set; } = property.IsShadowProperty();

    private Func<object, object?>? _getDelegate;

    public object? Get(object entity)
    {
        if (_getDelegate is null)
        {
            var instance = Expression.Parameter(typeof(object), "instance");

            if (Property.PropertyInfo is not { } property) return null;

            if (property.DeclaringType is not { } type)
            {
                throw new ArgumentException("Unable to determine DeclaringType from Property");
            }

            var instanceCast = !type.IsValueType
                ? Expression.TypeAs(instance, type)
                : Expression.Convert(instance, type);

            var getter = property.GetGetMethod(true) ??
                         type.GetProperty(property.Name)?.GetGetMethod(true);

            if (getter != null)
                _getDelegate = Expression.Lambda<Func<object, object>>(
                    Expression.TypeAs(Expression.Call(instanceCast, getter), typeof(object)), instance).Compile();
            else
                _getDelegate = o => null;
        }

        return _getDelegate.Invoke(entity);
    }
}