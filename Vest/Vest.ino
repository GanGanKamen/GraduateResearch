// Visual Micro is in vMicro>General>Tutorial Mode
// 
/*
    Name:       Vest.ino
    Created:	2020/09/07 10:24:10
    Author:     DESKTOP-6EKBB5E\GanGanKamen
*/

// Define User Types below here or use a .h file
//


// Define Function Prototypes that use User Types below here or use a .h file
//


// Define Functions below here or use other .ino or cpp files
//
bool heaterSwitch = false;
bool moterSwitch = false;

// The setup() function runs once each time the micro-controller starts
void setup()
{
	bool heaterSwitch = false;
	bool moterSwitch = false;

}

// Add the main program code into the continuous loop() function
void loop()
{
	GetSerial();
	Moter();
	Heater();

}

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		switch (cmd) {
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

void Moter() {
	switch (moterSwitch) {
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

void Heater() {
	switch (heaterSwitch) {
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