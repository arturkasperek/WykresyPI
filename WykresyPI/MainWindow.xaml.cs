using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WykresyPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        public List<string> impulsowe;
        public List<string> ciagle;
        public MainWindow()
        {
            InitializeComponent();
            impulsowe=new List<string>();
            ciagle= new List<string>();
            impulsowe.Add("wei");
            impulsowe.Add("il");
            impulsowe.Add("wea");
            impulsowe.Add("wes");
            impulsowe.Add("wel");
            impulsowe.Add("weak");


            ciagle.Add("czyt");
            ciagle.Add("pisz");
            ciagle.Add("wys");
            ciagle.Add("weja");
            ciagle.Add("przep");
            ciagle.Add("wyl");
            ciagle.Add("wyak");
            ciagle.Add("dod");
            ciagle.Add("ode");
            ciagle.Add("wyad");
            ciagle.Add("as");
            ciagle.Add("sa");

            

            //Line(10,10,100,100, Color.FromRgb(0,255,0));
            //Text(150, 150, "wysdsfsdfkj", Color.FromRgb(255, 0, 0));
            //Rectangle(50,50,100,100,Color.FromRgb(0,0,255));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            canvasObj.Children.Clear();
            string line;
            List<List<string>> listaRozkazow= new List<List<string>>();
            List<string> pomList = new List<string>();
            for (int x = 0; x < tekst.LineCount; x++)
            {
                line = tekst.GetLineText(x);
                int pom = 1;
                string sample="";

                for (int y = 0; y < line.Length; y++)
                {
                    if(line[y]=='\n'||line[y]=='\r')
                    {
                        if(pom==0)pomList.Add(sample);
                        sample = "";
                        break;
                    }
                    if (pom == 1 && line[y] != ' ' && line[y] != ','&& line[y]!='\0') pom = 0;
                    if (pom == 0)//czytamy wyraz
                    {
                        if (line[y] == ' ' || line[y] == ',' && line[y] != '\0')
                        {
                            pomList.Add(sample);
                            sample = "";
                            pom = 1;
                        }
                        else
                        {
                            sample += line[y];
                        }
                    }
                    if (pom == 0 && y == line.Length - 1)
                    {
                        pomList.Add(sample);
                        sample = "";
                        pom = 1;
                    }
                }
                listaRozkazow.Add(new List<string>(pomList));
                pomList.Clear();
            }
            List<string> doNarysowania= new List<string>();
            foreach (List<string> variable in listaRozkazow)
            {
                foreach (string variable2 in variable)
                {
                    //string pom2 = new string(variable2);
                    if (doNarysowania.Contains(variable2)==false)
                    {
                        doNarysowania.Add(variable2);
                    }

                }
            }
            var positionDictionary = new Dictionary<string, int>();
            int yOffSet = 20;
            foreach (var variable in doNarysowania)
            {
                Line(0,yOffSet+5,500,yOffSet+5,Color.FromRgb(255,200,30));
                Text(10,yOffSet,variable,Color.FromRgb(255,100,0));
                positionDictionary[variable] = yOffSet;
                yOffSet += 25;
            }
            Line(0,yOffSet+5,500,yOffSet+5,Color.FromRgb(255,200,30));
            int xOffSet = 70;
            //yOffSet = 20;
            Text(2, 5, "Takty:", Color.FromRgb(254, 12, 200));
            int pomvar = 1;
            foreach (var variable in listaRozkazow)
            {
                Line(xOffSet,20,xOffSet,400,Color.FromRgb(100,255,55));
                Text(xOffSet,5,"f"+pomvar.ToString(),Color.FromRgb(254,12,200));
                foreach (var variable2 in variable)
                {
                    if (ciagle.Contains(variable2))
                    {
                        Rectangle(xOffSet-30,positionDictionary[variable2]+15,xOffSet,positionDictionary[variable2]+25,Color.FromRgb(30,34,123));
                    }
                    if (impulsowe.Contains(variable2))
                    {
                        Rectangle(xOffSet -10, positionDictionary[variable2]+15, xOffSet,positionDictionary[variable2]+25, Color.FromRgb(40, 120, 23));
                    }
                }
                pomvar++;
                xOffSet += 80;
            }
        }
        /*
czyt, wys, wei, il
wyad, wea
czyt, wys, weja, przep, weak
wyl, wea
         */
        private void Line(double x1, double y1, double x2, double y2,

                          Color color)
        {

            Line lineObj = new Line();

            lineObj.Stroke = new SolidColorBrush(color);

            lineObj.X1 = x1;

            lineObj.X2 = x2;

            lineObj.Y1 = y1;

            lineObj.Y2 = y2;

            canvasObj.Children.Add(lineObj);

        }

        private void Rectangle(double x1, double y1, double x2, double y2,Color color)
        {
            Rectangle rectangleObj= new Rectangle();
            rectangleObj.Stroke= new SolidColorBrush(color);
            rectangleObj.Fill= new SolidColorBrush(color);
            rectangleObj.Width = x2 - x1;
            rectangleObj.Height = y2 - y1;
            rectangleObj.StrokeThickness = 2;
            Canvas.SetLeft(rectangleObj,x1);
            Canvas.SetTop(rectangleObj,y1);
            canvasObj.Children.Add(rectangleObj);
        }

        private void Text(double x, double y, string text,

                          Color color)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            canvasObj.Children.Add(textBlock);

        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(canvasObj);
            double dpi = 96d;


            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);


            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(canvasObj);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Image"; // Default file name
                dlg.DefaultExt = ".png"; // Default file extension
                dlg.Filter = "Image documents (.png)|*.png"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    pngEncoder.Save(ms);
                    ms.Close();

                    System.IO.File.WriteAllBytes(filename, ms.ToArray());
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
