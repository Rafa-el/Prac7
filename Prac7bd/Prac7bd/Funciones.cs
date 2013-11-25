using System;
using MySql.Data.MySqlClient;
namespace Prac7bd
{
	public class Funciones : Conexion
	{
		public Funciones ()
		{
		}
		
		public void mostrarTodos(){
			this.abrirConexion();
            MySqlCommand myCommand = new MySqlCommand(this.querySelect(), 
			                                          myConnection);
            MySqlDataReader myReader = myCommand.ExecuteReader();	
	        while (myReader.Read()){
	            string id = myReader["id"].ToString();
	            string nombre = myReader["nombre"].ToString();
	            string codigo = myReader["codigo"].ToString();
				string telefono = myReader["telefono"].ToString();
				string email = myReader["email"].ToString();

	            Console.WriteLine("ID: " + id +
				                  " Nombre: " + nombre +
				                  " Código: " + codigo + 
				                  " Telefono: " + telefono +
				                  " Email: " + email);
	       }
			
            myReader.Close();
			myReader = null;
            myCommand.Dispose();
			myCommand = null;
			this.cerrarConexion();
		}
		
		public void insertarRegistroNuevo(string nombre, string codigo, string telefono, string email){
			this.abrirConexion();
			string sql = "INSERT INTO tabla (nombre, codigo, telefono, email) VALUES ('" + nombre + "', '" + codigo + "', '" +
														telefono + "', '" +	email + "')";
			this.ejecutarComando(sql);
			this.cerrarConexion();
		}

		private int ejecutarComando(string sql){
			MySqlCommand myCommand = new MySqlCommand(sql,this.myConnection);
			int afectadas = myCommand.ExecuteNonQuery();
			myCommand.Dispose();
			myCommand = null;
			return afectadas;
		}
		
		private string querySelect(){
			return "SELECT * " +
	           	"FROM tabla";
		}

		public void Menu ()
		{
			int opc;
			string nom, cod, tel, email;
			do {
				Console.WriteLine("Presione una tecla...");
				Console.ReadKey();
				Console.Clear();
				Console.WriteLine ("Menu");
				Console.WriteLine ("1. Mostrar todos");
				Console.WriteLine ("2. Agregar nuevo registro");
				Console.WriteLine ("3. Salir");
				Console.WriteLine ("Ingrese una opcion: ");
				opc = Convert.ToInt16 (Console.ReadLine ());

				switch(opc){
					case 1:
						this.mostrarTodos();
						break;
					case 2:
						Console.WriteLine("Ingrese Nombre: "); nom=Console.ReadLine();
						Console.WriteLine("Ingrese Codigo: "); cod=Console.ReadLine();
						Console.WriteLine("Ingrese Telefono: "); tel=Console.ReadLine();
						Console.WriteLine("Ingrese Email: "); email=Console.ReadLine();
						this.insertarRegistroNuevo(nom, cod, tel, email);
						break;
				}
			} while(opc != 3);
		}
	}
}