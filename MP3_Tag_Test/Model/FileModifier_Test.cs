// ///////////////////////////////////
// File: FileModifier_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Model
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Exception;
    using MP3_Tag.Model;



    [TestClass]
    public class FileModifier_Test
    {
        #region Fields

        private FileModifier fileModifier;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.fileModifier = new FileModifier();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void ReturnTrueWhenFileExists()
        {
            // Arrange
            const string FilePath = @"C:\Windows\System32\cmd.exe";

            // Act
            bool actualValue = this.fileModifier.FileExists(FilePath);

            // Assert
            Assert.IsTrue(actualValue);
        }

        [TestMethod]
        public void ReturnFalseWhenFilePathEmptyString()
        {
            // Act
            bool actualValue = this.fileModifier.FileExists(string.Empty);

            // Assert
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        public void ReturnFalseWhenFilePathIsNull()
        {
            // Act
            bool actualValue = this.fileModifier.FileExists(null);

            // Assert
            Assert.IsFalse(actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(FileException))]
        public void ThrowDeleteExceptionWhenFileNotExists()
        {
            // Act
            this.fileModifier.Delete(string.Empty);

            // trhow exception
        }

        [TestMethod]
        [ExpectedException(typeof(FileException))]
        public void ThrowMoveExceptionWhenFileNotExists()
        {
            // Act
            this.fileModifier.Rename(string.Empty, null);

            // throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(FileException))]
        public void ThrowMoveExceptionWhenFileAlreadyExists()
        {
            // Arrange
            const string OldFilePath = @"C:\Windows\System32\cmd.exe";
            const string NewFilePath = @"C:\Windows\System32\cmdkey.exe";

            // Act
            this.fileModifier.Rename(OldFilePath, NewFilePath);

            // throw exception
        }

        #endregion
    }
}