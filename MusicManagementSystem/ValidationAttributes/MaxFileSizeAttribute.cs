using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as IFormFileCollection;
            if (files != null)
            {
                foreach(var formFile in files)
                {
                    if (formFile.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessageForEnumerable(formFile.FileName, formFile.Length));
                    }
                }
                return ValidationResult.Success;
            }

            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize / 1_048_576} MBytes.";
        }

        public string GetErrorMessageForEnumerable(string fileName, long fileSize)
        {
            return GetErrorMessage() + $" {fileName} is {fileSize / 1_048_576} MBytes";
        }

    }
}
