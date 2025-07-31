using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Project
{
    public partial class btnList : Form
    {
        public btnList()
        {
            InitializeComponent();
        }
        DatabaseEFEntities db = new DatabaseEFEntities();
        private void btnListele_Click(object sender, EventArgs e)
        {
            var values = db.GuideTable.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GuideTable guide = new GuideTable();
            guide.GuideName=txtName.Text;
            guide.GuideSurname=txtSurname.Text;
            db.GuideTable.Add(guide);
            db.SaveChanges();
            MessageBox.Show($"{guide.GuideName} isimli rehber başarıyla eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var removeValue = db.GuideTable.Find(id);
            db.GuideTable.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Rehber başarıyla silindi");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtId.Text);
            var updateValue = db.GuideTable.Find(id);
            updateValue.GuideName=txtName.Text;
            updateValue.GuideSurname= txtSurname.Text;
            db.SaveChanges();
            MessageBox.Show("Rehber başarıyla güncellendi","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            int id =int.Parse(txtId.Text);
            var values = db.GuideTable.Where(x=>x.GuideId==id).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
