using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace StoredProcedureEFCore
{
    /// <summary>
    /// Mapper <see cref="DbDataReader"/> to model of type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    internal class Mapper<T> where T : class, new()
    {
        /// <summary>
        /// Contains different columns set information mapped to type <typeparamref name="T"/>.
        /// </summary>
        private static readonly ConcurrentDictionary<int, Prop<T>[]> PropertiesCache = new ConcurrentDictionary<int, Prop<T>[]>();

        private readonly DbDataReader _reader;
        private readonly Prop<T>[] _properties;

        public Mapper(DbDataReader reader)
        {
            _reader = reader;
            _properties = MapColumnsToProperties();
        }

        /// <summary>
        /// Map <see cref="DbDataReader"/> to a T and apply an action on it for each row
        /// </summary>
        /// <param name="action">Action to apply to each row</param>
        public void Map(Action<T> action)
        {
            while (_reader.Read())
            {
                T row = MapNextRow();
                action(row);
            }
        }

        /// <summary>
        /// Map <see cref="DbDataReader"/> to a T and apply an action on it for each row
        /// </summary>
        /// <param name="action">Action to apply to each row</param>
        public Task MapAsync(Action<T> action)
        {
            return MapAsync(action, CancellationToken.None);
        }

        /// <summary>
        /// Map <see cref="DbDataReader"/> to a T and apply an action on it for each row
        /// </summary>
        /// <param name="action">Action to apply to each row</param>
        /// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled</param>
        public async Task MapAsync(Action<T> action, CancellationToken cancellationToken)
        {
            while (await _reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                T row = await MapNextRowAsync(cancellationToken).ConfigureAwait(false);
                action(row);
            }
        }

        public T MapNextRow()
        {
            T row = new T();
            for (int i = 0; i < _properties.Length; ++i)
            {
                object value = _reader.IsDBNull(_properties[i].ColumnOrdinal) ? null : _reader.GetValue(_properties[i].ColumnOrdinal);
                _properties[i].Setter(row, value);
            }
            return row;
        }

        public Task<T> MapNextRowAsync()
        {
            return MapNextRowAsync(CancellationToken.None);
        }

        public async Task<T> MapNextRowAsync(CancellationToken cancellationToken)
        {
            T row = new T();
            for (int i = 0; i < _properties.Length; ++i)
            {
                object value = await _reader.IsDBNullAsync(_properties[i].ColumnOrdinal, cancellationToken).ConfigureAwait(false)
                    ? null
                    : _reader.GetValue(_properties[i].ColumnOrdinal);
                _properties[i].Setter(row, value);
            }
            return row;
        }

        internal static int ComputePropertyKey(IEnumerable<string> columns)
        {
            unchecked
            {
                int hashCode = 17;
                foreach (string column in columns)
                {
                    hashCode = (hashCode * 31) + column.GetHashCode();
                }
                return hashCode;
            }
        }

        private Prop<T>[] MapColumnsToProperties()
        {
            Type modelType = typeof(T);

            string[] columns = new string[_reader.FieldCount];
            for (int i = 0; i < _reader.FieldCount; ++i)
                columns[i] = _reader.GetName(i);

            int propKey = ComputePropertyKey(columns);
            if (PropertiesCache.TryGetValue(propKey, out Prop<T>[] s))
            {
                return s;
            }

            ParameterExpression instance = Expression.Parameter(typeof(T), "instance");
            ParameterExpression value = Expression.Parameter(typeof(object), "value");

            var properties = new List<Prop<T>>(columns.Length);
            for (int i = 0; i < columns.Length; i++)
            {
                string name = columns[i].Replace("_", "");
                PropertyInfo prop = modelType.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null)
                    continue;

                // "x as T" is faster than "(T) x" if x is a reference type
                UnaryExpression valueCast = prop.PropertyType.IsValueType
                    ? Expression.Convert(value, prop.PropertyType)
                    : Expression.TypeAs(value, prop.PropertyType);

                MethodCallExpression setterCall = Expression.Call(instance, prop.GetSetMethod(), valueCast);
                var setter = (Action<T, object>) Expression.Lambda(setterCall, instance, value).Compile();

                properties.Add(new Prop<T>
                {
                    ColumnOrdinal = i,
                    Setter = setter,
                });
            }
            Prop<T>[] propertiesArray = properties.ToArray();
            PropertiesCache[propKey] = propertiesArray;
            return propertiesArray;
        }
    }
}
