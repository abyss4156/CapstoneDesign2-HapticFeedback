#include <CurieBLE.h>

#define EXPLOSION   0x01
#define REGULAR_VIB 0x02
#define RANDOM_VIB  0x04
#define WIND        0x08
#define COOLING     0x10
#define HEATING     0x20

// personal BLE characteristic UUID
const char* serv_uuid = "fbac47bc-c3a8-4119-b5cd-1fa5b8783681";
const char* char_uuid = "fbac47bd-c3a8-4119-b5cd-1fa5b8783681";

BLEPeripheral device;
BLEService device_serv(serv_uuid);
BLEUnsignedCharCharacteristic device_char(char_uuid, BLERead | BLEWrite | BLENotify);

/*  pin number
 *  PWM3  : output for micro vibration motor
 *          first amp input right
 *  PWM5  : same as above
 *          first amp input left
 *  PWM6  : same as above
 *          second amp input right
 *  PWM9  : same as above
 *          second amp input left
 *  GPIO10: output for peltier heater module
 *          relay input 3
 *  GPIO11: output for peltier cooler module
 *          relay input 2
 *  GPIO12 : output for cooling fan
 *          relay input 1
 *  GPIO4 : send the digital signal for connection with another board
 */
int vib[4] = {3, 5, 6, 9};
int peltier[3] = {12, 11, 10};
int dcInput = 4;

void setup()
{
  randomSeed(analogRead(0));
  
  Serial.begin(9600);
  
  pinMode(vib[0], OUTPUT);
  pinMode(vib[1], OUTPUT);
  pinMode(vib[2], OUTPUT);
  pinMode(vib[3], OUTPUT);
  pinMode(peltier[0], OUTPUT);
  pinMode(peltier[1], OUTPUT);
  pinMode(peltier[2], OUTPUT);
  pinMode(dcInput, OUTPUT);

  device.setLocalName("BLE Test");
  device.setAdvertisedServiceUuid(device_serv.uuid());
  device.addAttribute(device_serv);
  device.addAttribute(device_char);
  device_char.setValue(0);

  device.begin();
}

void loop()
{
  char temp = 0; 
  
  BLECentral device_central = device.central();

  if (device_central) 
  {
    Serial.print("connected to the central: ");
    Serial.println(device_central.address());

    while (device_central.connected()) 
    {
      if (temp = device_char.value()) 
      {
        Serial.print("received: " + String(int(temp)) + "\t");

        /*  meaning of each binary
         *  binary1  : explosion
         *  binary2  : regular vibration
         *  binary3  : random vibration
         *  binary4  : wind
         *  binary5  : cooling module
         *  binary6  : heating module
         */

        // print serial binary for check
        Serial.print(temp&EXPLOSION ? 1 : 0);
        Serial.print(temp&REGULAR_VIB ? 1 : 0);
        Serial.print(temp&RANDOM_VIB ? 1 : 0);
        Serial.print(temp&WIND ? 1 : 0);
        Serial.print(temp&COOLING ? 1 : 0);
        Serial.println(temp&HEATING ? 1 : 0);

        // decide vibration mode 
        if (temp & EXPLOSION) 
        {
          analogWrite(vib[0], 250);
          analogWrite(vib[1], 250);
          analogWrite(vib[2], 250);
          analogWrite(vib[3], 250);
        }
        else if (temp & REGULAR_VIB) 
        {
          analogWrite(vib[0], 200);
          analogWrite(vib[1], 200);
          analogWrite(vib[2], 200);
          analogWrite(vib[3], 200);
        }
        else if (temp & RANDOM_VIB)
        {
          vibration_random();
        }
        else 
        {
          analogWrite(vib[0], 0);
          analogWrite(vib[1], 0);
          analogWrite(vib[2], 0);
          analogWrite(vib[3], 0);
        }

        // send the signal whether wind is needed or not
        digitalWrite(dcInput, temp & WIND ? HIGH : LOW);

        // activate or deactivate cooling and heating module
        // relay module activate when the signal is LOW, opposite is HIGH
        digitalWrite(peltier[0], temp & COOLING ? LOW : HIGH);
        digitalWrite(peltier[1], temp & COOLING ? LOW : HIGH);
        digitalWrite(peltier[2], temp & HEATING ? LOW : HIGH);
      }
      else 
      {
        // if the operation code is 0(null), condition of 'if' state is negative
        // for exception handling, make 'else' state when the code is 0
        Serial.println("received: 0\t000000");

        // turn off and deactivate all modules
        analogWrite(vib[0], 0);
        analogWrite(vib[1], 0);
        analogWrite(vib[2], 0);
        analogWrite(vib[3], 0);
        digitalWrite(dcInput, LOW);
        digitalWrite(peltier[0], HIGH);
        digitalWrite(peltier[1], HIGH);
        digitalWrite(peltier[2], HIGH);
      }
    }
  }
  else 
  {
    Serial.println("device is not connected with central");  

    // when the device disconnect with content abnormaly
    // module can be leaved as activated states
    // for exception handling, turn off all modules
    analogWrite(vib[0], 0);
    analogWrite(vib[1], 0);
    analogWrite(vib[2], 0);
    analogWrite(vib[3], 0);
    digitalWrite(dcInput, LOW);
    digitalWrite(peltier[0], HIGH);
    digitalWrite(peltier[1], HIGH);
    digitalWrite(peltier[2], HIGH);
  }
}

void vibration_random()
{
  int interval = 0;
  int power = 0;
  int index = 0;
  
  for (int i = 0; i < 5; i++) 
  {  
    interval = random(50, 150);
    power = random(150, 250);
    index = random(4);

    analogWrite(vib[index], power);
    delay(50);
    analogWrite(vib[index], 0);
    delay(interval);
  }
}
