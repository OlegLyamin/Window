using System;
using System.Reflection;
using NUnit.Framework;
using GUI;

namespace UnitTests
{
    [TestFixture]
    class WindowParametrsTest
    {
       
        [TestCase(700, 700, 30, 3, 3, TestName =
            "Тестирование положительных входных параметров: 700, 700, 30, 3, 3")]
        [TestCase(678, 675, 34, 1, 1, TestName =
            " Тестирование положительных входных параметров: 678, 675, 34, 1,1")]
        [TestCase(666, 657, 25, 2, 2, TestName =
            " Тестирование положительных входных параметров: 666, 657, 25, 2,2")]
        [TestCase(982, 758, 30, 3, 3, TestName =
            " Тестирование положительных входных параметров: 982, 758, 30, 3,3")]
        [TestCase(856, 758, 35, 4, 4, TestName =
            "Тестирование положительных входных параметров: 856, 758, 35, 4,4 ")]
        [Test]
        public void TestWindowParametrsPositive(int lengthHeight, int lengthWidth,
            int lengthWeight, int sectionNumber, int opensection)
        {
            var windowParametrs = new WindowParametrs(lengthHeight, lengthWidth,
                lengthWeight, sectionNumber, opensection);
        }

        

        [TestCase(-700, -700, -30, -3, -3, TestName = " Проверка на неправильный " +
                                                      "ввод входных параметров")]
        [TestCase(700, -700, -30, -3, -3, TestName = " Проверка на неправильный " +
                                                     "ввод входных параметров")]
        [TestCase(700, 700, -30, -3, -3, TestName = " Проверка на неправильный " +
                                                    "ввод входных параметров")]
        [TestCase(700, -700, -30, -3, -3, TestName = " Проверка на неправильный " +
                                                     "ввод входных параметров")]
        [TestCase(-700, 700, 30, -3, -3, TestName = " Проверка на неправильный " +
                                                    "ввод входных параметров")]
        [TestCase(700, -700, -30, 3, -3, TestName = " Проверка на неправильный " +
                                                    "ввод входных параметров")]
        [Test]
        public void TestWindowParametrsNegative(int lengthHeight, int lengthWidth,
            int lengthWeight, int sectionNumber, int opensection)
        {
            Assert.That(() =>
                {
                    var windowParametrs = new WindowParametrs(lengthHeight, lengthWidth,
                        lengthWeight, sectionNumber, opensection);
                },
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }

        [TestCase(700, "LengthHeight", TestName = " TestWindowParametrsPositive " +
                                                  "| LengthHeight")]
        [TestCase(700, "LengthWidth", TestName = " TestWindowParametrsPositive " +
                                                 "| LengthWidth")]
        [TestCase(30, "LengthWeight", TestName = " TestWindowParametrsPositive " +
                                                 "| LengthWeight")]
        [TestCase(3, "SectionNumber", TestName = " TestWindowParametrsPositive " +
                                                 "| SectionNumber")]
        [TestCase(3, "OpenSection", TestName = " TestWindowParametrsPositive " +
                                               "| OpenSection")]
        [Test]
        public void TestWindowParametrsPositive(double propValue,
            string propName)
        {
            var windowparametrs = new WindowParametrs(700, 700, 30, 3, 3);
            Type t = windowparametrs.GetType();
            PropertyInfo p = t.GetProperty(propName);
            Assert.AreEqual(propValue, p.GetValue(windowparametrs));
        }
    }
}
