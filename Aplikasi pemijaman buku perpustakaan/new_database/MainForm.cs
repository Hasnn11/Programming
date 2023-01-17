using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace new_database
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		MySqlCommand query;
		Koneksi sambung;
		MySqlDataAdapter adapter;
		string sql;
		DataTable tabel;
		DataSet ds;
		
		public MainForm()
		{
			InitializeComponent();
			TampilDalamTabel();
		}
		
		void TampilDalamTabel(){
			ViewData tampilkan = new ViewData();
			DataTable tabel = new DataTable();
			
			tabel = tampilkan.bacasemua();
			dataGridView1.DataSource = tabel;
		}
		
		public class ViewData
		{
			MySqlCommand query;
			Koneksi sambung;
			MySqlDataAdapter adapter;
			string sql;
			DataTable tabel;
			
			public DataTable bacasemua()
			{
				sambung = new Koneksi();
				sql = "SELECT * FROM pinjam";
				tabel = new DataTable();
			try
			{
				sambung.buka();
				query = new MySqlCommand(sql,sambung.koneksi);
				adapter = new MySqlDataAdapter(query);
				query.ExecuteNonQuery();
				adapter.Fill(tabel);
			}
			catch (Exception er)
			{
				MessageBox.Show(er.Message);
			}
			sambung.tutup();
			return tabel;
			}
		}
		
		public void bersihkan()
		{
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
			textBox4.Text = "";
			textBox5.Text = "";
			textBox6.Text = "";
			dateTimePicker1 = null;
			dateTimePicker2 = null;
		}
		
		public void simpan()
		{
			sambung = new Koneksi();
			sql = "insert into pinjam (kd_peminjaman, nama, no_hp, alamat, nama_buku, tanggal_pinjam, tanggal_kembali) " +
				"VALUES ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"'," +
				"'"+textBox5.Text+"','"+dateTimePicker1.Text + "','"+dateTimePicker2.Text+ "')";
		try
		{
			sambung.buka();
			query = new MySqlCommand(sql,sambung.koneksi);
			adapter = new MySqlDataAdapter(query);
			query.ExecuteNonQuery();
			TampilDalamTabel();
			dataGridView1.Refresh();
			MessageBox.Show("Data Berhasil Tersimpan !!!", "Information");
		}
		catch (Exception er)
		{
			MessageBox.Show(er.Message);
		}
			sambung.tutup();
		}
		
		public void edit()
		{
			sambung = new Koneksi();
			sql = "update pinjam set nama='"+ textBox2.Text + "',no_hp='"+ textBox3.Text + "'," +
				"alamat='"+ textBox4.Text + "',nama_buku='"+ textBox5.Text + "',tanggal_pinjam='"+ dateTimePicker1.Text + "'," +
				"tanggal_kembali='"+ dateTimePicker2.Text +"' where kd_peminjaman='"+ textBox1.Text +"'";
		try
		{
			sambung.buka();
			query = new MySqlCommand(sql,sambung.koneksi);
			adapter = new MySqlDataAdapter(query);
			query.ExecuteNonQuery();
			TampilDalamTabel();
			dataGridView1.Refresh();
			MessageBox.Show("Data Berhasil Terupdate !!!", "Information");
		}
		catch (Exception er)
		{
			MessageBox.Show(er.Message);
		}
			sambung.tutup();
		}
		
		public void hapus()
		{
			if (MessageBox.Show("Yakin akan menghapus data Peminjaman : "+ textBox2.Text +" ?","Konfirmasi",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
			{
				sambung = new Koneksi();
				sql = "delete from pinjam where kd_peminjaman='"+ textBox1.Text +"'";
			{
				sambung.buka();
				query = new MySqlCommand(sql,sambung.koneksi);
				adapter = new MySqlDataAdapter(query);
				query.ExecuteNonQuery();
				TampilDalamTabel();
				dataGridView1.Refresh();
				MessageBox.Show("Data Berhasil Terhapus !!!", "Information");
			}
			}
		}
		
		public void cari()
		{
			sambung= new Koneksi();
			sql= "Select * from pinjam where kd_peminjaman like '%"+ textBox6.Text+"%' or nama like '%"+ textBox6.Text+"%'";		
		try
		{
				sambung.buka();
				query= new MySqlCommand(sql,sambung.koneksi);
				ds = new DataSet();
				adapter = new MySqlDataAdapter(query);
				adapter.Fill(ds,"pinjam");
				query.ExecuteNonQuery();
				dataGridView1.Refresh();
				dataGridView1.DataSource=ds;
				dataGridView1.DataMember="pinjam";
					
		}
		catch(Exception er)
		{
			MessageBox.Show(er.Message);
		}
			sambung.tutup();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			simpan();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			edit();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			hapus();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			bersihkan();
		}
				
		void TextBox6TextChanged(object sender, EventArgs e)
		{
			cari();
		}
		
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{

				DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
				textBox1.Text = row.Cells["kd_peminjaman"].Value.ToString();
				textBox2.Text = row.Cells["nama"].Value.ToString();
				textBox3.Text = row.Cells["no_hp"].Value.ToString();
				textBox4.Text = row.Cells["alamat"].Value.ToString();
				textBox5.Text = row.Cells["nama_buku"].Value.ToString();
				dateTimePicker1.Text = row.Cells["tanggal_pinjam"].Value.ToString();
				dateTimePicker2.Text = row.Cells["tanggal_kembali"].Value.ToString();
			
		}
	}
}