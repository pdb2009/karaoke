using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karaoke
{
    class Frase
    {
        public Frase(String texto, uint tempoInicio, uint tempoFim)
        {
            if (tempoFim < tempoInicio)
            {                
                throw new Exception("O tempo de fim deve ser maior que o tempo de início");
            }
            else
            {
                this.texto = texto;
                this.tempoInicio = tempoInicio;
                this.tempoFim = tempoFim;    
            }
        }

        private String texto;

        public String Texto
        {
            get { return texto; }
            set { texto = value; }
        }
        private uint tempoInicio;

        public uint TempoInicio
        {
            get { return tempoInicio; }
            set
            {
                if (value > this.tempoFim)
                {                    
                    throw new Exception("O tempo de fim deve ser maior que o tempo de início");
                }
                else
                {
                    tempoInicio = value;
                }
            }
        }
        private uint tempoFim;

        public uint TempoFim
        {
            get { return tempoFim; }
            set 
            {
                if (value < this.tempoInicio)
                {                    
                    throw new Exception("O tempo de fim deve ser maior que o tempo de início");
                }
                else
                {
                    tempoFim = value;
                }
            }
        }

        public override String ToString()
        {
            return this.texto + " (" + this.tempoInicio.ToString() + " - " + this.tempoFim.ToString() + ")"; 
        }
    }
}
