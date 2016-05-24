using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model
{
    public class HelpScoutError
    {
        public int Code { get; set; }
        public string Error { get; set; }
        public IList<ValidationError> ValidationErrors { get; set; }
    }

    public class ValidationError
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("Property:'{0}', Value:'{1}', Message:'{2}'", Property, Value, Message);
        }
    }
}
