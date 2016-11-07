// ///////////////////////////////////
// File: ObjectBase_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Helper
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Helper;



    [TestClass]
    public class ObjectBase_Test
    {
        #region  Static Fields and Constants

        private const string InitFirstNameValue = "Max";
        private const string InitLastNameValue = "Mustermann";

        #endregion



        #region Fields

        private ObjectTestClass testClass;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.testClass = new ObjectTestClass(InitFirstNameValue, InitLastNameValue);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void ChangesPropertyValues()
        {
            // Arrange
            const string ExpectedFirstName = "Andre";

            // Act
            this.testClass.FirstName = ExpectedFirstName;

            // Assert
            Assert.AreEqual(ExpectedFirstName, this.testClass.FirstName);
        }

        [TestMethod]
        public void NotInEditModeAfterInit()
        {
            // Assert
            Assert.IsFalse(this.testClass.InEditMode);
        }

        [TestMethod]
        public void InEditModeAfterPropertyChange()
        {
            // Act
            this.testClass.FirstName = "Andre";

            // Assert
            Assert.IsTrue(this.testClass.InEditMode);
        }

        [TestMethod]
        public void RestoreValuesAfterPropertyChanges()
        {
            // Arrange
            this.testClass.FirstName = "Andre";
            this.testClass.LastName = "Multerer";

            // Act
            this.testClass.CancelEdit();

            // Assert
            Assert.AreEqual(InitFirstNameValue, this.testClass.FirstName);
            Assert.AreEqual(InitLastNameValue, this.testClass.LastName);
        }

        [TestMethod]
        public void NotInEditModeAfterRestoringPropertyValues()
        {
            // Arrange
            this.testClass.FirstName = "Testname";

            // Act
            this.testClass.CancelEdit();

            // Assert
            Assert.IsFalse(this.testClass.InEditMode);
        }

        [TestMethod]
        public void NotInEditModeWhenSettingPropertiesToInitStateAgain()
        {
            // Act
            this.testClass.FirstName = "Andre";
            this.testClass.FirstName = InitFirstNameValue;

            // Assert
            Assert.IsFalse(this.testClass.InEditMode);
        }

        [TestMethod]
        public void NotInEditModeWhenValuesWereSaved()
        {
            // Arrange
            this.testClass.FirstName = "Andre";

            // Act
            this.testClass.EndEdit();

            // Assert
            Assert.IsFalse(this.testClass.InEditMode);
        }

        #endregion



        #region Nested type: ObjectTestClass

        public class ObjectTestClass : ObjectBase
        {
            #region Fields

            private string _firstName;
            private string _lastName;

            #endregion



            #region Constructors

            public ObjectTestClass(string paramFirstName, string paramLastName)
            {
                this._firstName = paramFirstName;
                this._lastName = paramLastName;
            }

            #endregion



            #region Properties, Indexers

            public string FirstName
            {
                get { return this._firstName; }
                set { this.SetProperty(newValue => this._firstName = newValue, value); }
            }

            public string LastName
            {
                get { return this._lastName; }
                set { this.SetProperty(newValue => this._lastName = newValue, value); }
            }

            #endregion
        }

        #endregion
    }
}