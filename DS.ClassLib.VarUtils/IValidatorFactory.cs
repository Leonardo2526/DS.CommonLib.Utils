using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public interface IValidatorFactory<T>
    {
        IValidator<T> GetValidator();
    }
}
