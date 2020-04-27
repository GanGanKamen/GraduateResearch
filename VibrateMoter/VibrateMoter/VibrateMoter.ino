/*
 Name:		VibrateMoter.ino
 Created:	2020/04/21 12:41:39
 Author:	GanGanKamen
*/

bool isVibrate[4];
float count[4];
float oldTime = 0;
float currentTime = 0;
float deltaTime;
const float vibrateTime = 1000;
// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	pinMode(12, OUTPUT);
	pinMode(11, OUTPUT);
	pinMode(10, OUTPUT);
	pinMode(9, OUTPUT);

	for (int i = 0; i < 4; i++)
	{
		isVibrate[i] = false;
		count[i] = 0;
	}
}

// the loop function runs over and over again until power down or reset
void loop() {

	GetDeltaTime();
	GetSerial();
	GetVibrate();
	StopVibrate();
}

void GetDeltaTime() {
	oldTime = currentTime;
	currentTime = millis();
	deltaTime = currentTime - oldTime;
}

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		Serial.println(cmd);
		switch (cmd) {
		case 'a':
			if (isVibrate[0] == false) {
				isVibrate[0] = true;
				Serial.println("a_get");
			}
			break;
		case 'b':
			if (isVibrate[1] == false) {
				isVibrate[1] = true;
				Serial.println("b_get");
			}
			break;
		}
	}
}

void GetVibrate() {
	for (int i = 0; i < 4; i++) {
		if (isVibrate[i] && count[i] < vibrateTime) {
			if (count[i] == 0) {
				switch (i)
				{
				case 0:
					digitalWrite(12, HIGH);
					Serial.println("a_on");
					break;
				case 1:
					digitalWrite(11, HIGH);
					Serial.println("b_on");
					break;
				case 2:
					digitalWrite(10, HIGH);
					break;
				case 3:
					digitalWrite(9, HIGH);
					break;
				}
			}
			count[i]+= deltaTime;
		}
	}
}

void StopVibrate() {
	for (int i = 0; i < 4; i++) {
		if (isVibrate[i] && count[i] >= vibrateTime) {
			switch (i)
			{
			case 0:
				digitalWrite(12, LOW);
				Serial.println("a_off");
          Serial.println(count[0]);
				break;
			case 1:
				digitalWrite(11, LOW);
				Serial.println("b_off");
				break;
			case 2:
				digitalWrite(10, LOW);
				break;
			case 3:
				digitalWrite(9, LOW);
				break;
			}
			count[i] = 0;
			isVibrate[i] = false;
		}
	}
}
