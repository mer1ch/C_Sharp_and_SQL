using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using DapperTraining.Dtos;

namespace DapperTraining
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Server=DESKTOP-IJKNVTD\\SQLEXPRESS;initial Catalog=DapperDb;integrated security=true");
        private async void btnList_Click(object sender, EventArgs e)
        {
            string query = "Select * From ProductTable";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into ProductTable(ProductName,ProductStock,ProductPrice,ProductCategory) values (@productName,@productStock, @productPrice, @productCategory)";
            var parameters = new DynamicParameters();
            parameters.Add("@productName", txtProductName.Text);
            parameters.Add("@productStock", txtProductStock.Text);
            parameters.Add("@productPrice", txtProductPrice.Text);
            parameters.Add("@productCategory", txtProductCategory.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Yeni Kitap Ekleme Başarılı");
            
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete From ProductTable Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", txtProductId.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Kitap Silme İşlemi Başarılı");
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update ProductTable Set ProductName = @productName,ProductPrice=@productPrice,ProductStock=@productStock,ProductCategory=@productCategory where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productName", txtProductName.Text);
            parameters.Add("@productStock", txtProductStock.Text);
            parameters.Add("@productPrice", txtProductPrice.Text);
            parameters.Add("@productCategory", txtProductCategory.Text);
            parameters.Add("@productId", txtProductId.Text);
            await connection.ExecuteAsync (query, parameters);
            MessageBox.Show("Kitap Güncelleme Başarılı","Güncelleme",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string query1 = "Select Count(*) From ProductTable";
            var productTotalCount = await connection.QueryFirstOrDefaultAsync<int>(query1);
            lblTotalProductCount.Text= productTotalCount.ToString();

            string query2 = "Select ProductName From ProductTable where ProductPrice=(Select Max(ProductPrice) From ProductTable)";
            var maxPriceProductName = await connection.QueryFirstOrDefaultAsync<string>(query2);
            lblMaxPriceProduct.Text = maxPriceProductName.ToString();

            string query3 = "Select Count(Distinct(ProductCategory)) From ProductTable";
            var productCategorySum = await connection.QueryFirstOrDefaultAsync<int>(query3);
            lblProductCategoryCount.Text = productCategorySum.ToString();
        }
    }
}
