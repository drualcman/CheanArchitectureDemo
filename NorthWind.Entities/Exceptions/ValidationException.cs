using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.Exceptions
{
    public class ValidationException :  Exception
    {
        public Dictionary<string, List<string>> Failures { get; }
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception inner) : base(message, inner) { }

        public ValidationException(string message, string propertyName, string error) : base(message) =>
            Failures = new Dictionary<string, List<string>>
            {
                [propertyName] = new List<string> { error }
            };

        public ValidationException(string message, IEnumerable<KeyValuePair<string, string>> failues) : base(message)
        {
            Failures = new Dictionary<string, List<string>>();
            foreach (var failure in failues)
            {
                if (Failures.ContainsKey(failure.Key))
                {
                    Failures[failure.Key].Add(failure.Value);
                }
                else
                {
                    Failures.Add(failure.Key, new List<string>() { failure.Value});
                }
            }
        }

        public ValidationException(string message, List<string> failuresMessages) : base(message) =>
            Failures = new()
            {
                ["Error"] = failuresMessages
            };
    }
}
