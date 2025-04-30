using Wallety.Portal.Core.Enum;

namespace Wallety.Portal.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ExcelColumnMappingAttribute : Attribute
    {
        public ExcelColumnMappingAttribute(int excelColumn, EnumExcelColumnType columnType)
        {
            ExcelColumn = excelColumn;
            ColumnType = columnType;
        }

        public ExcelColumnMappingAttribute(string excelColumnName, EnumExcelColumnType columnType)
        {
            ExcelColumnName = excelColumnName;
            ColumnType = columnType;
        }

        public int ExcelColumn
        {
            get;
            set;
        }

        public string ExcelColumnName
        {
            get;
            set;
        }

        public EnumExcelColumnType ColumnType
        {
            get;
            set;
        }

        private bool _isRequired = true;

        /// <summary>
        /// Default is true
        /// </summary>
        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        private bool _validateDateTime = false;

        public bool ValidateDateTime
        {
            get
            {
                return _validateDateTime;
            }
            set
            {
                _validateDateTime = value;
            }
        }

        private bool _tryParseDateValue = false;

        public bool TryParseDateValue
        {
            get { return _tryParseDateValue; }
            set { _tryParseDateValue = value; }
        }

        // Needs to be implemented
        private bool _requireUnique = false;

        public bool RequireUnique
        {
            get { return _requireUnique; }
            set { _requireUnique = value; }
        }

        public int ColumnWidth
        {
            get; set;
        }

        public bool IsZeroNull
        {
            get; set;
        }
    }
}
