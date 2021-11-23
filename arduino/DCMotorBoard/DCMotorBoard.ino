#include <Wire.h>

/*  pin number
 *  PWM13   : power of motor A
 *  PWM12   : spin direction of motor A
 *  PWM11   : spin direction of motor A
 *  PWM10   : power of motor B
 *  PWM9    : spin direction of motor B
 *  PWM8    : spin direction of motor B
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
  Wire.onReceive(receiveEvent);

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

    Serial.println("received: 1");
    
    digitalWrite(in1, HIGH);
    digitalWrite(in2, LOW);
    digitalWrite(in3, HIGH);
    digitalWrite(in4, LOW);
    
    analogWrite(enA, 250);
    analogWrite(enB, 250); 
  }
  else {

    Serial.println("received: 0");
    
    analogWrite(enA, 0);
    analogWrite(enB, 0);

    digitalWrite(in1, LOW);
    digitalWrite(in2, LOW);
    digitalWrite(in3, LOW);
    digitalWrite(in4, LOW);
  }
}
