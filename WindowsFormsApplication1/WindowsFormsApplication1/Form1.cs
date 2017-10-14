using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        // This DataGridView control displays the contents of the list.
       // private DataGridView customersDataGridView = new DataGridView();

        // This BindingSource binds the list to the DataGridView control.
        private BindingSource customersBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
         

            // Set up the DataGridView.
            customersDataGridView.Dock = DockStyle.Fill;
            this.Controls.Add(customersDataGridView);

            this.Size = new System.Drawing.Size(800, 800);
        }

        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
        public ObservableCollection<Quote> Quotes { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            BindingList<Quote> customerList = new BindingList<Quote>();
            Quotes = new ObservableCollection<Quote>();

            //Some example tickers
            Quotes.Add(new Quote("AAPL"));
            Quotes.Add(new Quote("SHOP"));
            Quotes.Add(new Quote("WB"));
            Quotes.Add(new Quote("BABA"));

            //get the data
            YahooStockEngine.Fetch(Quotes);

            // Bind the list to the BindingSource.
            this.customersBindingSource.DataSource = Quotes;

            // Attach the BindingSource to the DataGridView.
            this.customersDataGridView.DataSource =
                this.customersBindingSource;
            //poll every 6 seconds
            timer.Interval = new TimeSpan(0, 0,25);
            timer.Tick += (o, u) => YahooStockEngine.Fetch(Quotes);

            timer.Start();

        }
    }
}
