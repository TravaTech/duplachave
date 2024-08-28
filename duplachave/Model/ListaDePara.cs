using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Model
{
    public class ListaDePara
    {
        public string Nome { get; set; }

        public List<string> Campos { get; set; }

        public Dictionary<string, Dictionary<string, string>> Valores { get; set; }

        public ListaDePara()
        {
            Campos = new List<string>();
            Valores = new Dictionary<string, Dictionary<string, string>>();
        }
    }
}
