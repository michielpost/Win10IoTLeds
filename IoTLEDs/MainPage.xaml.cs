using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTLEDs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		static int count = 0;

        static DispatcherTimer timer;
        GpioPin pin1;
        GpioPin pin2;
        GpioPin pin3;
        GpioPin pin4;

		int LED_PIN1 = 5; //5,6,13,26
        int LED_PIN2 = 6; //5,6,13,26
        int LED_PIN3 = 13; //5,6,13,26
        int LED_PIN4 = 26; //5,6,13,26
		bool isOn = true;

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();

            InitGPIO();
        }

        private void Timer_Tick(object sender, object e)
        {
            FlipLED();
        }

        private void FlipLED()
        {

			if (isOn)
			{
				if(count == 0)
					pin2.Write(GpioPinValue.Low);
				if (count == 1)
					pin3.Write(GpioPinValue.Low);
				if (count == 2)
					pin4.Write(GpioPinValue.Low);
				if (count == 3)
					pin1.Write(GpioPinValue.Low);
			}
			else
			{
				if (count == 0)
					pin2.Write(GpioPinValue.High);
				if (count == 1)
					pin3.Write(GpioPinValue.High);
				if (count == 2)
					pin4.Write(GpioPinValue.High);
				if (count == 3)
					pin1.Write(GpioPinValue.High);

			}



			count++;
			if (count > 3)
			{
				count = 0;
				isOn = !isOn;
			}


		}

		private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin1 = null;
                pin2 = null;
                pin3 = null;
				pin4 = null;
                // GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin1 = gpio.OpenPin(LED_PIN1);
            pin2 = gpio.OpenPin(LED_PIN2);
            pin3 = gpio.OpenPin(LED_PIN3);
            pin4 = gpio.OpenPin(LED_PIN4);

            // Show an error if the pin wasn't initialized properly
            if (pin1 == null)
            {
                //GpioStatus.Text = "There were problems initializing the GPIO pin.";
                return;
            }

            pin1.Write(GpioPinValue.High);
            pin2.Write(GpioPinValue.High);
            pin3.Write(GpioPinValue.High);
            pin4.Write(GpioPinValue.High);

            pin1.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            pin3.SetDriveMode(GpioPinDriveMode.Output);
            pin4.SetDriveMode(GpioPinDriveMode.Output);

            //GpioStatus.Text = "GPIO pin initialized correctly.";
        }

        
    }
}

