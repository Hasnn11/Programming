using System;
using MySql.Data.MySqlClient;

namespace new_database
{
	public class Koneksi
	{
		string alamat ="server=localhost; database=perpus; uid=root ;pwd=";
		public MySqlConnection koneksi;
		
		public void buka()
		 {
		 	koneksi = new MySqlConnection(alamat);
		 	koneksi.Open();
		 }
		 public void tutup()
		 {
		 	koneksi = new MySqlConnection(alamat);
		 	koneksi.Close();
		 }
	}
}