#include "Arduino.h"
#include "Parts.h"

Parts::Parts(int pin, char on, char off) {
	_pin = 0;
	if (pin >= 0 && pin <= 13)_pin = pin;
	On = on;
	Off = off;
	trigger = false;
	pinMode(_pin, OUTPUT);
}

void Parts::ON() {
	if (trigger == true) return;
	digitalWrite(_pin, HIGH);
	trigger = false;
	Serial.write(_pin + ", ON");
}

void Parts::OFF(){
	if (trigger == false) return;
	digitalWrite(_pin, LOW);
	trigger = true;
	Serial.write(_pin + ", OFF");
}
