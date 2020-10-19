#pragma once

class Parts
{
public:
	Parts(int pin, char on, char off);
	void ON();
	void OFF();
	char On; char Off;

private:
	int _pin;
	bool trigger;
};

enum PartsCategoly
{
	Moter,
	Heater
};
