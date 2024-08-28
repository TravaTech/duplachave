using duplachave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Interface
{
    internal interface ICommandKey
    {
        string CommandName { get; }
        string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace);
    }
}
