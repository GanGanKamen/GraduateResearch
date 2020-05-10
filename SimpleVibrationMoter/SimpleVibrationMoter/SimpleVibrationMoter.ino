/*
 Name:		SimpleVibrationMoter.ino
 Created:	2020/05/09 22:44:47
 Author:	GanGanKamen
*/

int amperes[] = { 0,0,0,0 };
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
	if (Serial.available() > 0) {
		char receiveDataMasage = Serial.read();
		switch (receiveDataMasage)
		{
		default:
			break;
		case 'a':
			amperes[0] = 255;
			Serial.println(amperes[0]);
			break;
		case 'b':
			amperes[1] = 255;
			break;
		case 'c':
			amperes[2] = 255;
		case 'd':
			amperes[3] = 255;
			break;
		case 'h':
			amperes[0] = 0;
			break;
		case 'i':
			amperes[1] = 0;
			break;
		case 'j':
			amperes[2] = 0;
			break;
		case 'k':
			amperes[3] = 0;
			break;
		}
		Serial.flush();
	}
}

void GetVibrate() {
	analogWrite(12, amperes[0]);
	analogWrite(11, amperes[1]);
	analogWrite(10, amperes[2]);
	analogWrite(9, amperes[3]);
}
