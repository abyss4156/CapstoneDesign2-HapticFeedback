/*  pin number
 *  PWM5   : power of motor A
 *  PWM6   : power of motor B
 *  DG12   : spin direction of motor A
 *  DG11   : spin direction of motor A
 *  DG10   : spin direction of motor B
 *  DG9    : spin direction of motor B
 *  DG4    : receive input digital signal
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
}

void loop() 
{
  int oper = int(digitalRead(input));

  if (oper) {
    
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

  delay(100);
}
