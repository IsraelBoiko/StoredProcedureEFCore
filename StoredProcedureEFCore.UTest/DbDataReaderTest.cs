using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Moq;
using Moq.DataReader;
using NUnit.Framework;

namespace StoredProcedureEFCore.UTest
{
    public class DbDataReaderTest
    {
        private readonly List<TestModel> _testModelsCollection = new List<TestModel>()
        {
            new TestModel(1, '2', 3, 4, 5, 6, 7, 8, 9, 10.11f, 12.13, true, "15, 16", DateTime.Now, 18.19M, YN.Perhaps),
            new TestModel()
        };

        [Test]
        public void TestToList()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestModel> resultSet = mock.ToList<TestModel>();
            Assert.AreEqual(2, resultSet.Count);
            TestModelEqual(resultSet[0], 0);
            TestModelEqual(resultSet[1], 1);
        }
        
        [Test]
        public void TestToListCastToSByte()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<sbyte>> resultSet = mock.ToList<TestNumericModel<sbyte>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (sbyte) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (sbyte) collection.F);
                Assert.AreEqual(model.D, (sbyte) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (sbyte) collection.Dec);
                Assert.AreEqual(model.En, (sbyte) collection.En);
            });
        }

        [Test]
        public void TestToListCastToChar()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<char>> resultSet = mock.ToList<TestNumericModel<char>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (char) collection.F);
                Assert.AreEqual(model.D, (char) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (char) collection.Dec);
                Assert.AreEqual(model.En, (char) collection.En);
            });
        }

        [Test]
        public void TestToListCastToShort()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<short>> resultSet = mock.ToList<TestNumericModel<short>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (short) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (short) collection.F);
                Assert.AreEqual(model.D, (short) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (short) collection.Dec);
                Assert.AreEqual(model.En, (short) collection.En);
            });
        }

        [Test]
        public void TestToListCastToInt()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<int>> resultSet = mock.ToList<TestNumericModel<int>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (int) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (int) collection.F);
                Assert.AreEqual(model.D, (int) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (int) collection.Dec);
                Assert.AreEqual(model.En, (int) collection.En);
            });
        }

        [Test]
        public void TestToListCastToIntNullable()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<int?>> resultSet = mock.ToList<TestNumericModel<int?>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (int?) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (int?) collection.F);
                Assert.AreEqual(model.D, (int?) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (int?) collection.Dec);
                Assert.AreEqual(model.En, (int?) collection.En);
            });
        }

        [Test]
        public void TestToListCastToLong()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<long>> resultSet = mock.ToList<TestNumericModel<long>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (long) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (long) collection.F);
                Assert.AreEqual(model.D, (long) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (long) collection.Dec);
                Assert.AreEqual(model.En, (long) collection.En);
            });
        }

        [Test]
        public void TestToListCastToByte()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<byte>> resultSet = mock.ToList<TestNumericModel<byte>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (byte) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (byte) collection.F);
                Assert.AreEqual(model.D, (byte) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (byte) collection.Dec);
                Assert.AreEqual(model.En, (byte) collection.En);
            });
        }

        [Test]
        public void TestToListCastToUShort()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<ushort>> resultSet = mock.ToList<TestNumericModel<ushort>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (ushort) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (ushort) collection.F);
                Assert.AreEqual(model.D, (ushort) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (ushort) collection.Dec);
                Assert.AreEqual(model.En, (ushort) collection.En);
            });
        }

        [Test]
        public void TestToListCastToUInt()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<uint>> resultSet = mock.ToList<TestNumericModel<uint>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (uint) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (uint) collection.F);
                Assert.AreEqual(model.D, (uint) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (uint) collection.Dec);
                Assert.AreEqual(model.En, (uint) collection.En);
            });
        }

        [Test]
        public void TestToListCastToULong()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<ulong>> resultSet = mock.ToList<TestNumericModel<ulong>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (ulong) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, (ulong) collection.F);
                Assert.AreEqual(model.D, (ulong) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, (ulong) collection.Dec);
                Assert.AreEqual(model.En, (ulong) collection.En);
            });
        }

        [Test]
        public void TestToListCastToFloat()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<float>> resultSet = mock.ToList<TestNumericModel<float>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (float) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, collection.F);
                Assert.AreEqual(model.D, (float) collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, collection.Dec);
                Assert.AreEqual(model.En, (float) collection.En);
            });
        }

        [Test]
        public void TestToListCastToDouble()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestNumericModel<double>> resultSet = mock.ToList<TestNumericModel<double>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, collection.Sb);
                Assert.AreEqual(model.C, (double) collection.C);
                Assert.AreEqual(model.S, collection.S);
                Assert.AreEqual(model.I, collection.I);
                Assert.AreEqual(model.L, collection.L);
                Assert.AreEqual(model.B, collection.B);
                Assert.AreEqual(model.Us, collection.Us);
                Assert.AreEqual(model.Ui, collection.Ui);
                Assert.AreEqual(model.Ul, collection.Ul);
                Assert.AreEqual(model.F, collection.F);
                Assert.AreEqual(model.D, collection.D);
                Assert.AreEqual(model.Bo, collection.Bo ? 1 : 0);
                Assert.AreEqual(model.Dec, collection.Dec);
                Assert.AreEqual(model.En, (double) collection.En);
            });
        }

        [Test]
        public void TestToListCastToEnum()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestTypedModel<YN>> resultSet = mock.ToList<TestTypedModel<YN>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, (YN) collection.Sb);
                Assert.AreEqual(model.C, (YN) collection.C);
                Assert.AreEqual(model.S, (YN) collection.S);
                Assert.AreEqual(model.I, (YN) collection.I);
                Assert.AreEqual(model.L, (YN) collection.L);
                Assert.AreEqual(model.B, (YN) collection.B);
                Assert.AreEqual(model.Us, (YN) collection.Us);
                Assert.AreEqual(model.Ui, (YN) collection.Ui);
                Assert.AreEqual(model.Ul, (YN) collection.Ul);
                Assert.AreEqual(model.F, (YN) collection.F);
                Assert.AreEqual(model.D, (YN) collection.D);
                Assert.AreEqual(model.Bo, (YN) (collection.Bo ? 1 : 0));
                Assert.AreEqual(model.En, (YN) collection.En);
            });
        }

        [Test]
        public void TestToListConvertToNullableEnum()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<TestTypedModel<YN?>> resultSet = mock.ToList<TestTypedModel<YN?>>();
            Assert.AreEqual(2, resultSet.Count);
            AssertAll(resultSet, (model, collection) =>
            {
                Assert.AreEqual(model.Sb, (YN?) collection.Sb);
                Assert.AreEqual(model.C, (YN?) collection.C);
                Assert.AreEqual(model.S, (YN?) collection.S);
                Assert.AreEqual(model.I, (YN?) collection.I);
                Assert.AreEqual(model.L, (YN?) collection.L);
                Assert.AreEqual(model.B, (YN?) collection.B);
                Assert.AreEqual(model.Us, (YN?) collection.Us);
                Assert.AreEqual(model.Ui, (YN?) collection.Ui);
                Assert.AreEqual(model.Ul, (YN?) collection.Ul);
                Assert.AreEqual(model.F, (YN?) collection.F);
                Assert.AreEqual(model.D, (YN?) collection.D);
                Assert.AreEqual(model.En, (YN?) collection.En);
            });
        }

        [Test]
        public void TestToDictionary()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            Dictionary<sbyte, TestModel> resultSet = mock.ToDictionary<sbyte, TestModel>(m => m.Sb);
            Assert.AreEqual(2, resultSet.Count);
            TestModelEqual(resultSet[_testModelsCollection[0].Sb], 0);
            TestModelEqual(resultSet[_testModelsCollection[1].Sb], 1);
        }

        [Test]
        public void TestToLookup()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            Dictionary<sbyte, List<TestModel>> resultSet = mock.ToLookup<sbyte, TestModel>(m => m.Sb);
            Assert.AreEqual(2, resultSet.Count);
            TestModelEqual(resultSet[_testModelsCollection[0].Sb][0], 0);
            TestModelEqual(resultSet[_testModelsCollection[1].Sb][0], 1);
        }

        [Test]
        public void TestToSet()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            HashSet<sbyte> resultSet = mock.ToSet<sbyte>();
            Assert.AreEqual(2, resultSet.Count);
            resultSet.Contains(_testModelsCollection[0].Sb);
            resultSet.Contains(_testModelsCollection[1].Sb);
        }

        [Test]
        public void TestFirst()
        {
            DbDataReader mock = CreateDataReaderMock(0).Object;
            TestModel tm = mock.First<TestModel>();
            TestModelEqual(tm, 0);
        }

        [Test]
        public void TestFirstOnEmpty()
        {
            DbDataReader mock = CreateDataReaderMock().Object;
            Assert.Throws<InvalidOperationException>(() => mock.First<TestModel>());
        }

        [Test]
        public void TestFirstOrDefault()
        {
            DbDataReader mock = CreateDataReaderMock(0).Object;
            TestModel tm = mock.FirstOrDefault<TestModel>();
            TestModelEqual(tm, 0);
        }

        [Test]
        public void TestFirstOrDefaultOnEmpty()
        {
            DbDataReader mock = CreateDataReaderMock().Object;
            Assert.Null(mock.FirstOrDefault<TestModel>());
        }

        [Test]
        public void TestSingle()
        {
            DbDataReader mock = CreateDataReaderMock(0).Object;
            TestModel tm = mock.Single<TestModel>();
            TestModelEqual(tm, 0);
        }

        [Test]
        public void TestSingleOnEmpty()
        {
            DbDataReader mock = CreateDataReaderMock().Object;
            Assert.Throws<InvalidOperationException>(() => mock.Single<TestModel>());
        }

        [Test]
        public void TestSingleOnNotSingle()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            Assert.Throws<InvalidOperationException>(() => mock.Single<TestModel>());
        }

        [Test]
        public void TestSingleOrDefault()
        {
            DbDataReader mock = CreateDataReaderMock(0).Object;
            TestModel tm = mock.SingleOrDefault<TestModel>();
            TestModelEqual(tm, 0);
        }

        [Test]
        public void TestSingleOrDefaultOnEmpty()
        {
            DbDataReader mock = CreateDataReaderMock().Object;
            Assert.Null(mock.SingleOrDefault<TestModel>());
        }

        [Test]
        public void TestSingleOrDefaultOnNotSingle()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            Assert.Throws<InvalidOperationException>(() => mock.SingleOrDefault<TestModel>());
        }

        [Test]
        public void TestColumn()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<sbyte> col = mock.Column<sbyte>();
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual(col[0], _testModelsCollection[0].Sb);
            Assert.AreEqual(col[1], _testModelsCollection[1].Sb);
        }

        [Test]
        public void TestColumn2()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<ulong> col = mock.Column<ulong>(nameof(TestModel.Ul));
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual(col[0], _testModelsCollection[0].Ul);
            Assert.AreEqual(col[1], _testModelsCollection[1].Ul);
        }

        [Test]
        public void TestColumn3()
        {
            DbDataReader mock = CreateDataReaderMock(0, 1).Object;
            List<int> col = mock.Column<int>(3);
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual(col[0], _testModelsCollection[0].I);
            Assert.AreEqual(col[1], _testModelsCollection[1].I);
        }

        private void AssertAll<TModel>(IList<TModel> resultSet, Action<TModel, TestModel> assertionsAction)
        {
            for (var i = 0; i < resultSet.Count; i++)
                assertionsAction(resultSet[i], _testModelsCollection[i]);
        }

        private void TestModelEqual(TestModel tm1, int i)
        {
            TestModel tm2 = _testModelsCollection[i];
            Assert.AreEqual(tm1.Sb, tm2.Sb);
            Assert.AreEqual(tm1.C, tm2.C);
            Assert.AreEqual(tm1.S, tm2.S);
            Assert.AreEqual(tm1.I, tm2.I);
            Assert.AreEqual(tm1.L, tm2.L);
            Assert.AreEqual(tm1.B, tm2.B);
            Assert.AreEqual(tm1.Us, tm2.Us);
            Assert.AreEqual(tm1.Ui, tm2.Ui);
            Assert.AreEqual(tm1.Ul, tm2.Ul);
            Assert.AreEqual(tm1.F, tm2.F);
            Assert.AreEqual(tm1.D, tm2.D);
            Assert.AreEqual(tm1.Bo, tm2.Bo);
            Assert.AreEqual(tm1.Str, tm2.Str);
            Assert.AreEqual(tm1.Date, tm2.Date);
            Assert.AreEqual(tm1.Dec, tm2.Dec);
            Assert.AreEqual(tm1.En, tm2.En);
        }

        private Mock<DbDataReader> CreateDataReaderMock(params int[] indexes)
        {
            List<TestModel> data = indexes.Select(i => (TestModel) _testModelsCollection[i].Clone()).ToList();
            var mock = new Mock<DbDataReader>();
            mock.SetupDataReader(data);
            return mock;
        }
    }
}
