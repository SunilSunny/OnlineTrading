using System;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows;
//using System.Windows.;

using System.Collections.ObjectModel;

namespace WindowsFormsApplication1
{
    public class CardDeckViewModel : DependencyObject
    {
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

        public ObservableCollection<Quote> Quotes { get; set; }

        public  CardDeckViewModel()
        {
            Quotes = new ObservableCollection<Quote>();

            //Some example tickers
            Quotes.Add(new Quote("AAPL"));
            Quotes.Add(new Quote("SHOP"));
            Quotes.Add(new Quote("WB"));
            Quotes.Add(new Quote("BABA"));

            //get the data
            YahooStockEngine.Fetch(Quotes);

            //poll every 6 seconds
            timer.Interval = new TimeSpan(0, 0, 30);
            timer.Tick += (o, e) => YahooStockEngine.Fetch(Quotes);

            timer.Start();
        }
    }
}
