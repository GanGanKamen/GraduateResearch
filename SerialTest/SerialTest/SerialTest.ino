/*
  Name:		SerialTest.ino
  Created:	2020/04/16 15:22:44
  Author:	GanGanKamen
*/

bool onOff = false;

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	pinMode(13, OUTPUT);
}

// the loop function runs over and over again until power down or reset
void loop() {
	GetSerial();
	switch (onOff)
	{
	case true:
		digitalWrite(13, HIGH);
		//Serial.println("ON");
		break;
	case false:
		digitalWrite(13, LOW);
		//Serial.println("OFF");
		break;
	}
}

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
