#include "Arduino.h"
#include "Parts.h"

Parts::Parts(PartsCategoly categoly, int pin, char on, char off) {
	_categoly = categoly;
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
}

void Parts::OFF(){
	if (trigger == false) return;
	digitalWrite(_pin, LOW);
	trigger = true;
}
