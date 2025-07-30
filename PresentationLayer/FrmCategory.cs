using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace PresentationLayer
{
    public partial class FrmCategory : Form
    {
        private readonly ICategoryService _categoryService;
        public FrmCategory ()
        {
            _categoryService = new CategoryManager(new EFCategoryDAL());
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            var categoryValues = _categoryService.TGetAll();
            dataGridView1.DataSource = categoryValues;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.CategoryName = txtCategoryName.Text;
            category.CategoryStatus = true;
            _categoryService.TInsert(category);
            MessageBox.Show("Ekleme Başarılı");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var deleteValues = _categoryService.TGetById(id);
            _categoryService.TDelete(deleteValues);
            MessageBox.Show("Silme Başarılı");
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var values = _categoryService.TGetById(id);
            dataGridView1 .DataSource = values;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool status;
            if (rdbActive.Checked)
            {
                status = true;
            }
            else
            {
                status= false;
            }
                int updateId = int.Parse(txtCategoryId.Text);
            var updateValue = _categoryService.TGetById(updateId);
            if (txtCategoryName.Text != "")
            {
                updateValue.CategoryName = txtCategoryName.Text;
            }
                updateValue.CategoryStatus = status;
            _categoryService.TUpdate(updateValue);
        }
    }
}
