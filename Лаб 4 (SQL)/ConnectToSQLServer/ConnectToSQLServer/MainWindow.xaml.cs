using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Controls;

namespace ConnectToSQLServer
{
    public partial class MainWindow : Window
    {
        string connectionString = null;        
        SqlConnection connection = null;
        SqlCommand command;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //MessageBox.Show(connectionString);
            GetPerformersData();
            //GetMusicansData();
            //GetTradeData();
            GetRecordsData();
        }

        private void GetAndDhowData(string SQLQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand(SQLQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGrid.ItemsSource = Table.DefaultView;
            connection.Close();            
        }

        private void GetPerformersData()
        {
            string sqlQ = "SELECT TOP(20) Name as [Назва], ThisYearSales as [Продажів за рік]" +
                " From Records R FULL JOIN Trade T " +
                "ON R.IDRecords = T.IDRecords ORDER BY ThisYearSales DESC";
            try
            {
                GetAndDhowData(sqlQ, PerformersDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /*private void GetMusicansData()
        {
            string sqlQ = "";
            try
            {
                GetAndDhowData(sqlQ, MusicansDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }*/

       /*private void GetTradeData()
        {
            string sqlQ = "";
            try
            {
                GetAndDhowData(sqlQ, TradeDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }*/

        private void GetRecordsData()
        {
            string sqlQ = "SELECT Performer as [Виконавець], Name as [Назва Альбому] From Records WHERE Performer = 'Nirvana'; ";
            try
            {
                GetAndDhowData(sqlQ, RecordsDG);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}