namespace Wallety.Portal.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class ValidationAttribute : Attribute
    {
        public ValidationAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName
        {
            get;
            protected set;
        }

        public string FriendlyName
        {
            get;
            set;
        }

        public string DescriptivePropertyName
        {
            get;
            set;
        }

        private bool _nullIsValid = false;
        /// <summary>
        /// Gets or sets a boolean indicating if a null value should be validated as null.  If not, validation will fail if a null value is passed
        /// Default is false
        /// </summary>
        public bool NullIsValid
        {
            get
            {
                return _nullIsValid;
            }
            set
            {
                _nullIsValid = value;
            }
        }

        private bool _allowNullValue = true;
        /// <summary>
        /// Gets or sets a boolean indicating if a null value is allowed.  If not and a null value is passed, an exception will be thrown
        /// Default is true
        /// </summary>
        public bool AllowNullValue
        {
            get
            {
                return _allowNullValue;
            }
            set
            {
                _allowNullValue = value;
            }
        }

        public abstract ValidationInfo Validate(object value);

        protected virtual bool IsValueNull(object value)
        {
            if (value == null)
            {
                if (!AllowNullValue)
                {
                    throw new ArgumentNullException("value", "Cannot validate a null parameter");
                }

                return true;
            }

            return false;
        }

        protected bool IsValueNullOrEmpty(object value)
        {
            if (IsValueNull(value))
            {
                return true;
            }

            return string.IsNullOrEmpty(value.ToString());
        }

        protected bool ValidateNullValue(object value)
        {
            if (IsValueNull(value))
            {
                return NullIsValid;
            }

            return true;
        }

    }

    public class ValidationInfo
    {
        private static ValidationInfo _valid;

        public static ValidationInfo Valid
        {
            get
            {
                if (_valid == null)
                {
                    _valid = new ValidationInfo(true, string.Empty);
                }

                return _valid;
            }
        }

        public ValidationInfo(bool isValid, ValidationAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute", "Parameter attribute cannot be null");
            }

            IsValid = isValid;
            ValidationAttribute = attribute;
        }

        public ValidationInfo(bool isValid, ValidationAttribute attribute, string errorMessage)
            : this(isValid, attribute)
        {
            ErrorMessage = errorMessage;
        }

        public ValidationInfo(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public ValidationAttribute ValidationAttribute
        {
            get;
            set;
        }

        public bool IsValid
        {
            get;
            set;
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_errorMessage) && !IsValid)
                {
                    _errorMessage = ValidationAttribute.FriendlyName + " is invalid";
                }

                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }
    }
}
