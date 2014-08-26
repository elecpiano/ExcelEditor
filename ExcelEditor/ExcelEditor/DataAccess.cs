using System;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.IO;

namespace ExcelEditor
{
    public class DataAccess
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;

        public DataAccess()
        {
            string connStringFormat = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
            string dataFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data.xlsx");
            string connString = string.Format(connStringFormat, dataFilePath);
            Conn = new OleDbConnection(connString);
        }

        /// <summary>
        /// Method to Get All the Records from Excel
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Customer>> GetDataFormExcelAsync()
        {
            ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                Customers.Add(new Customer()
                {
                    //WeChatID = Convert.ToInt32(Reader["wechat_id"]),
                    CustomerName = Reader["customer_name"].ToString(),
                    Email = Reader["email"].ToString(),
                    Phone = Reader["phone"].ToString(),
                    WeChatID = Reader["wechat_id"].ToString(),
                    MediaName = Reader["media_name"].ToString(),
                    City = Reader["city"].ToString(),
                    SelectedStyle = Reader["selected_style"].ToString(),
                });
            }
            Reader.Close();
            Conn.Close();
            return Customers;
        }

        /// <summary>
        /// Method to Insert Record in the Excel
        /// S1. If the EmpNo =0, then the Operation is Skipped.
        /// S2. If the Customer is already exist, then it is taken for Update
        /// </summary>
        /// <param name="customer"></param>
        public async Task InsertOrUpdateRowInExcelAsync(Customer customer)
        {
            bool IsSave = false;
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.Parameters.AddWithValue("@customer_name", customer.CustomerName);
            Cmd.Parameters.AddWithValue("@email", customer.Email);
            Cmd.Parameters.AddWithValue("@phone", customer.Phone);
            Cmd.Parameters.AddWithValue("@wechat_id", customer.WeChatID);
            Cmd.Parameters.AddWithValue("@media_name", customer.MediaName);
            Cmd.Parameters.AddWithValue("@city", customer.City);
            Cmd.Parameters.AddWithValue("@selected_style", customer.SelectedStyle);

            if (true)//!CheckIfRecordExistAsync(customer).Result)
            {
                Cmd.CommandText = "Insert into [Sheet1$] values (@customer_name, @email, @phone, @wechat_id, @media_name, @city, @selected_style)";
            }
            else
            {
                Cmd.CommandText =
@"Update [Sheet1$] set 
customer_name = @customer_name, 
email = @email, 
phone = @phone, 
wechat_id = @wechat_id, 
media_name = @media_name, 
city = @city, 
selected_style = @selected_style 
where wechat_id = @wechat_id";
            }
            int result = await Cmd.ExecuteNonQueryAsync();
            if (result > 0)
            {
                IsSave = true;
            }
            Conn.Close();
        }

        /// <summary>
        /// The method to check if the record is already available 
        /// in the workgroup
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private async Task<bool> CheckIfRecordExistAsync(Customer customer)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where customer_name=@customer_name";
            Cmd.Parameters.AddWithValue("@customer_name", customer.CustomerName);
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }
    }

}
