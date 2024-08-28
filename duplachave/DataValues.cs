using duplachave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave
{
    public class DataValues
    {
        public List<DataChave> chaves { get; set; }
        public List<DataIndex> indexs { get; set; }
        public DataValues()
        {
            chaves = new List<DataChave>();
            indexs = new List<DataIndex>();
        }
        public bool ContemChave(string chave)
        {
            return chaves.Exists(x => x.chave == chave);
        }
        public void AddChave(string valor)
        {
            string[] chave = valor.Substring(0, valor.Length - 2).Substring(2).Split(':');

            if (!ContemChave(chave[0]))
            {
                DataChave data = new DataChave();
                data.chave = chave[0];
                if (chave.Length > 1)
                {
                    data.AddComando(chave[1]);
                }
                chaves.Add(data);
            }
            else
            {
                DataChave data = chaves.FirstOrDefault(x => x.chave == chave[0]);
                if (chave.Length > 1)
                {
                    data.AddComando(chave[1]);
                }
            }

            if (chave[0].Contains('['))
            {
                DataIndex data = new DataIndex();
                string[] partes = chave[0].Split('[');

                if (!indexs.Exists(x => x.chave == partes[0]))
                {
                    data.chave = partes[0];
                    for (int i = 0; i < partes.Length; i++)
                    {
                        data.indexVar = partes[i].Split(']')[0];
                        int poss = 0;
                        if (int.TryParse(data.indexVar, out poss))
                        {
                            data.index = poss;
                        }
                        else
                        {
                            data.index = -1;
                        }
                    }
                    indexs.Add(data);
                }
            }
        }
    }
}
