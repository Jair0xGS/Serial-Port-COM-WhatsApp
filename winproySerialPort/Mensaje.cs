using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winproySerialPort
{
    class Mensaje
    {
        string[] parte;
        int contador;
        int numeroPartes;
        public int identificador { set; get; }
        bool estaLleno;
        public Mensaje(int identificador, int numeroPartes)
        {
            this.identificador = identificador;
            this.numeroPartes = numeroPartes;
            this.parte = new string[numeroPartes];
            estaLleno = false;
            this.contador = 0;

        }
        public long getNumeroPartes()
        {
            return numeroPartes;
        }
        public int getContador()
        {
            return contador;
        }
        public bool getEstaLleno()
        {
            return estaLleno;
        }
        public int getIdentificador()
        {
            return identificador;
        }
        public void insertarMensaje(int orden,string mensaje)
        {
            if (!estaLleno)
            {
                parte[orden-1] = mensaje;
                contador += 1;
                if (contador == numeroPartes)
                {
                    estaLleno = true;
                }
            }
            
        }
        public string getMensajeCompleto()
        {
            if (estaLleno)
            {
                string todo = "";
                for (int i = 0; i < parte.Length; i++)
                {
                    todo += parte[i];
                }
                return todo;
                
            }
            return "";
        }
    }
}
