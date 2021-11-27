#include <CurieBLE.h>

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
 *  DG10  : output for peltier heater module
 *          relay input 3
 *  DG11  : output for peltier cooler module
 *          relay input 2
 *  DG12  : output for cooling fan
 *          relay input 3
 *  DG4   : send the digital signal for connection with another board
 */
int vib[4] = {3, 5, 6, 9};
int peltier[3] = {12, 11, 10};
int dcInput = 4;

// to check keeping activation of cooling module
//bool keepCool = false;
//unsigned long lastTime = 0;

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
  BLECentral device_central = device.central();

  if (device_central) {
    
    Serial.print("connected to the central: ");
    Serial.println(device_central.address());

    while (device_central.connected()) {
      
      if (char temp = device_char.value()) {

        Serial.print("received: " + String(int(temp)) + "\t");

        /*  meaning of each binary
         *  bit1  : explosion
         *  bit2  : regular vibration
         *  bit3  : random vibration
         *  bit4  : wind
         *  bit5  : cooling module
         *  bit6  : heating module
         */

        // print serial binary for check
        Serial.print(temp&1 ? 1 : 0);
        Serial.print(temp&2 ? 1 : 0);
        Serial.print(temp&4 ? 1 : 0);
        Serial.print(temp&8 ? 1 : 0);
        Serial.print(temp&16 ? 1 : 0);
        Serial.println(temp&32 ? 1 : 0);

        // decide vibration mode 
        if (temp & 1) {
          for (int i = 0; i < 4; i++)
            analogWrite(vib[i], 200);
        }
        else if (temp & 2) {
          for (int i = 0; i < 4; i++)
            analogWrite(vib[i], 100);
        }
        else if (temp & 4)
          vibration_random();
        else {
          for (int i = 0; i < 4; i++)
            analogWrite(vib[i], 0);
        }

        // send the signal whether wind is needed or not
        digitalWrite(dcInput, temp&8 ? HIGH : LOW);

        // activate or deactivate cooling and heating module
        //if (temp & 16 || keepCool)
        //  cooling();
        digitalWrite(peltier[1], temp&16 ? HIGH : LOW);
        digitalWrite(peltier[2], temp&16 ? HIGH : LOW);
        digitalWrite(peltier[0], temp&32 ? HIGH : LOW);
      }
    }
  }
  else
    Serial.println("device is not connected with central");
}

void vibration_random()
{
  int interval = 0;
  int power = 0;
  
  for (int i = 0; i < 4; i++) {
    interval = random(50, 250);
    power = random(100, 250);

    analogWrite(vib[i], power);
    delay(100);
    analogWrite(vib[i], 0);
    delay(interval);
  }
}

/*
void cooling()
{
  // when get in the function first time
  // set the lastTime when cooling start
  // then change keepCool value for continue to call this function in loop()
  if (!keepCool)
    lastTime = millis();
  keepCool = true;

  // get the present time
  // turn on cooling module until reach to the 3000ms after lastTime
  unsigned long tempTime = millis();

  if (tempTime < lastTime + 3000) {
    
    digitalWrite(peltier[1], HIGH);
    digitalWrite(peltier[2], HIGH);
  }
  else {

    digitalWrite(peltier[1], LOW);
    digitalWrite(peltier[2], LOW);
    keepCool = false;
  }
}
*/
