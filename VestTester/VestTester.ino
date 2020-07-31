bool heaterSwitch = false;
bool moterSwitch = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  pinMode(12,OUTPUT); //モーター
  pinMode(13,OUTPUT); //ヒーター
}

void loop() {
  // put your main code here, to run repeatedly:
  GetSerial();
  Moter();
  Heater();
}

void GetSerial(){
  while(Serial.available() > 0){
    char cmd = Serial.read();
    Serial.println(cmd);
    switch(cmd){
      case'a':  //ヒーター　オン
      heaterSwitch = true;
      break;
      case'b':  //ヒーター　オフ
      heaterSwitch = false;
      break;
      case'c':
      moterSwitch = true;
      break;
      case'd':
      moterSwitch = false;
      break;
    }
  }
}

void Moter(){
  switch(moterSwitch){
    case true:
    digitalWrite(13, HIGH);
    Serial.println("Moter_ON");
    break;
    case false:
    digitalWrite(13, LOW);
    Serial.println("Moter_OFF");
    break;
  }
}

void Heater(){
  switch(heaterSwitch){
    case true:
    digitalWrite(12, HIGH);
    Serial.println("Heater_ON");
    break;
    case false:
    digitalWrite(12, LOW);
    Serial.println("Heater_OFF");
    break;
  }
}
