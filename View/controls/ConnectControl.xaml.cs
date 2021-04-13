using SimolatorDesktopApp_1.Model;
using SimolatorDesktopApp_1.ViewModel;
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

namespace SimolatorDesktopApp_1.View.controls
{
    /*
     * Class View ConnectControl.
     */
    public partial class ConnectControl : UserControl
    {
        private const string disconnected = "Simulator Disconnected";
        private const string connected = "Simulator Connected";
        VMConnectControl _vmConnectControl;
        Dialog _dialog;

        /*
         * Constructor of ConnectControl
         */
        public ConnectControl()
        {
            InitializeComponent();
            _vmConnectControl = new VMConnectControl((Application.Current as App)._simultorConnectorModel);
            DataContext = _vmConnectControl;
        }

        /*
         * Function for display stustus of connection with the simulator.
         */
        private void connectDisplayStatus()
        {
            if (_vmConnectControl.VM_IsConnected) // check if connected
            {
                this.StatusConnectTextBlock.Text = connected;
                this.StatusConnectTextBlock.Foreground = Brushes.LightGreen;
            }
            else
            {
                this.StatusConnectTextBlock.Text = disconnected;
                this.StatusConnectTextBlock.Foreground = Brushes.Red;
            }
            this.StatusConnectTextBlock.Visibility = Visibility.Visible; // change blockText to visiible
        }

        /*
         * Function that play when connect button selected.
         */
        private void ButtonPressedConnect(object sender, RoutedEventArgs e)
        {
            popUp();
            try
            {
                if (_dialog.getFlag())
                {
                    _vmConnectControl.VMConnect(ipContextTextBox.Text, Int32.Parse(portContextTextBox.Text));
                    ConnectButton.IsEnabled = false;
                    ConnectButton.Visibility = Visibility.Collapsed;
                    DisconnectButton.IsEnabled = true;
                    DisconnectButton.Visibility = Visibility.Visible;
                    this.connectDisplayStatus(); // connect succsess
                }
            }
            catch (Exception _exception)
            {
                this.connectDisplayStatus(); // connect failed
            }
        }

        /*
         * Function that pop up when connect button selected.
         */
        private void popUp()
        {
            _dialog = new Dialog();
            _dialog.ShowDialog();
        }

        /*
         * Function that play when disconnect button is selected.
         */
        private void ButtonPressedDisconnect(object sender, RoutedEventArgs e)
        {
            try
            {
                _vmConnectControl.VMDisconnect();
                DisconnectButton.IsEnabled = false;
                DisconnectButton.Visibility = Visibility.Collapsed;
                ConnectButton.IsEnabled = true;
                ConnectButton.Visibility = Visibility.Visible;
                this.connectDisplayStatus(); // connect succsess
            }
            catch (Exception _exception)
            {
                this.connectDisplayStatus(); // connect failed
            }
        }
    }
}
