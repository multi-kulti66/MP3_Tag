// ///////////////////////////////////
// File: BindableValidator_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Validation
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Validation;



    [TestClass]
    public class BindableValidator_Test : BindableValidator
    {
        #region Fields

        private ValidatorTestClass validatorTestClass;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.validatorTestClass = new ValidatorTestClass();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void SetsAndGetsNewValidationPropertyValue()
        {
            // Arrange
            const string ExpectedValue = "Multerer";

            // Act
            this.validatorTestClass.ValidationProperty = ExpectedValue;

            // Assert
            Assert.AreEqual(ExpectedValue, this.validatorTestClass.ValidationProperty);
        }

        [TestMethod]
        public void SetsAndGetsNewPropertyValue()
        {
            // Arrange
            const string ExpectedValue = "Multerer";

            // Act
            this.validatorTestClass.NoValidationProperty = ExpectedValue;

            // Assert
            Assert.AreEqual(ExpectedValue, this.validatorTestClass.NoValidationProperty);
        }

        [TestMethod]
        public void RaisePropertyChangedWhenValueChanges()
        {
            // Arrange
            string raisedPropertyName = string.Empty;
            this.validatorTestClass.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                raisedPropertyName = e.PropertyName;
            };

            // Act
            this.validatorTestClass.ValidationProperty = "test";

            // Assert
            Assert.AreEqual(nameof(this.validatorTestClass.ValidationProperty), raisedPropertyName);
        }

        [TestMethod]
        public void GetNoErrorWhenPropertyValueValid()
        {
            // Arrange

            // Act
            this.validatorTestClass.ValidationProperty = "testValue";
            string errorReturnValue = (this.validatorTestClass as IDataErrorInfo)[nameof(this.validatorTestClass.ValidationProperty)];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(errorReturnValue));
        }

        [TestMethod]
        public void GetRequiredErrorWhenPropertyHasNoValue()
        {
            // Arrange

            // Act
            this.validatorTestClass.ValidationProperty = string.Empty;
            string errorReturnValue = (this.validatorTestClass as IDataErrorInfo)[nameof(this.validatorTestClass.ValidationProperty)];

            // Assert
            Assert.IsNotNull(errorReturnValue);
        }

        [TestMethod]
        public void GetValidStringErrorWhenPropertyHasInvalidCharacter()
        {
            // Arrange

            // Act
            this.validatorTestClass.ValidationProperty = "+nicht erlaubt";
            string errorReturnValue = (this.validatorTestClass as IDataErrorInfo)[nameof(this.validatorTestClass.ValidationProperty)];

            // Assert
            Assert.IsNotNull(errorReturnValue);
        }

        [TestMethod]
        public void GetStringLengthErrorWhenPropertyValueIsTooLong()
        {
            // Arrange

            // Act
            this.validatorTestClass.ValidationProperty = "Sehr sehr langer Name, also nicht gültig";
            string errorReturnValue = (this.validatorTestClass as IDataErrorInfo)[nameof(this.validatorTestClass.ValidationProperty)];

            // Assert
            Assert.IsNotNull(errorReturnValue);
        }

        [TestMethod]
        public void GetNoErrorWhenNoValidationPropertyGetsValidated()
        {
            // Act
            this.validatorTestClass.NoValidationProperty = "Andre";
            string errorReturnValue = (this.validatorTestClass as IDataErrorInfo)[nameof(this.validatorTestClass.NoValidationProperty)];

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(errorReturnValue));
        }

        #endregion



        #region Nested type: ValidatorTestClass

        public class ValidatorTestClass : BindableValidator
        {
            #region Fields

            private string _validationProperty;
            private string _noValidationProperty;

            #endregion



            #region Properties, Indexers

            [DisplayName]
            [Required]
            [ValidString()]
            [StringLength(15)]
            public string ValidationProperty
            {
                get { return this._validationProperty; }
                set { this.SetProperty(newValue => this._validationProperty = newValue, value); }
            }

            public string NoValidationProperty
            {
                get { return this._noValidationProperty; }
                set { this.SetProperty(newValue => this._noValidationProperty = newValue, value); }
            }

            #endregion
        }

        #endregion
    }
}