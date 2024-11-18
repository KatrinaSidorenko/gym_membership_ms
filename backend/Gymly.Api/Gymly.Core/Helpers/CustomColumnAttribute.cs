namespace Gymly.Core.Helpers;

public class CustomColumnAttribute : Attribute
{
    public string ColumnName { get; set; }
    public CustomColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}
