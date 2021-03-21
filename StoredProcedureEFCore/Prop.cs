using System;

namespace StoredProcedureEFCore
{
    struct Prop<T>
    {
        public int ColumnOrdinal { get; set; }
        public Action<T, object> Setter { get; set; }
    }
}
