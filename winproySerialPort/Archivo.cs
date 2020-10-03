using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace winproySerialPort
{
    class Archivo
    {
        
        int contador;
        //static ReaderWriterLock rwl = new ReaderWriterLock();
        int tamaño;
        public int identificador { set; get; }
        public string name;
        private static Random random = new Random();
        bool estaLleno;
        public string directorioDeGuardado { set; get; }
        public string nombreArchivo{ set; get; }
        public string nombreFuente { set; get; }
        List<Task> taskList = new List<Task>();
        public Archivo(int identificador, int tamaño, string nombreArchiv, string directorioDeGuardado)
        {
            this.name = "";
            this.nombreFuente = nombreArchiv;
            string nombre = Path.GetFileNameWithoutExtension(nombreArchiv);
            string numeroRamdom = RandomString(10);
            string extension = Path.GetExtension(nombreArchiv);


            this.name = nombre + numeroRamdom + extension;

            //MessageBox.Show(nombreArchiv);
            //MessageBox.Show("nopmbre archivo"+ name);
            //MessageBox.Show("nopmbre archivo de path" + Path.GetFileNameWithoutExtension(nombreArchiv));
            this.identificador = identificador;
            this.tamaño = tamaño;
            estaLleno = false;
            this.contador = 0;
            this.directorioDeGuardado = directorioDeGuardado;
            

        }
        //public static 
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string getInfo()
        {
            return "estalleno:" + estaLleno.ToString() + " indeficador:" + identificador + " tamaño:" + tamaño.ToString() + " contador:" + contador.ToString();
        }
        public int getPorcentajeDeAvance()
        {
            return (int)((16777215 / tamaño) );
        }
        public long getTamaño()
        {
            return tamaño;
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
        public string getFullPath()
        {
            return directorioDeGuardado + "\\" + name;
        }
        public async void insertarBytes(int orden, byte[] mensaje)
        {
            //rwl.AcquireWriterLock(10);
             using  (var fs = new FileStream(directorioDeGuardado+"\\"+name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, bufferSize: 4096, useAsync: true))
            {
                
                fs.Position = (orden-1)*1014;
                await fs.WriteAsync(mensaje, 0, mensaje.Length);
            }
            
            
            
            contador += 1;
            if (contador == tamaño)
            {
                estaLleno = true;
            }
            //rwl.ReleaseWriterLock();
        }
        

    }
}
