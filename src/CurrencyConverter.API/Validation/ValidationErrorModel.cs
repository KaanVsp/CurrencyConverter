namespace CurrencyConverter.API.Validation
{
    public class ValidationErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }

        public ValidationErrorModel(string FieldName, string Message)
        {
            this.FieldName = FieldName;
            this.Message = Message;
        }
    }
}
