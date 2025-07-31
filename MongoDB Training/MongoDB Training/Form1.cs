using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_Training.Entities;
using MongoDB_Training.Services;

namespace MongoDB_Training
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname=txtCustomerSurname.Text,
                CustomerCity=txtCustomerCity.Text,
                CustomerBalance=decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount=int.Parse(txtCustomerShoppingCount.Text)
            };
            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri ekleme başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string customerId=txtCustomerId.Text;
            customerOperations.DeleteCustomer(customerId);
            MessageBox.Show("Müşteri Başarıyla Silindi");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            var updateCustomer = new Customer()
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity=txtCustomerCity.Text,
                CustomerBalance=decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount=int.Parse(txtCustomerShoppingCount.Text),
            };
            customerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("Müşteri Güncelleme Başarılı");
        }

        private void btnGetCustomerById_Click(object sender, EventArgs e)
        {
            string id =txtCustomerId.Text;
            Customer customer = customerOperations.GetCustomerById(id);
            dataGridView1.DataSource = new List<Customer>{customer};
        }
    }
}
