/************************************************************************************************
 * This wrapper class was written by Daniel J Riches for Gordon Hendersons WiringPi C library   *
 * I take no responsibility for this wrapper class providing proper functionality and give no   *
 * warranty of any kind, nor it's use or fitness for any purpose. You use this wrapper at your  *
 * own risk.                                                                                    *
 *                                                                                              *
 * This code is released as Open Source under GNU GPL license, please ensure that you have a    *
 * copy of the license and understand the usage terms and conditions.                           *
 *                                                                                              *
 * I take no credit for the underlying functionality that this wrapper provides.                *
 * Authored: 29/04/2013                                                                         *
 ************************************************************************************************
 * Changelog
 * Date         Changed By          Details of change
 * 08 May 2013  Daniel Riches       Corrected c library mappings for I2C and SPI, added this header
 * 
 ************************************************************************************************
 * Changelog
 * Date         Changed By          Details of change  
 * 23 Nov 2013  Gerhard de Clercq   Changed digitalread to return int and implemented wiringPiISR
 *  
 ************************************************************************************************
 * Changelog
 * Date         Changed By          Details of change  
 * 01 Dec 2014  Marek Krzyzowski    Changed:
 *                                  Add: Init.wiringPiSetupPhys
 *                                  Change: class GPIO, now pin number corresponds used WiringPiSetupXXX,
 *                                  modified/extended enums (GpioPinMode,PullMode), PWM function moved to
 *                                  PWM class.
 *                                  Add: PWM class
 *                                  Add: Timing.micros
 *                                  Change: PiThreadInterrupts enum InterruptMode
 *                                  Add: MiscFunctions.physPinToGpio, MiscFunctions.getAlt
 * 
 ************************************************************************************************/
using System.Runtime.InteropServices;

namespace WiringPi
{
    /// <summary>
    /// Used to initialise Gordon's library, there's 3 different ways to initialise and we're going to support all 3
    /// </summary>
    public class Init
    {
        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetup")]     //This is an example of how to call a method / function in a c library from c#
        public static extern int WiringPiSetup();

        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupGpio")]
        public static extern int WiringPiSetupGpio();

        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupPhys")]
        public static extern int wiringPiSetupPhys();

        [DllImport("libwiringPi.so", EntryPoint = "wiringPiSetupSys")]
        public static extern int WiringPiSetupSys();
    }

    /// <summary>
    /// Used to configure a GPIO pin's direction and provide read & write functions to a GPIO pin
    /// </summary>
    public class GPIO
    {

        public enum GpioPinDigitalValue
        {
            Low = 0,
            High = 1
        }

        public enum PullMode
        {
            PudOff = 0,
            PudDown = 1,
            PudUp = 2
        }

        public enum GpioPinMode
        {
            Input = 0,
            Output = 1,
            PwmOutput = 2,
            GpioClock = 3,
            SoftPwmOutput = 4,
            SoftToneOutput = 5,
            PwmToneOutput = 6
        }

        //PIN MODE READ WRITE PULLCONTROL
        //extern void pinModeAlt(int pin, int mode);
        [DllImport("libwiringPi.so", EntryPoint = "pinModeAlt")]
        public static extern void pinModeAlt(int pin, GpioPinMode mode);

        //extern void pinMode(int pin, int mode);
        [DllImport("libwiringPi.so", EntryPoint = "pinMode")]
        public static extern void pinMode(int pin, GpioPinMode mode);

        //extern void pullUpDnControl(int pin, int pud);
        [DllImport("libwiringPi.so", EntryPoint = "pullUpDnControl")]
        public static extern void pullUpDnControl(int pin, PullMode pud);

        //extern int digitalRead(int pin);
        [DllImport("libwiringPi.so", EntryPoint = "digitalRead")]
        public static extern int digitalRead(int pin);

        //extern void digitalWrite(int pin, int value);
        [DllImport("libwiringPi.so", EntryPoint = "digitalWrite")]
        public static extern void digitalWrite(int pin, GpioPinDigitalValue value);

        //extern int analogRead(int pin);
        [DllImport("libwiringPi.so", EntryPoint = "analogRead")]
        public static extern int analogRead(int pin);

        [DllImport("libwiringPi.so", EntryPoint = "analogWrite")]
        public static extern void analogWrite(int pin, int value);

        [DllImport("libwiringPi.so", EntryPoint = "digitalWriteByte")]
        public static extern void digitalWriteByte(int value);

        [DllImport("libwiringPi.so", EntryPoint = "gpioClockSet")]
        public static extern void gpioClockSet(int pin, int freq);
    }

    /// <summary>
    /// Provides use of the Timing functions such as delays
    /// </summary>
    public class Timing
    {
        [DllImport("libwiringPi.so", EntryPoint = "millis")] //ms
        public static extern uint millis();

        [DllImport("libwiringPi.so", EntryPoint = "micros")] //us
        public static extern uint micros();

        [DllImport("libwiringPi.so", EntryPoint = "delay")] //Delay in ms
        public static extern void delay(uint howLong);

        [DllImport("libwiringPi.so", EntryPoint = "delayMicroseconds")] //Delay in us
        public static extern void delayMicroseconds(uint howLong);
    }

    /// <summary>
    /// Provides access to the Thread priority and interrupts for IO
    /// </summary>
    public class PiThreadInterrupts
    {
        public enum InterruptMode
        {
            IntEdgeSetup = 0,
            IntEdgeFalling = 1,
            IntEdgeRising = 2,
            IntEdgeBoth = 3,
        }

        [DllImport("libwiringPi.so", EntryPoint = "piHiPri")]
        public static extern int PiHiPri(int priority);

        [DllImport("libwiringPi.so", EntryPoint = "waitForInterrupt")]
        public static extern int waitForInterrupt(int pin, int timeout);

        //This is the C# equivelant to "void (*function)(void))" required by libwiringPi to define a callback method
        public delegate void ISRCallback();

        [DllImport("libwiringPi.so", EntryPoint = "wiringPiISR")]
        public static extern int wiringPiISR(int pin, InterruptMode mode, ISRCallback method);

        //TODO
        //static extern int piThreadCreate(string name);
    }

    public class MiscFunctions
    {
        //UTILS
        [DllImport("libwiringPi.so", EntryPoint = "piBoardRev")]
        public static extern int piBoardRev();

        [DllImport("libwiringPi.so", EntryPoint = "wpiPinToGpio")]
        public static extern int wpiPinToGpio(int wPiPin);

        [DllImport("libwiringPi.so", EntryPoint = "setPadDrive")]
        public static extern int setPadDrive(int group, int value);
        
        [DllImport("libwiringPi.so", EntryPoint = "physPinToGpio")]
        public static extern int physPinToGpio(int physPin);
        
        [DllImport("libwiringPi.so", EntryPoint = "getAlt")]
        public static extern int getAlt(int pin);

        //TODO
        //extern void piBoardId           (int *model, int *rev, int *mem, int *maker, int *overVolted) ;
    }

    /// <summary>
    /// Provides use of the PWM functions
    /// </summary>
    public class PWM
    {
        //PWM
        [DllImport("libwiringPi.so", EntryPoint = "pwmWrite")]
        public static extern void pwmWrite(int pin, int value);

        [DllImport("libwiringPi.so", EntryPoint = "pwmSetMode")]
        public static extern void pwmSetMode(int mode);

        [DllImport("libwiringPi.so", EntryPoint = "pwmSetRange")]
        public static extern void pwmSetRange(uint range);

        [DllImport("libwiringPi.so", EntryPoint = "pwmSetClock")]
        public static extern void pwmSetClock(int divisor);

        [DllImport("libwiringPi.so", EntryPoint = "pwmToneWrite")]
        public static extern void pwmToneWrite(int pin, int freq);
    }

    /// <summary>
    /// Provides SPI port functionality
    /// </summary>
    public class SPI
    {
        /// <summary>
        /// Configures the SPI channel specified on the Raspberry Pi
        /// </summary>
        /// <param name="channel">Selects either Channel 0 or 1 for use</param>
        /// <param name="speed">Selects speed, 500,000 to 32,000,000</param>
        /// <returns>-1 for an error, or the linux file descriptor the channel uses</returns>
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPISetup")]
        public static extern int wiringPiSPISetup(int channel, int speed);

        /// <summary>
        /// Read and Write data over the SPI bus, don't forget to configure it first
        /// </summary>
        /// <param name="channel">Selects Channel 0 or Channel 1 for this operation</param>
        /// <param name="data">signed byte array pointer which holds the data to send and will then hold the received data</param>
        /// <param name="len">How many bytes to write and read</param>
        /// <returns>-1 for an error, or the linux file descriptor the channel uses</returns>
        [DllImport("libwiringPiSPI.so", EntryPoint = "wiringPiSPIDataRW")]
        public static unsafe extern int wiringPiSPIDataRW(int channel, byte* data, int len);  //char is a signed byte
    }

    /// <summary>
    /// Provides access to the I2C port
    /// </summary>
    public class I2C
    {
        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CSetup")]
        public static extern int wiringPiI2CSetup(int devId);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CRead")]
        public static extern int wiringPiI2CRead(int fd);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CWrite")]
        public static extern int wiringPiI2CWrite(int fd, int data);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CWriteReg8")]
        public static extern int wiringPiI2CWriteReg8(int fd, int reg, int data);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CWriteReg16")]
        public static extern int wiringPiI2CWriteReg16(int fd, int reg, int data);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CReadReg8")]
        public static extern int wiringPiI2CReadReg8(int fd, int reg);

        [DllImport("libwiringPiI2C.so", EntryPoint = "wiringPiI2CReadReg16")]
        public static extern int wiringPiI2CReadReg16(int fd, int reg);
    }
}

