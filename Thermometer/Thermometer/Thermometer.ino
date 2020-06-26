/*
 Name:		Thermometer.ino
 Created:	2020/06/26 14:15:03
 Author:	GanGanKamen
*/

int pin = A0;
float thermometerValue = 0;
// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);

}

// the loop function runs over and over again until power down or reset
void loop() {
	thermometerValue = analogRead(pin);
	float temp = modTemp(thermometerValue);
	Serial.println(temp);
	delay(500);
}

float modTemp(float val) {
	float tempC = val /1024 * 100;
	return tempC;
}
