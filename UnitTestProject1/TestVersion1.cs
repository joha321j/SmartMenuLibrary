using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartMenuLibrary;

namespace SmartMenuTest
{
    [TestClass]
    public class TestVersion1
    {
        SmartMenu smartmenu;
        [TestInitialize]
        public void CreateNewSmartMenu()
        {
            smartmenu = new SmartMenu();
        }
        //[TestMethod]
        //public void TestOfReadFile()
        //{
        //    Assert.AreEqual(true, smartmenu.LoadMenu("C:\\Users\\Johannes\\Documents\\Visual Studio 2017\\Projects\\SmartMenuApp\\SmartMenuLibrary\\bin\\Debug\\MenuSpec.txt"));
        //}

        //Possible tests to add: Does it throw an exception when you try to read a wrong filetype?
        //                       Rewrite menuName, menuDescription, menuList and menuId to properties and test,
        //                       that they contain the correct values.
    }
}
