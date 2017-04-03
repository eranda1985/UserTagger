using Api.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Validators
{
    public static class UserTaggerValidator
    {
         public static Validation CheckNull<T>(T obj, string param) where T: class
        {
            return Validate.Begin().IsNotNull<T>(obj, param).Check();
        }
    }
}
