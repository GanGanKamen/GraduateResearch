/*
 Name:		VibrateMoter.ino
 Created:	2020/04/21 12:41:39
 Author:	GanGanKamen
*/

int amperes[] = {0,0,0,0};

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	pinMode(12, OUTPUT);
	pinMode(11, OUTPUT);
	pinMode(10, OUTPUT);
	pinMode(9, OUTPUT);

}

// the loop function runs over and over again until power down or reset
void loop() {
	GetSerial();
	GetVibrate();
}


void GetSerial() {
	while (Serial.available()) {
		Serial.println("x");
		String receiveDataMasage = Serial.readStringUntil(';');
		Serial.println("y");
		char sign = receiveDataMasage[0];
		receiveDataMasage.remove(0, 1);
		float receiveData = receiveDataMasage.toFloat();
		if (receiveData < 0 || receiveData > 1)return;
		int ampere = (int)(receiveData * 255);
		switch (sign)
		{
		default:
			break;
		case 'a':			
			amperes[0] = ampere;
      Serial.println(amperes[0]);
			break;
		case 'b':
			amperes[1] = ampere;
			break;
		case 'c':
			amperes[2] = ampere;
		case 'd':
			amperes[3] = ampere;
			break;
		}
	}
}

void GetVibrate() {
	analogWrite(12, amperes[0]);
	analogWrite(11, amperes[1]);
	analogWrite(10, amperes[2]);
	analogWrite(9, amperes[3]);
}
