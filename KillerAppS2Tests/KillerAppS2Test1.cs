using System;
using System.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KillerAppS2Tests
{
    [TestClass]
    public class KillerAppS2Test1
    {
        private Player _player;
        [TestInitialize]
        public void TestInitialize()
        {
            _player = new Player();
        }

        /* Style Syntax For TestMethods
        [TestMethod()]
        public void MethodName_Scenario_Expectedbehavior() or just Expectedbehaviour
        {
            // Arrange
            // Act
            // Assert
        }*/

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
