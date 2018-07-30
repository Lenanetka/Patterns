using System;
using System.Collections.Generic;
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

/*Observer*/
//Wait on creating log and add new log to textblock

namespace Patterns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private World world;
        private LogBuilder logBuilder;
        public MainWindow()
        {
            InitializeComponent();
            logBuilder = new LogBuilder();
            world = World.Instance();

            world.nextWorldStateEvent += logBuilder.newEra;
            logBuilder.newEraEvent += log;

            world.nextAgeEvent += logBuilder.setCurrentAge;
            world.nextAgeEvent += setAge;

            world.newGovernmentEvent += logBuilder.newGovernment;
            logBuilder.newGovernmentEvent += newGovernment;
            world.newGovernmentEvent += newKingQueen;

            world.newRandomEvent += logBuilder.randomEvent;
            logBuilder.newRandomEvent += randomEvent;

            OnNext += world.nextYear;
        }
        private void log(string message)
        {
            textLog.Text += message + "\n";
        }
        private void setAge(int age)
        {
            labelYear.Content = age + " г. н.э.";
        }
        public void newKingQueen(string king, string queen)
        {
            labelKing.Content = king;
            labelQueen.Content = queen;
        }
        private void newGovernment(string message)
        {
            textLog.Text += message + "\n";
        }
        private void randomEvent(string message)
        {
            textLog.Text += message + "\n";
        }
        public delegate void method();
        public event method OnNext;
        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            OnNext();
        }
    }
}
