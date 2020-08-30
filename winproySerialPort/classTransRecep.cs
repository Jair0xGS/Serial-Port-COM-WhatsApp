using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace winproySerialPort
{
    class classTransRecep
    {
        // es como un puntero en c++

        /**
         * C# delegates are similar to pointers to functions, in C or C++. 
         * A delegate is a reference type variable that holds the reference to a method. 
         * The reference can be changed at runtime.
         * Delegates are especially used for implementing events and the call-back methods. 
         * All delegates are implicitly derived from the System.Delegate class.
         */
        public delegate void HandlerIO(object oo , string mensaje);
        public event HandlerIO LlegoMensaje;
        public event HandlerIO LlegoArchivo;
        Thread procesoVerificaSalida;
        private SerialPort puerto;
        List<Task> taskList = new List<Task>();
        public string directorioDeGuardado { set; get; }
        private string mensRecibido;
        private string nombreArchivoRecibido;
        private string pathArchivoRecibido;
        private int porcentajeAvanzeArchivo;
        long tamañoMaximoMensajeDeTrama = 1014;
        private Boolean bufferVacio;
        byte[] TramaDeRelleno;

        byte[] TramaRecibida;

        List<Mensaje> mensajes;
        List<Archivo> archivos;
        private int  contadorIDsMensajes ;
        private volatile int contadorIDsArchivos;
        private volatile int contadorIDsArchivosEntrantes;
        public classTransRecep(string dir)
        {
            directorioDeGuardado = dir;
            TramaDeRelleno      = new byte[1024];
            TramaRecibida = new byte[1024];
            contadorIDsMensajes = 0;
            contadorIDsArchivos = 0; contadorIDsArchivosEntrantes = 0;
             mensajes = new List<Mensaje>();
            archivos = new List<Archivo>();
            for (int i = 0; i < 1023; i++)
            {
                TramaDeRelleno[i] = 64;
            }

        }
        public void Inicializa(string NombrePuerto)
        {
            puerto = new SerialPort(NombrePuerto, 57600, Parity.Even, 8, StopBits.Two);
            puerto.ReceivedBytesThreshold = 1024;
            puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
            puerto.Open();

            procesoVerificaSalida = new Thread(verificandoSalida);
            procesoVerificaSalida.Start();

            //MessageBox.Show("apertura del puerto" + puerto.PortName) ;
            
        }
        public void termina()
        {
            puerto.Close();
        }
        public void cambiaVelocidad(int velocidad)
        {
            puerto.BaudRate = velocidad;
        }

        private void puerto_DataReceived(object oo, SerialDataReceivedEventArgs serialDataReceivedEventArg)
        {
           

            if (puerto.BytesToRead >= 1024)
            {
                puerto.Read(TramaRecibida, 0, 1024);

                 

                Trama recibida =new Trama(TramaRecibida);
                //MessageBox.Show("trama recibida:\n" + recibida.getStringCabezeraPartes());
                var allTask = Task.WhenAll(taskList.ToArray());
                switch (recibida.getTipo())
                {
                    //mensaje
                    case 0xb:

                        //MessageBox.Show("trama mensaje info:\n" +mensajes[0].getInfo());
                        
                        if (allTask.IsCompleted)
                        {
                            taskList.Add(Task.Factory.StartNew(() => recibiendoMensaje(recibida)));
                        }
                        

                        //procesoRecibirMensaje = new Thread(recibiendoMensaje);
                        //procesoRecibirMensaje.Start();
                        break;
                    //archivo
                    case 0xa:
                        //MessageBox.Show(recibida.getOrdenInt().ToString());
                        if (allTask.IsCompleted)
                        {
                            taskList.Add(Task.Factory.StartNew(() => recibiendoArchivo(recibida)));
                        }
                        ;
                        break;
                    //informacion
                    case 0xf:
                        if(recibida.getTamañoMensaje() == 0)
                        {
                            //informacion de mensajes
                            mensajes.Add(new Mensaje(recibida.getIdentificador(), recibida.getNumeroPartes()));
                            //MessageBox.Show("trama ifo mensaje :\n" + mensajes[0].getInfo());
                        }
                        else
                        {
                            //informacion de archivos
                            Archivo temp = new Archivo(recibida.getIdentificador(), recibida.getNumeroPartes(), recibida.getMensajeString(), directorioDeGuardado);
                            archivos.Add(temp);
                            Console.WriteLine(directorioDeGuardado + "//" + recibida.getMensajeString());
                            onLlegoArchivo(temp.getFullPath(), 0.ToString(), recibida.getIdentificador().ToString(), "20");
                            
                        }
                        
                        break;

                    default:

                        Console.WriteLine("tipo de trama no valida " +recibida.getCabezeraString() );

                        break;
                }

               
            }
            //MessageBox.Show(mensRecibido);
        }

        public void recibirInformacion()
        {
            //string header
        }
        public void recibiendoArchivo(Trama recibida)
        {
            foreach (var item in archivos)
            {
                if (!item.getEstaLleno())
                {

                    if (item.getIdentificador() == recibida.getIdentificador())
                    {
                        //item.insertarMensaje(recibida.getOrden(), recibida.getMensajeString());
                        Console.WriteLine("#######");
                        Console.WriteLine("trama recibida "+recibida.getCabezeraString());
                        Console.WriteLine(item.getIdentificador().ToString() + "  " + item.getPorcentajeDeAvance().ToString());
                        Console.WriteLine("#######");
                        item.insertarBytes(recibida.getOrdenInt(), recibida.getTramaMensaje());
                        onLlegoArchivo("", item.getPorcentajeDeAvance().ToString(), item.getIdentificador().ToString(), "21");
                        if (item.getEstaLleno())
                        {
                            
                            pathArchivoRecibido = item.getFullPath();
                            item.identificador = 555;
                            //onLlegoArchivo("",item.getPorcentajeDeAvance().ToString(),item.getIdentificador().ToString(),"21");
                            //onLlegoMensaje();

                        }
                    }
                }
            }
        }
        public void recibiendoMensaje(Trama recibida)
        {
            
            foreach (var item in mensajes)
            {
                if (! item.getEstaLleno())
                {

                    if (item.getIdentificador() == recibida.getIdentificador())
                    {
                        item.insertarMensaje(recibida.getOrdenInt(), recibida.getMensajeString());
                        //MessageBox.Show("llego mensaje");
                        if (item.getEstaLleno())
                        {
                            mensRecibido = item.getMensajeCompleto();
                            item.identificador = 777;
                            onLlegoMensaje();

                        }
                    }
                }
            }
            
        }
        protected virtual void onLlegoMensaje()
        {
            if (LlegoMensaje != null)
            {
                //MessageBox.Show("ejecutando on llego mensaje");
                LlegoMensaje(this, mensRecibido);

            }

        }
        protected virtual void onLlegoArchivo(string nombre,string porcentaje,string identificador,string tipoDeArchivo )
        {
            if (LlegoArchivo != null)
            {
                LlegoArchivo(this, nombre+">"+porcentaje+">"+identificador+">"+tipoDeArchivo);

            }

        }

        public void Enviar(string mens,string tipoM)
        {
            if(tipoM == "Mensaje")
            {

                //procesoEnvio = new Thread(Enviando);
                int copia = contadorIDsMensajes;
                Task.Factory.StartNew(() => EnviandoMensaje(mens, copia));
                
                //procesoEnvio.Start(mens);
                if (contadorIDsMensajes == 15)
                {
                    contadorIDsMensajes = 0;
                }
                else
                {
                    contadorIDsMensajes += 1;
                }


            }
            else if(tipoM == "Archivo")
            {
                //MessageBox.Show("lego a enviar");
                Console.WriteLine(contadorIDsArchivos.ToString());
                int copia = contadorIDsArchivos;
                Task.Factory.StartNew(() => EnviandoArchivo(mens, copia));

                if (contadorIDsArchivos == 15)
                {
                    contadorIDsArchivos = 0;
                }
                else
                {
                    contadorIDsArchivos += 1;
                }
            }
            

        }
        private void EnviandoArchivo(string nombreArchivo, int contador)
        {
            //MessageBox.Show("lego a enviando archivo");
            using (System.IO.Stream f = new System.IO.FileStream(nombreArchivo, FileMode.Open))
            {
                int offset = 0;
                long len = f.Length;
                byte[] buffer = new byte[1014];

                if(f.Length/tamañoMaximoMensajeDeTrama+1 < 16777215)
                {
                    int numPart = Convert.ToInt32(f.Length / tamañoMaximoMensajeDeTrama);

                    if (f.Length % tamañoMaximoMensajeDeTrama != 0)
                    {
                        numPart += 1;
                    }
                    int porcentaje = Convert.ToInt32((16777215 / numPart) );
                   
                    Trama info = new Trama(0xf, contador, 0, numPart, nombreArchivo);
                    puerto.Write(info.getTramaCompleta(), 0, 1024);
                    //onLlegoArchivo(nombreArchivo, 0.ToString(), contadorIDsArchivos.ToString(), "10");
                    int readLen = 1014; // leer pedazos de 1024
                    Trama parte;
                    int i = 1;
                    while (offset != len)
                    {
                        if (offset + readLen > len)
                        {
                            readLen = (int)len - offset;
                        }
                        //MessageBox.Show("offset : "+offset.ToString()+" redlen :"+readLen.ToString()+" len:"+len.ToString());

                        //deberia funcionar  ----arreglar
                        offset += f.Read(buffer, 0, readLen);
                       
                        parte = new Trama(0xa, contador, i,0,buffer,readLen);
                        //Console.WriteLine("--------------");
                        //Console.WriteLine("i= "+i.ToString());
                        //Console.WriteLine(parte.getCabezeraString());
                        //Console.WriteLine("orden int = " + parte.getOrdenInt().ToString() + " orden hex ="+parte.getOrdenHexString());
                        ///d
                        Console.WriteLine("--------------");
                        Console.WriteLine("enviando trama "+parte.getCabezeraString());
                        Console.WriteLine(contador.ToString() + "  " + porcentaje.ToString());
                        Console.WriteLine("--------------");
                        puerto.Write(parte.getTramaCompleta(), 0, 1024);
                        //Console.WriteLine("");
                        
                        onLlegoArchivo("", porcentaje.ToString(), contador.ToString(), "11");
                        //MessageBox.Show(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
                        buffer = new byte[1014];
                        i++;
                    }
                }
                else
                {
                    // generar un error por q el archivo es demasiado grande para enviar :v
                }
                
                
            }

            
        }
        private void EnviandoMensaje(string mensaje, int contador)
        {

           

            int numPart =Convert.ToInt32( (mensaje).Length / tamañoMaximoMensajeDeTrama);

            if(numPart < 16777215)
            {
                if ((mensaje).Length % tamañoMaximoMensajeDeTrama != 0)
                {
                    numPart += 1;
                }
                //TRAMA DE INFORMACION
                Trama info = new Trama(0xf, contador, 0, numPart, "");
                //MessageBox.Show("trama enviada:\n"+ info.getStringCabezeraPartes());
                puerto.Write(info.getTramaCompleta(), 0, 1024);

                Trama parte;
                for (int i = 1; i <= numPart; i++)
                {
                    if (i != numPart)
                    {

                        
                        parte = new Trama(0xb, contador, i,0, mensaje.Substring(1014 * (i - 1), 1014));
                    }
                    else
                    {
                        
                        parte = new Trama(0xb, contador, i,0, mensaje.Substring(1014 * (i - 1)));
                    }
                    //MessageBox.Show("trama enviada:\n" + parte.getStringCabezeraPartes());
                    puerto.Write(parte.getTramaCompleta(), 0, 1024);

                }
                
            }
            


            //puerto.Write(TramaCabezeraEnvio, 0, TramaCabezeraEnvio.Length);
            //puerto.Write(TramaDeEnvio, 0, TramaDeEnvio.Length);
            //puerto.Write(TramaDeRelleno, 0, 1024 - TramaCabezeraEnvio.Length - TramaDeEnvio.Length);

            
        }


        private void  verificandoSalida()
        {
            //hacerlo mientras el puerto este abierto
            try
            {
                while (puerto.IsOpen)
                {
                    if (puerto.BytesToWrite > 0)
                    {
                        bufferVacio = false;
                    }
                    else
                    {
                        bufferVacio = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public int bytesPorSalir()
        {
            int cantidad = 0;
            if (!bufferVacio)
            {
                cantidad = puerto.BytesToWrite;
                
            }
            return cantidad;
        }


      
    }

    

}
