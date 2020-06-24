/*
  Name:		SerialTest.ino
  Created:	2020/04/16 15:22:44
  Author:	GanGanKamen
*/

bool onOff = false;
float receiveData = 0;
// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	pinMode(12, OUTPUT);
}

// the loop function runs over and over again until power down or reset
void loop() {
	GetSerial();
	switch (onOff)
	{
	case true:
		digitalWrite(12, HIGH);
		Serial.println("on");
		break;
	case false:
		digitalWrite(12, LOW);
		break;
	}
}

/*
void GetSerial() {
	if (Serial.available() > 0) {
		String receiveDataMasage = Serial.readStringUntil(';');
		Serial.println(receiveDataMasage);
		switch (receiveDataMasage[0])
		{
		default:
			break;
		case 'a':
			receiveDataMasage.remove(0,1);
			receiveData = receiveDataMasage.toFloat();
			Serial.println(receiveData);
			digitalWrite(12, LOW);
			break;
		case 'b':
			receiveDataMasage.remove(0,1);
			receiveData = receiveDataMasage.toFloat();
			Serial.println(receiveData);
			digitalWrite(12, HIGH);
			break;
		}

	}
}
*/

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		Serial.println(cmd);
		switch (cmd)
		{
		case 'a':
			onOff = true;
			break;
		case 's':
			onOff = false;
			break;
		}
	}
}
