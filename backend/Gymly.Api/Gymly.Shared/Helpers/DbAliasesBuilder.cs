using Gymly.Core.Helpers;
using System.Linq.Expressions;
using System.Reflection;

namespace Gymly.Shared.Helpers
{
    public class DbAliasesBuilder<T> where T : class
    {
        private readonly List<string> _aliases = new();

        /// <summary>
        /// Adds all aliases for properties decorated with CustomColumnAttribute.
        /// </summary>
        public DbAliasesBuilder<T> AddAliases()
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var customAttribute = property.GetCustomAttribute<CustomColumnAttribute>();
                if (customAttribute != null)
                {
                    _aliases.Add($"{customAttribute.ColumnName} AS {property.Name}");
                }
            }
            return this;
        }

        /// <summary>
        /// Adds an alias for a specific property.
        /// </summary>
        public DbAliasesBuilder<T> AddAlias(Expression<Func<T, object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var property = GetPropertyFromExpression(expression);
            if (property == null)
                throw new ArgumentException("Expression must target a property.", nameof(expression));

            var customAttribute = property.GetCustomAttribute<CustomColumnAttribute>();
            if (customAttribute != null)
            {
                _aliases.Add($"{customAttribute.ColumnName} AS {property.Name}");
            }
            else
            {
                _aliases.Add($"{property.Name} AS {property.Name}");
            }
            return this;
        }

        /// <summary>
        /// Adds a custom alias for a specific property.
        /// </summary>
        public DbAliasesBuilder<T> AddAlias(Expression<Func<T, object>> expression, string alias)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Alias cannot be null or whitespace.", nameof(alias));

            var property = GetPropertyFromExpression(expression);
            if (property == null)
                throw new ArgumentException("Expression must target a property.", nameof(expression));

            _aliases.Add($"{alias} AS {property.Name}");
            return this;
        }

        public DbAliasesBuilder<T> AddAlias(string alias, string columnName)
        {
            _aliases.Add($"{columnName} AS {alias}");
            return this;
        }

        /// <summary>
        /// Builds the aliases as a comma-separated string.
        /// </summary>
        public string BuildAliases(string tableName = null)
        {
            if (_aliases.Count == 0)
                return "*";

            // add table name to aliases
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                return string.Join(", ", _aliases.Select(alias => $"{tableName}.{alias}"));
            }

            _aliases.Reverse();

            return string.Join(", ", _aliases);
        }

        /// <summary>
        /// Extracts the property information from an expression.
        /// </summary>
        private static PropertyInfo GetPropertyFromExpression(Expression<Func<T, object>> expression)
        {
            // Handle UnaryExpression (e.g., converting value types to object)
            if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMember)
            {
                return unaryMember.Member as PropertyInfo;
            }

            // Handle direct MemberExpression
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member as PropertyInfo;
            }

            return null;
        }
    }
}
