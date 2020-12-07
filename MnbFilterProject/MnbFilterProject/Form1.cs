using MnbFilterProject.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MnbFilterProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateWebService();
        }

        private void CreateWebService()
        {
            var webService = new MNBArfolyamServiceSoapClient();
            //MNBArfolyamServiceSoapClient osztály példányosítása

            var beertek = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2019-01-01",
                endDate = "2020-01-01"
            };
            var visszaertek = webService.GetExchangeRates(beertek);
            var vegertek = visszaertek.GetExchangeRatesResult;
            //vegertek string!!!
        }
    }
}
