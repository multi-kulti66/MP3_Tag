// ///////////////////////////////////
// File: ObjectBase.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Helper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;



    public abstract class ObjectBase : IEditableObject
    {
        #region Fields

        private bool isDictionaryInitialized;
        private Dictionary<string, object> propertyDictionary;

        #endregion



        #region Constructors

        protected ObjectBase()
        {
            this.InEditMode = false;
            this.isDictionaryInitialized = false;
        }

        #endregion



        #region Properties, Indexers

        public bool InEditMode { get; private set; }

        #endregion



        #region IEditableObject Members

        public void BeginEdit()
        {
            if (!this.InEditMode)
            {
                this.InEditMode = true;
            }
        }

        public void CancelEdit()
        {
            if (!this.InEditMode)
            {
                return;
            }

            this.RestoreAllPropertyValues();
            this.InEditMode = false;
        }

        public void EndEdit()
        {
            if (this.InEditMode)
            {
                this.InitDictionary();
                this.InEditMode = false;
            }
        }

        #endregion



        #region Methods

        protected void SetProperty<T>(Action<T> paramStorage, T paramValue)
        {
            this.InitDictionaryIfNotInitialized();

            if (Equals(paramStorage, paramValue))
            {
                return;
            }

            paramStorage(paramValue);
            this.UpdateState();
        }

        private void InitDictionaryIfNotInitialized()
        {
            if (this.isDictionaryInitialized)
            {
                return;
            }

            this.InitDictionary();
            this.isDictionaryInitialized = true;
        }

        private void InitDictionary()
        {
            this.propertyDictionary = new Dictionary<string, object>();
            this.AddAllPropertiesIntoDictionary();
        }

        private void AddAllPropertiesIntoDictionary()
        {
            this.IteratePropertiesAndDoAction(this.AddPropertyIntoDictionary);
        }

        private void AddPropertyIntoDictionary(string paramKey, object paramValue)
        {
            this.propertyDictionary.Add(paramKey, paramValue);
        }

        private void RestoreAllPropertyValues()
        {
            this.IteratePropertiesAndDoAction(this.RestorePropertyValue);
        }

        private void RestorePropertyValue(string paramObjectKey, object paramObjectValue)
        {
            object dictionaryValue = this.propertyDictionary[paramObjectKey];

            if (dictionaryValue == paramObjectValue)
            {
                return;
            }

            this.GetType().GetProperty(paramObjectKey).SetValue(this, dictionaryValue);
        }

        private void IteratePropertiesAndDoAction(Action<string, object> paramAction)
        {
            PropertyInfo[] propertyInfos = this.GetPropertyInfos();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                object propertyValue = propertyInfo.GetValue(this);
                paramAction(propertyName, propertyValue);
            }
        }

        private PropertyInfo[] GetPropertyInfos()
        {
            return this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.CanWrite && x.CanRead).ToArray();
        }

        private void UpdateState()
        {
            if (!this.InEditMode)
            {
                this.BeginEdit();
            }
            else
            {
                if (this.HavePropertyValuesChanged())
                {
                    return;
                }

                this.InEditMode = false;
            }
        }

        private bool HavePropertyValuesChanged()
        {
            return this.IteratePropertiesAndGetResult(this.HasPropertyValueChanged);
        }

        private bool HasPropertyValueChanged(string paramObjectKey, object paramObjectValue)
        {
            object dictionaryValue = this.propertyDictionary[paramObjectKey];

            if (dictionaryValue != paramObjectValue)
            {
                return true;
            }

            return false;
        }

        private bool IteratePropertiesAndGetResult(Func<string, object, bool> paramFunc)
        {
            PropertyInfo[] propertyInfos = this.GetPropertyInfos();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                object propertyValue = propertyInfo.GetValue(this);

                if (paramFunc(propertyName, propertyValue))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}