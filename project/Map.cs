using System;
using System.Drawing;
using System.Windows.Forms;
using ApiHelp;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Threading;
using System.Globalization;
using Gmap_Markers;


namespace project
{
    public partial class Map : Form
    {
        public double lat;
        public double lng;
        public Boolean vis = true;

        public Map()
        {
            InitializeComponent();
        }
        private bool isLeftButtonDown = false;
        private System.Windows.Forms.Timer blinkTimer = new System.Windows.Forms.Timer();
        private Gmap_Markers.GMapMarkerImage currentMarker;
        private GMap.NET.WindowsForms.GMapOverlay markersOverlay;
        private void Map_Load(object sender, EventArgs e)
        {
            button1.Visible = vis;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.GrayScaleMode = true;
            gMapControl1.MarkersEnabled = true;
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Dock = DockStyle.Fill;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            GMap.NET.MapProviders.GMapProvider.WebProxy = System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance; 
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 16;
            gMapControl1.Zoom = 4;
            gMapControl1.Position = new GMap.NET.PointLatLng(66.4169575018027, 94.25025752215694);
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControl1.CanDragMap = true;
            gMapControl1.ShowCenter = false;
            gMapControl1.ShowTileGridLines = false;
            markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("marker");
            if (lat != 0)
            {
                gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
                gMapControl1.Zoom = 10;
            }
            gMapControl1.OnMapZoomChanged += new MapZoomChanged(mapControl_OnMapZoomChanged);
            gMapControl1.MouseClick += new MouseEventHandler(mapControl_MouseClick);
            gMapControl1.MouseDown += new MouseEventHandler(mapControl_MouseDown);
            gMapControl1.MouseUp += new MouseEventHandler(mapControl_MouseUp);
            gMapControl1.MouseMove += new MouseEventHandler(mapControl_MouseMove);
            gMapControl1.OnMarkerClick +=new MarkerClick(mapControl_OnMarkerClick);
            gMapControl1.OnMarkerEnter += new MarkerEnter(mapControl_OnMarkerEnter);
            gMapControl1.OnMarkerLeave += new MarkerLeave(mapControl_OnMarkerLeave);
            gMapControl1.Overlays.Add(markersOverlay);
        }
        void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown)
            {
                if (currentMarker != null)
                {
                    PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                    currentMarker.Position = point;
                    currentMarker.ToolTipText = string.Format("{0},{1}", point.Lat, point.Lng);
                }
            }
        }

        void mapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isLeftButtonDown = false;
            }
        }

        void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isLeftButtonDown = true;
            }
        }
        void mapControl_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerImage)
            {
                currentMarker = null;
                GMapMarkerImage m = item as GMapMarkerImage;
                m.Pen.Dispose();
                m.Pen = null;
            }
        }
        void mapControl_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerImage)
            {
                currentMarker = item as GMapMarkerImage;
                currentMarker.Pen = new Pen(Brushes.Red, 2);
            }
        }

        void mapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {

        }

        void mapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                markersOverlay.Markers.Clear();
                PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                Bitmap bitmap = Bitmap.FromFile(Application.StartupPath + @"\marker.png") as Bitmap;
                GMapMarker marker = new GMapMarkerImage(point, bitmap);
                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                marker.ToolTipText = string.Format("{0},{1}", point.Lat, point.Lng);
                string ct = "Туда не летят самолёты";
                CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                double rate = 0.01;
                foreach (var item in ApiH.cities)
                {
                    double lat = double.Parse(item.lat);
                    double lng = double.Parse(item.lng);
                    if (lat > point.Lat- rate && lat <point.Lat+ rate && lng > point.Lng - rate && lng < point.Lng + rate)
                    {
                        ct = item.name;
                    }
                }
                if(ct == "Туда не летят самолёты")
                {
                    rate = 0.05;
                    foreach (var item in ApiH.cities)
                    {
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        if (lat > point.Lat - rate && lat < point.Lat + rate && lng > point.Lng - rate && lng < point.Lng + rate)
                        {
                            ct = item.name;
                        }
                    }
                }
                if (ct == "Туда не летят самолёты")
                {
                    rate = 0.1;
                    foreach (var item in ApiH.cities)
                    {
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        if (lat > point.Lat - rate && lat < point.Lat + rate && lng > point.Lng - rate && lng < point.Lng + rate)
                        {
                            ct = item.name;
                        }
                    }
                }
                label1.Text = ct;
                markersOverlay.Markers.Add(marker);
            }
        }
        void mapControl_OnMapZoomChanged()
        {

        }

        private void buttonBeginBlink_Click(object sender, EventArgs e)
        {
            blinkTimer.Interval = 1000;
            blinkTimer.Tick += new EventHandler(blinkTimer_Tick);
            blinkTimer.Start();
        }

        void blinkTimer_Tick(object sender, EventArgs e)
        {
            foreach (GMapMarker m in markersOverlay.Markers)
            {
                if (m is GMapMarkerImage)
                {
                    GMapMarkerImage marker = m as GMapMarkerImage;
                    if (marker.OutPen == null)
                        marker.OutPen = new Pen(Brushes.Red, 2);
                    else
                    {
                        marker.OutPen.Dispose();
                        marker.OutPen = null;
                    }
                }
            }
            gMapControl1.Refresh();
        }

        private void buttonStopBlink_Click(object sender, EventArgs e)
        {
            blinkTimer.Stop();
            foreach (GMapMarker m in markersOverlay.Markers)
            {
                if (m is GMapMarkerImage)
                {
                    GMapMarkerImage marker =
                        m as GMapMarkerImage;
                    marker.OutPen.Dispose();
                    marker.OutPen = null;
                }
            }
            gMapControl1.Refresh();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox111.Text != "")
            {
                foreach (var item in ApiH.cities)
                {
                    if (item.name == textBox111.Text)
                    {
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
                        gMapControl1.Zoom = 10;
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(label1.Text!= "Туда не летят самолёты")
            {
                GMapMarkerImage.City = label1.Text;
            }
        }
    }
}
