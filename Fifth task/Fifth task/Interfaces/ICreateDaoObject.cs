using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    public interface ICreateDaoObject
    {
        IDao<T> Create<T>(string stringConnection)where T:class;
    }
}
