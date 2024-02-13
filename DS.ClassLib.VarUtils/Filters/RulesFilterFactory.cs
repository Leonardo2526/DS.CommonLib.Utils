using DS.ClassLib.VarUtils.Collisons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Filters
{
    public class RulesFilterFactory<T1, T2> : IFilterFactory<(T1, T2)>
    {
        private readonly IEnumerable<Func<(T1, T2), bool>> _rules;

        public RulesFilterFactory(IEnumerable<Func<(T1, T2), bool>> rules)
        {
            _rules = rules;
        }

        public Func<(T1, T2), bool> GetFilter()
        {
            bool func((T1, T2) f)
            {
                foreach (var rool in _rules)
                {
                    if (!rool.Invoke(f)) { return false; }
                }
                return true;
            }

            return func;
        }
    }
}
