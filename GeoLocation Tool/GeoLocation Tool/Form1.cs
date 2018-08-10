using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Net;
using System.Diagnostics;
using System.Web;
namespace DXApplication7
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            LoadTheMap();
        }
        string YourIP = new WebClient().DownloadString("http://icanhazip.com");
        public static string ISVPN(string IP, string ISP, string AS)
        {
            try
            {
                string[] MEME = new string[] {
                    "Nuclearfallout Enterprises",
                    "Nuclearfallout",
                    "Internap Network Services Corporation",
                    "Internap Network",
                    "Bandcon",
                    "OVH Hosting",
                    "OVH SAS",
                    "AS16276 OVH SAS",
                    "OVH",
                    "Optus",
                    "Ziggo",
                    "IPVanish",
                    "Digital Ocean",
                    "Vodafone Australia",
                    "Vodafone",
                    "PureVPN",
                    "Eweka Internet Services B.V.",
                    "Keminet Ltd.",
                    "EDIS GmbH",
                    "Cooolbox",
                    "VIPnet d.o.o.",
                    "Virgin Mobile",
                    "Virgin",
                    "Secure Internet LLC",
                    "Co.pa.co.",
                    "SingleHop"};
                if (MEME.Contains<string>(ISP))
                {
                    return "VPN";
                }
                if (AS.Contains("AS4804 Microplex PTY LTD") || AS.Contains("AS9500 Vodafone NZ Ltd.") || AS.Contains("T-Mobile USA"))
                {
                    return "Mobile Hotspot";
                }
                return "Broadband Connection";
            }
            catch (Exception E)
            {
                return "Error ," + E.ToString();
            }
        }
        private void LoadTheMap()
        {
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            gMapControl1.Manager.Mode = AccessMode.ServerOnly;
            GMap.NET.MapProviders.GMapProvider.WebProxy = null;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CenterPen = new Pen(Color.Green, 2);
            gMapControl1.MinZoom = trackBarControl1.Properties.Minimum = 1;
            gMapControl1.MaxZoom = trackBarControl1.Properties.Maximum = 20;
            trackBarControl1.Value = 10;
            gMapControl1.Zoom = trackBarControl1.Value;
        }
        public static string[] IPLocation(string IP)
        {
            StringBuilder output = new StringBuilder();
            XmlReader xmlReader = XmlReader.Create((TextReader)new StringReader(new WebClient { Proxy = null }.DownloadString(("http://ip-api.com/xml/" + IP))));
            using (XmlWriter.Create(output, new XmlWriterSettings() { Indent = true }))
            {
                try
                {
                    xmlReader.ReadToFollowing("country");
                    string str1 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("regionName");
                    string str2 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("city");
                    string str3 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("zip");
                    string str4 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("timezone");
                    string str5 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("isp");
                    string str6 = xmlReader.ReadElementContentAsString();
                    xmlReader.ReadToFollowing("as");
                    string str7 = xmlReader.ReadElementContentAsString();
                    string str8 = ISVPN(IP, str6, str7);
                    return new string[] { str1, str2, str3, str4, str5, str6, str7, str8 };
                }
                catch (Exception e)
                {
                    return new string[] { "N/A", "N/A", "N/A", "N/A", "N/A", "N/A", "N/A", "N/A" };
                }
            }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string[] MEME = IPLocation(textEdit7.Text);
            textEdit3.Text = textEdit7.Text;
            textEdit4.Text = MEME[0];
            textEdit6.Text = MEME[1];
            textEdit5.Text = MEME[2];
            textEdit8.Text = MEME[3];
            textEdit9.Text = MEME[7];
            textEdit10.Text = MEME[5];
            gMapControl1.SetPositionByKeywords(MEME[2]);
            gMapControl1.Zoom = 15;
            double X = Math.Round(gMapControl1.Position.Lng, 6);
            double Y = Math.Round(gMapControl1.Position.Lat, 6);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] MEME = IPLocation(YourIP);
            textEdit3.Text = YourIP;
            textEdit4.Text = MEME[0];
            textEdit6.Text = MEME[1];
            textEdit5.Text = MEME[2];
            textEdit8.Text = MEME[3];
            textEdit9.Text = MEME[7];
            textEdit10.Text = MEME[5];
            gMapControl1.SetPositionByKeywords(MEME[2]);
            gMapControl1.Zoom = 15;
            double X = Math.Round(gMapControl1.Position.Lng, 6);
            double Y = Math.Round(gMapControl1.Position.Lat, 6);
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackBarControl1.Value;
        }
    }
}
