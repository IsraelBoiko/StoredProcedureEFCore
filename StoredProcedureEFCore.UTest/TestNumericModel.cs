namespace StoredProcedureEFCore.UTest
{
    internal class TestNumericModel<T> : TestTypedModel<T>
    {
        public T Dec { get; set; }
    }
}