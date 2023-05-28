using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7.Repository
{
    internal interface IRepository<Value>
    {
        List<Value> GetAll();
        int insert(Value value);
        int update(int id, Value value);
    }
}
