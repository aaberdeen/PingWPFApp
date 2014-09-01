using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.NetworkInformation;
using System.Timers;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Timer aTimer = new Timer();
        // Property for binding
        public event PropertyChangedEventHandler PropertyChanged;
       
        private string _textaddressvalue;
        public string textAddressValue
        {
            get { return _textaddressvalue; }
            set
            {
                _textaddressvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textAddressValue"));
                }
            }
        }

        private string _textroundtriptimevalue;
        public string textRoundTripTimeValue
        {
            get { return _textroundtriptimevalue; }
            set
            {
                _textroundtriptimevalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textRoundTripTimeValue"));
                }
            }
        }
        private string _texttinetolivevalue;
        public string textTineToLiveValue
        {
            get { return _texttinetolivevalue; }
            set
            {
                _texttinetolivevalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textTineToLiveValue"));
                }
            }
        }
        private string _textdontfragvalue;
        public string textDontFragValue
        {
            get { return _textdontfragvalue; }
            set
            {
                _textdontfragvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textDontFragValue"));
                }
            }
        }
       
        private string _textbuffersizevalue;
        public string textBufferSizeValue
        {
            get { return _textbuffersizevalue; }
            set
            {
                _textbuffersizevalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textBufferSizeValue"));
                }
            }
        }

        private string _texthostipvalue;
        public string textHostIpValue
        {
            get { return _texthostipvalue; }
            set
            {
                _texthostipvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textHostIpValue"));
                }
            }
        }

        private Int32 _textintervalvalue;
        public Int32 textIntervalValue
        {
            get { return _textintervalvalue; }
            set
            {
                _textintervalvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textIntervalValue"));
                }
            }
        }

        private bool _checkBeepValue;
        public bool checkBeepValue
        {
            get
            {return _checkBeepValue;}
            set
            {
                _checkBeepValue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("checkBeepValue"));
                }
            }
        }

        private float _textsentvalue;
        public float textSentValue
        {
            get { return _textsentvalue; }
            set
            {
                _textsentvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textSentValue"));
                }
            }
        }
        private Int32 _textReceivedvalue;
        public Int32 textReceivedValue
        {
            get { return _textReceivedvalue; }
            set
            {
                _textReceivedvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textReceivedValue"));
                }
            }
        }
        private float _textlostvalue;
        public float textLostValue
        {
            get { return _textlostvalue; }
            set
            {
                _textlostvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textLostValue"));
                }
            }
        }
        private float _textpcentvalue;
        public float textPcentValue
        {
            get { return _textpcentvalue; }
            set
            {
                _textpcentvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textPcentValue"));
                }
            }
        }

        private string _textdatavalue;
        public string textDataValue
        {
            get { return _textdatavalue; }
            set
            {
                _textdatavalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("textDataValue"));
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 500;
            textIntervalValue = 500;
            aTimer.Enabled = false;
            DataContext = this;
            textHostIpValue = "192.168.101.103";
            textDataValue = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        }

         private void OnTimedEvent(object source, ElapsedEventArgs e)
     {
         
          Ping pingSender = new Ping();
         PingOptions options = new PingOptions();

         // Use the default Ttl value which is 128, 
         // but change the fragmentation behavior.
         options.DontFragment = true;
         

         // Create a buffer of 32 bytes of data to be transmitted. 
        // string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
         string data = textDataValue;
         byte[] buffer = Encoding.ASCII.GetBytes(data);
         int timeout = 120;
         PingReply reply = pingSender.Send(textHostIpValue, timeout, buffer, options);
         // PingReply reply = pingSender.Send (args[0], timeout, buffer, options);
         textSentValue += 1;
         aTimer.Interval = textIntervalValue;
         if (reply.Status == IPStatus.Success)
         {

             textAddressValue = reply.Address.ToString();
             textRoundTripTimeValue = reply.RoundtripTime.ToString();
             textTineToLiveValue = reply.Options.Ttl.ToString();
             textDontFragValue = reply.Options.DontFragment.ToString();
             textBufferSizeValue = reply.Buffer.Length.ToString();
             
             textReceivedValue += 1;
            
         }
         else
         {
             
             textAddressValue = "fail";
             textLostValue += 1;
             if (checkBeepValue == true)
             {
                 Console.Beep();
             }
            
         }
        textPcentValue = ((textLostValue *100) / textSentValue);

     }
        

        
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Enabled = true;

           

        }
       private void button2_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Enabled = false;
        }


        public void Ping()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(textHostIp.Text, timeout, buffer, options);
            // PingReply reply = pingSender.Send (args[0], timeout, buffer, options);

            if (reply.Status == IPStatus.Success)
            {

                textAddress.Text = reply.Address.ToString();
                textRoundTripTime.Text = reply.RoundtripTime.ToString();
                textTineToLive.Text = reply.Options.Ttl.ToString();
                textDontFrag.Text = reply.Options.DontFragment.ToString();
                textBufferSize.Text = reply.Buffer.Length.ToString();
            }
        }

        private void textHostIp_TextChanged(object sender, TextChangedEventArgs e)
        {
                TextBox txtbox = sender as TextBox;
                BindingExpression bindingExpression = txtbox.GetBindingExpression(TextBox.TextProperty);
                bindingExpression.UpdateSource();
        }

        private void textData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            BindingExpression bindingExpression = txtbox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
        }

        private void textInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            BindingExpression bindingExpression = txtbox.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
        }

     

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textSentValue = 0;
            textReceivedValue = 0;
            textLostValue = 0;
            
        }

        private void checkBoxBeep_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = sender as CheckBox;
            BindingExpression bEx = chBox.GetBindingExpression(CheckBox.IsCheckedProperty);
            bEx.UpdateSource();
        }

        private void checkBoxBeep_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = sender as CheckBox;
            BindingExpression bEx = chBox.GetBindingExpression(CheckBox.IsCheckedProperty);
            bEx.UpdateSource();
        }

 

   



    }
}
