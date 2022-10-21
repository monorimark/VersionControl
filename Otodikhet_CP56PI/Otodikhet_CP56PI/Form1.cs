using Otodikhet_CP56PI.MbnServiceReference;
using Otodikhet_CP56PI.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otodikhet_CP56PI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebHivas();
            dataGridView1.DataSource = Rates;
        }
        private void WebHivas()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
        }
        BindingList<RateData> Rates = new BindingList<RateData>();
    }
}
