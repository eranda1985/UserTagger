using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IConverter<T, U>
    {
        void Convert(T source, out U dest);
    }
}
