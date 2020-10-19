/*
 Name:		SimpleVest.ino
 Created:	2020/10/19 13:48:47
 Author:	C427
*/

// the setup function runs once when you press reset or power the board
void setup() {
  Serial.begin(115200);
	for (int i = 13; i > 5; i--)
	{
		pinMode(i, OUTPUT);
	}
 Serial.write("start");
}

// the loop function runs over and over again until power down or reset
void loop() {
  GetSerial();
}

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		switch (cmd) {

		case'a':  
			ON(13);
			break;
		case'b':  
			OFF(13);
			break;
		case'c':
			ON(12);
			break;
		case'd':
			OFF(12);
			break;
		case'e':
			ON(11);
			break;
		case'f':
			OFF(11);
			break;
		case'g':
			ON(10);
			break;
		case'h':
			OFF(10);
			break;
		case'i':
			ON(9);
			break;
		case'j':
			OFF(9);
			break;
		case'k':
			ON(8);
			break;
		case'l':
			OFF(8);
			break;
		case'm':
			ON(7);
			break;
		case'n':
			OFF(7);
			break;
		case'o':
			ON(6);
			break;
		case'p':
			OFF(6);
			break;
		}
	}
}

void ON(int pin) {
	digitalWrite(pin, HIGH);
	String msg = String(pin) + "ON";
	char charBuf[10];
	msg.toCharArray(charBuf, 10);
	Serial.write(charBuf);
}

void OFF(int pin) {
	digitalWrite(pin, LOW);
	  String msg = String(pin) + "OFF";
  char charBuf[10];
  msg.toCharArray(charBuf, 10);
  Serial.write(charBuf);
}
