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
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }
        DatabaseEFEntities db = new DatabaseEFEntities();
        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            lblLocationCount.Text = db.LocationTable.Count().ToString();
            lblSumCapacity.Text = db.LocationTable.Sum(x=>x.LocationCapacity).ToString();
            lblGuideCount.Text = db.GuideTable.Count().ToString();
            lblAvgCapacity.Text = db.LocationTable.Average(x => x.LocationCapacity).ToString();
            lblAvgLocationPrice.Text = db.LocationTable.Average(x=>x.LocationPrice).ToString() + "TL";
            int lastCountryId = db.LocationTable.Max(x => x.LocationId);
            lblLastCountryName.Text = db.LocationTable.Where(x => x.LocationId == lastCountryId).Select(y => y.LocationCountry).FirstOrDefault();
            lblCappadociaCapacity.Text=db.LocationTable.Where(x=>x.LocationCity=="Kapadokya").Select(y=>y.LocationCapacity).FirstOrDefault().ToString();
            lblTurkeyAvgCapacity.Text = db.LocationTable.Where(x => x.LocationCountry == "Türkiye").Average(y => y.LocationCapacity).ToString();
            var RomeGuideId = db.LocationTable.Where(x => x.LocationCity == "Roma").Select(y => y.GuideId).FirstOrDefault();
            lblRomeGuideName.Text = db.GuideTable.Where(x => x.GuideId == RomeGuideId).Select(y => y.GuideName + " " + y.GuideSurname).FirstOrDefault().ToString();
            var maxCapacityLocation = db.LocationTable.Max(x => x.LocationCapacity);
            lblMaxCapacityLocation.Text=db.LocationTable.Where(x=>x.LocationCapacity==maxCapacityLocation).Select(y=>y.LocationCity).FirstOrDefault().ToString();
            var maxPrice = db.LocationTable.Max(x => x.LocationPrice);
            lblMaxPriceLocation.Text = db.LocationTable.Where(x => x.LocationPrice == maxPrice).Select(y=>y.LocationCity).FirstOrDefault().ToString();
            lblGuideLocationCount.Text = db.LocationTable.Where(x => x.GuideId == 6).Count().ToString();

        }
    }
}
