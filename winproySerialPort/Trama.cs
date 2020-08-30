using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winproySerialPort
{
    class Trama
    {
        byte tipo;

        byte identificador;
        byte tipoYIdentificador;
        byte[] orden;
        byte[] numeroParte;
        byte[] tamañoData;
        byte[] mensaje;
        byte[] cabezera = new byte[10];
        byte[] padding;
        int tamañoCuerpoEstatico = 1014;
        public Trama(byte tipo, int indentificador, int orden, int numeroPartes, string mensaje)
        {
            //sacar los ultimos 4 bits
            this.tipo = (byte)(tipo & 0x0F);
            //sacar los ultimos 4 bits
            this.identificador= (byte)(indentificador & 0x0F);
            //unir los 2 en un byte
            this.tipoYIdentificador = (byte)((this.tipo << 4) + this.identificador);


            // 3 bytes de orden  -> max value = 0xffffff = 16777215
            this.orden = new byte[3];
            this.orden[0] = (byte)orden;
            this.orden[1] = (byte)(orden >> 8);
            this.orden[2] = (byte)(orden >> 16);
            // 3 bytes de numero de partes
            this.numeroParte = new byte[3];
            this.numeroParte[0] = (byte)numeroPartes;
            this.numeroParte[1] = (byte)(numeroPartes >> 8);
            this.numeroParte[2] = (byte)(numeroPartes >> 16);
            // 3 bytes de tamaño de data
            this.tamañoData = new byte[3];
            int tamaño = mensaje.Length;
            this.tamañoData[0] = (byte)tamaño;
            this.tamañoData[1] = (byte)(tamaño >> 8);
            this.tamañoData[2] = (byte)(tamaño >> 16);
            // mensaje
            this.mensaje = new byte[tamaño];
            this.mensaje = ASCIIEncoding.UTF8.GetBytes(mensaje);
            //padding
            this.padding = new byte[tamañoCuerpoEstatico - tamaño];
            this.padding = ASCIIEncoding.UTF8.GetBytes(new string('@', tamañoCuerpoEstatico - tamaño));
            //armar cabezera completa
            this.cabezera[0] = tipoYIdentificador;
            Array.Copy(
                this.orden
                .Concat(this.numeroParte)
                .Concat(this.tamañoData).ToArray()
                , 0
                , cabezera
                , 1
                , 9
            );

        }
        public Trama(byte tipo, int indentificador, int orden, int numeroPartes, byte[] mensaje, int tamaño)
        {

            //MessageBox.Show(orden.ToString());

            //sacar los ultimos 4 bits
            this.tipo = (byte)(tipo & 0x0F);
            //sacar los ultimos 4 bits
            
            this.identificador = (byte)(indentificador & 0x0F);
            Console.WriteLine("tipoyIdentificador q llego fue " + this.identificador.ToString("X2"));
            //unir los 2 en un byte
            this.tipoYIdentificador = (byte)((this.tipo << 4) + this.identificador);
            Console.WriteLine("tipoyIdentificador q llego fue " + this.tipoYIdentificador.ToString("X2"));

            // 3 bytes de orden  -> max value = 0xffffff = 16777215
            this.orden = new byte[3];
            this.orden[0] = (byte)orden;
            this.orden[1] = (byte)(orden >> 8);
            this.orden[2] = (byte)(orden >> 16);
            // 3 bytes de numero de partes
            this.numeroParte = new byte[3];
            this.numeroParte[0] = (byte)numeroPartes;
            this.numeroParte[1] = (byte)(numeroPartes >> 8);
            this.numeroParte[2] = (byte)(numeroPartes >> 16);
            // 3 bytes de tamaño de data
            this.tamañoData = new byte[3];
            this.tamañoData[0] = (byte)tamaño;
            this.tamañoData[1] = (byte)(tamaño >> 8);
            this.tamañoData[2] = (byte)(tamaño >> 16);
            // mensaje
            this.mensaje = new byte[tamaño];
            Array.Copy(mensaje, this.mensaje, tamaño);
            //padding
            this.padding = new byte[tamañoCuerpoEstatico - tamaño];
            this.padding = ASCIIEncoding.UTF8.GetBytes(new string('@', tamañoCuerpoEstatico - tamaño));
            //armar cabezera completa
            this.cabezera[0] = tipoYIdentificador;
            Array.Copy(
                this.orden
                .Concat(this.numeroParte)
                .Concat(this.tamañoData).ToArray()
                , 0
                , cabezera
                , 1
                , 9
            );
            
        }
        public Trama(byte[] byteArray)
        {
            this.tipoYIdentificador = byteArray[0];
            Console.WriteLine("tipoyIdentificador q llego fue " + this.tipoYIdentificador.ToString("X2") );
            this.tipo = (byte)(tipoYIdentificador >> 4);
            this.identificador = (byte)(tipoYIdentificador & 0x0f);

            // 3 bytes de orden  -> max value = 0xffffff = 16777215
            this.orden = new byte[3];
            Array.Copy(byteArray, 1, orden, 0, 3);
            //MessageBox.Show(BitConverter.ToString(orden));
            //MessageBox.Show(bytesof3ToInt(orden).ToString());
            // 3 bytes de numero de partes
            this.numeroParte= new byte[3];
            Array.Copy(byteArray, 4, numeroParte, 0, 3);
            // 3 bytes de tamaño de data
            this.tamañoData = new byte[3];
            Array.Copy(byteArray, 7,tamañoData, 0, 3);

            int tamaño = bytesof3ToInt(this.tamañoData);

            this.mensaje = new byte[tamaño];
            Array.Copy(byteArray, 10, mensaje, 0, tamaño);
            this.padding = new byte[tamañoCuerpoEstatico - tamaño];
            this.padding = ASCIIEncoding.UTF8.GetBytes(new string('@', tamañoCuerpoEstatico - tamaño));
            this.cabezera[0] = tipoYIdentificador;
            Array.Copy(
                this.orden
                .Concat(this.numeroParte)
                .Concat(this.tamañoData).ToArray()
                , 0
                , cabezera
                , 1
                , 9
            );
        }
        public int bytesof3ToInt(byte[] arr)
        {
            //MessageBox.Show(BitConverter.ToString(arr));
            return (arr[2]<<16)+(arr[1]<<8)+(arr[0]) ;
        }
        
        public byte getTipo()
        {
            return tipo;
        }
        public int getOrdenInt()
        {
            return bytesof3ToInt(orden);
        }
        public string getOrdenHexString()
        {
            return BitConverter.ToString(orden);
        }
        public int getNumeroPartes()
        {
            return bytesof3ToInt(numeroParte);
        }
        public int getTamañoMensaje()
        {
            return bytesof3ToInt(tamañoData);
        }
        public int getIdentificador()
        {
            return identificador;
        }
        public byte[] getCabezera()
        {
            return cabezera;
        }
        public string getCabezeraString()
        {
            return BitConverter.ToString(cabezera);
        }
        public byte[] getTramaMensaje()
        {
            return mensaje;
        }
        public string getMensajeString()
        {
            return ASCIIEncoding.UTF8.GetString(mensaje);
        }
        public byte[] getTramaCompleta()
        {
            
            return cabezera.Concat(mensaje).Concat(padding).ToArray();
        }
        public string getTramaCompletaString()
        {
            return BitConverter.ToString(cabezera.Concat(mensaje).Concat(padding).ToArray());
        }
    }
}
