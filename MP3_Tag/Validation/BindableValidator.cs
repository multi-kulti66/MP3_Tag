// ///////////////////////////////////
// File: BindableValidator.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using GalaSoft.MvvmLight;
    using MP3_Tag.Properties;



    public abstract class BindableValidator : ViewModelBase, IDataErrorInfo
    {
        #region Properties, Indexers

        public bool IsValid
        {
            get
            {
                foreach (PropertyInfo propertyinfo in this.GetPropertyInfos())
                {
                    string error = (this as IDataErrorInfo)[propertyinfo.Name];

                    if (!string.IsNullOrEmpty(error))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion



        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { throw new NotSupportedException(Resources.BindableValidator_IDataErrorInfo_PropertyErrorNotSupported); }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.OnValidate(propertyName); }
        }

        #endregion



        #region Methods

        protected void SetProperty<T>(Action<T> paramStorage, T paramValue, [CallerMemberName] string propertyName = null)
        {
            paramStorage(paramValue);
            this.RaisePropertyChanged(propertyName);
        }

        protected virtual string OnValidate(string propertyName)
        {
            var results = new List<ValidationResult>();

            var value = this.GetPropertyValueAndConvertToEmptyStringIfNecessary(propertyName);
            var result = Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName }, results);

            string error = string.Empty;

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }

            return error;
        }

        private object GetPropertyValueAndConvertToEmptyStringIfNecessary(string propertyName)
        {
            PropertyInfo[] propertyInfos = this.GetPropertyInfos();

            var value = propertyInfos.First(x => x.Name == propertyName).GetValue(this);
            var type = propertyInfos.First(x => x.Name == propertyName).PropertyType;

            if ((type == typeof(string)) && (value == null))
            {
                value = string.Empty;
            }

            return value;
        }

        private PropertyInfo[] GetPropertyInfos()
        {
            return this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.CanWrite && x.CanRead).ToArray();
        }

        #endregion
    }
}