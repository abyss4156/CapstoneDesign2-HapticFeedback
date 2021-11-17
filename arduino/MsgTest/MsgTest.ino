#include <CurieBLE.h>

const char* serv_uuid = "fbac47bc-c3a8-4119-b5cd-1fa5b8783681";
const char* char_uuid = "fbac47bd-c3a8-4119-b5cd-1fa5b8783681";

BLEPeripheral device;
BLEService device_serv(serv_uuid);
BLEUnsignedCharCharacteristic device_char(char_uuid, BLERead | BLEWrite | BLENotify);

void setup() 
{
  Serial.begin(9600);

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

  char operCode = 0;

  if (device_central) {
    
    Serial.print("connected to the central: ");
    Serial.println(device_central.address());

    while (device_central.connected()) {

      if (device_char.written())
        operCode = device_char.value();

      Serial.println("received: " + String(int(operCode)));

      if (operCode & 1)
        Serial.println("1st bit is activated");
      if (operCode & 2)
        Serial.println("2nd bit is activated");
      if (operCode & 4)
        Serial.println("3rd bit is activated");
      if (operCode & 8)
        Serial.println("4th bit is activated");
      if (operCode & 16)
        Serial.println("5th bit is activated");
      if (operCode & 32)
        Serial.println("6th bit is activated");
    }
  }
  else
    Serial.println("device is not connected with central");
}
