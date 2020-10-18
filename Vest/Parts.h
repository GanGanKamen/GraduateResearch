#pragma once

class Parts
{
public:
	Parts(PartsCategoly categoly, int pin, char on, char off);
	void ON();
	void OFF();
	char On; char Off;

private:
	PartsCategoly _categoly;
	int _pin;
	bool trigger;
};

enum PartsCategoly
{
	Moter,
	Heater
};
