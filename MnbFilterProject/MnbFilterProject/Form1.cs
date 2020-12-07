using MnbFilterProject.Entities;
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
using System.Xml;

namespace MnbFilterProject
{
    public partial class Form1 : Form
    {
        BindingList<Arfolyam> Arfolyamok=new BindingList<Arfolyam>();
        public Form1()
        {
            InitializeComponent();
            CreateWebService();
            dataGridView1.DataSource = Arfolyamok;
        }

        private void CreateWebService()
        {
            var webService = new MNBArfolyamServiceSoapClient();
            //MNBArfolyamServiceSoapClient osztály példányosítása

            var beertek = new GetExchangeRatesRequestBody()
            {
                currencyNames = "DKK",
                startDate = "2019-12-31",
                endDate = "2020-12-31"
            };
            var visszaertek = webService.GetExchangeRates(beertek);
            var vegertek = visszaertek.GetExchangeRatesResult;
            //vegertek string!!!

            var xml = new XmlDocument();
            xml.LoadXml(vegertek);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var jelenarfolyam = new Arfolyam();
                Arfolyamok.Add(jelenarfolyam);

                jelenarfolyam.dátum = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];

                jelenarfolyam.valuta = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    jelenarfolyam.érték = value / unit;
                {

                }


            }
        }
    }
}
