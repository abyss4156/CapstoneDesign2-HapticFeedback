/*  pin number
 *  PWM5   : power of motor A
 *  PWM6   : power of motor B
 *  GPIO12 : spin direction of motor A
 *  GPIO11 : spin direction of motor A
 *  GPIO10 : spin direction of motor B
 *  GPIO9  : spin direction of motor B
 *  GPIO4  : receive input digital signal
 */
int enA = 5;
int enB = 6;
int in1 = 12;
int in2 = 11;
int in3 = 10;
int in4 = 9;
int input = 4;

void setup()
{
  Serial.begin(9600);
  
  pinMode(enA, OUTPUT);
  pinMode(enB, OUTPUT);
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  pinMode(in3, OUTPUT);
  pinMode(in4, OUTPUT);
  pinMode(input, INPUT);

  digitalWrite(enA, LOW);
  digitalWrite(enB, LOW);
}

void loop() 
{
  int oper = int(digitalRead(input));

  if (oper)
  {  
    Serial.println("received: 1");

    digitalWrite(enA, HIGH);
    digitalWrite(enB, HIGH);

    digitalWrite(in1, HIGH);
    digitalWrite(in2, LOW);
    digitalWrite(in3, HIGH);
    digitalWrite(in4, LOW);
  }
  else 
  {
    Serial.println("received: 0");

    digitalWrite(in1, LOW);
    digitalWrite(in2, LOW);
    digitalWrite(in3, LOW);
    digitalWrite(in4, LOW);

    digitalWrite(enA, LOW);
    digitalWrite(enB, LOW);
  }
}
