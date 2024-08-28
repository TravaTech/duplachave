using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Model
{
    public class DataComando
    {
        public string comando { get; set; }
        public string fullcomando { get; set; }
        public Dictionary<int, string> parametros { get; set; }
        public DataComando()
        {
            parametros = new Dictionary<int, string>();
        }
    }
}
