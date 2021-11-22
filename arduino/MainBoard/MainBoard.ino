#include <CurieBLE.h>
#include <Wire.h>

const char* serv_uuid = "fbac47bc-c3a8-4119-b5cd-1fa5b8783681";
const char* char_uuid = "fbac47bd-c3a8-4119-b5cd-1fa5b8783681";

BLEPeripheral device;
BLEService device_serv(serv_uuid);
BLEUnsignedCharCharacteristic device_char(char_uuid, BLERead | BLEWrite | BLENotify);

int oper = 0;

/*  pin number
 *  PWM2  : output for micro vibration motor
 *    first amp input right
 *  PWM3  : same as above
 *    first amp input left
 *  PWM4  : same as above
 *    second amp input right
 *  PWM5  : same as above
 *    second amp input left
 *  PWM9  : output for peltier heater module
 *    relay input 1
 *  PWM10 : output for peltier cooler module
 *    relay input 2
 *  PWM11 : output for cooling fan
 *    relay input 3
 *  A4    : wire communication for controlling DC motors
 */
int vib[4] = [2, 3, 4, 5];
int dcInput = 4;
int peltier[3] = [9, 10, 11];

void setup()
{
  Wire.begin();
  Serial.begin(9600);
  randomSeed(analogRead(0));
  
  pinMode(vib[0], OUTPUT);
  pinMode(vib[1], OUTPUT);
  pinMode(vib[2], OUTPUT);
  pinMode(vib[3], OUTPUT);

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

        Serial.println("received: " + String(int(temp)));

        if (temp & 1) {
          
          Serial.println("1st bit is activated");
          vibration_explosion();
        }
        
        if (temp & 2) {
          
          Serial.println("2nd bit is activated");
          vibration_regular();
        }
        
        if (temp & 4) {
          
          Serial.println("3rd bit is activated");
          vibration_waterdrop();
        }
        
        if (temp & 8) {
          
          Serial.println("4th bit is activated");
          Wire.beginTransmission(dcInput);
          Wire.write(wind);
          Wire.endTransmission();
        }
        
        if (temp & 16) {
          
          Serial.println("5th bit is activated");
          digitalWrite(peltier[0], LOW);
          digitalWrite(peltier[1], HIGH);
          digitalWrite(peltier[2], HIGH);
        }
        
        if (temp & 32) {
          
          Serial.println("6th bit is activated");
          digitalWrite(peltier[0], HIGH);
          digitalWrite(peltier[1], LOW);
          digitalWrite(peltier[2], LOW);
        }
        
        device.end();
      }
    }

    device.begin();
  }
  else
    Serial.println("device is not connected with central");
}

void vibration_explosion()
{
  for (int i = 50; i < 256; i++) {
    analogWrite(vib[0], i);
    analogWrite(vib[1], i);
    analogWrite(vib[2], i);
    analogWrite(vib[3], i);
  }

  delay(200);

  for (int i = 0; i < 256; i++) {
    analogWrite(vib[0], 255 - i);
    analogWrite(vib[1], 255 - i);
    analogWrite(vib[2], 255 - i);
    analogWrite(vib[3], 255 - i);
  }
}

void vibration_regular()
{
  for (int i = 50; i < 150; i++) {
    analogWrite(vib[0], i);
    analogWrite(vib[1], i);
    analogWrite(vib[2], i);
    analogWrite(vib[3], i);
  }

  delay(200);

  for (int i = 0; i < 150; i++) {
    analogWrite(vib[0], 150 - i);
    analogWrite(vib[1], 150 - i);
    analogWrite(vib[2], 150 - i);
    analogWrite(vib[3], 150 - i);
  }
}

void vibration_waterdrop()
{
  int interval = 0;
  int power = 0;
  
  for (int i = 0; i < 4; i++) {
    int interval = random(100, 300);
    int power = random(50, 255);

    analogWrite(vib[i], power);
    delay(100);
    analogWrite(vib[i], 0);
    delay(interval);
  }
}
