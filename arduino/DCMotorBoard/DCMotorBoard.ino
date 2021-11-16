#include <Wire.h>

/*  pin number
 *  
 */
int enA = 13;
int in1 = 12;
int in2 = 11;
int enB = 10;
int in3 = 9;
int in4 = 8;

void setup()
{
  Wire.begin(4);
  Wire.onReceive()

  pinMode(enA, OUTPUT);
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  pinMode(enB, OUTPUT);
  pinMode(in3, OUTPUT);
  pinMode(in4, OUTPUT);
}

void loop() 
{
  delay(100);
}

void receiveEvent(int size)
{
  bool wind = Wire.read();

  if (wind) {
    digitalWrite(in1, HIGH);
    digitalWrite(in2, LOW);
    digitalWrite(in3, HIGH);
    digitalWrite(in4, LOW);
    
    for (int i = 0; i < 256; i++) {
      analogWrite(enA, i);
      analogWrite(enB, i); 
    }
  }
  else {
    for (int i = 0; i < 256; i++) {
      analogWrite(enA, 255 - i);
      analogWrite(enB, 255 - i);
    }

    digitalWrite(in1, LOW);
    digitalWrite(in2, LOW);
    digitalWrite(in3, LOW);
    digitalWrite(in4, LOW);
  }
}
