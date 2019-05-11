using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StoreManagement.Model;

namespace StoreManagingApp.DataProcessing
{
    public static class ValidationHelper
    {
        public static List<object> ValidateStore(StoreCharacters store)
        {
            var context = new ValidationContext(store, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(store, context, results, validateAllProperties: true);

            var errorMessage = new List<object>();
            if (results.Any())
            {                
                foreach (var res in results)
                {
                    errorMessage.Add(res.ErrorMessage);
                }
            }

            return errorMessage;
        }
    }
}
