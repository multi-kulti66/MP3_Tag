// ///////////////////////////////////
// File: ValidStringAttribute.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using MP3_Tag.Properties;



    public class ValidStringAttribute : ValidationAttribute
    {
        #region Constructors

        public ValidStringAttribute()
        {
            this.ErrorMessage = Resources.Validation_Error_InvalidCharacter;
        }

        #endregion



        #region Methods

        public override bool IsValid(object value)
        {
            return this.IsValidString(value as string);
        }

        private bool IsValidString(string paramValue)
        {
            return paramValue.All(c => ((c >= '0') && (c <= '9')) || ((c >= 'A') && (c <= 'Z'))
                                       || (c == 'Ä') || (c == 'Ö') || (c == 'Ü')
                                       || (c == 'ä') || (c == 'ö') || (c == 'ü')
                                       || ((c >= 'a') && (c <= 'z')) || (c == ' ')
                                       || (c == '.') || (c == ',') || (c == '_') || (c == '-')
                                       || (c == '(') || (c == ')') || (c == '&') || (c == '\''));
        }

        #endregion
    }
}