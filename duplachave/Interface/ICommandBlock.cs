using duplachave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Interface
{
    public interface ICommandBlock
    {
        string CommandStartName { get; }
        string CommandEndName { get; }
        string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace, string stringBetween);
    }
}
