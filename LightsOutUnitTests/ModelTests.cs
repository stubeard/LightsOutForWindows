using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOut;

namespace LightsOutUnitTests
{
    [TestClass]
    public class ModelTests
    {
        private IModel CreateEmptyModel()
        {
            IModel model = new Model();

            model.Reset();

            return model;
        }


        private bool[,] GetActual( IModel model )
        {
            bool[,] result = new bool[ Model.MaxRows, Model.MaxColumns ];

            for (int i = 0; i < Model.MaxColumns; ++i)
            {
                for (int j = 0; j < Model.MaxRows; ++j)
                {
                    result[j, i] = model.Get(i, j); //local array is accessed row, column
                }
            }
            return result;
        }

        [TestMethod]
        public void TestMinXYBoundary()
        {
            IModel model = CreateEmptyModel();

            bool[,] expected = new bool[,]{ { true,  true,  false, false, false },
                                            { true,  false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(0, 0);

            bool[,] actual = GetActual(model);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMaxXYBoundary()
        {
            IModel model = CreateEmptyModel();

            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, true  },
                                            { false, false, false, true,  true  } };

            model.Set(4, 4);

            bool[,] actual = GetActual(model);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMinXMaxYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { true,  false, false, false, false },
                                            { true,  true,  false, false, false } };

            model.Set(0, 4);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMaxXMinYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, true,  true  },
                                            { false, false, false, false, true  },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(4, 0);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOutOfRangeMinXYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(-1, -1);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOutOfRangeMaxXYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(5, 5);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOutOfRangeMinXMaxYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(-1, 5);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOutOfRangeMaxXMinYBoundary()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false },
                                            { false, false, false, false, false } };

            model.Set(5, -1);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGoodValue1()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, false, true,  false, false },
                                            { false, true,  true,  true,  false },
                                            { false, false, true,  false, false },
                                            { false, false, false, false, false } };

            model.Set(2, 2);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGoodValue2()
        {
            IModel model = CreateEmptyModel();
    
            bool[,] expected = new bool[,]{ { false, false, false, false, false },
                                            { false, true,  true,  false, false },
                                            { true,  false, false, true,  false },
                                            { false, true,  true,  false, false },
                                            { false, false, false, false, false } };

            model.Set(2, 2);
            model.Set(1, 2);
    
            bool[,] actual = GetActual(model);
    
            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
