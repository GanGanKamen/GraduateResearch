/*
 Name:		SerialTest.ino
 Created:	2020/04/16 15:22:44
 Author:	GanGanKamen
*/

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
}

// the loop function runs over and over again until power down or reset
void loop() {
	Serial.print("Hello");
	Serial.println("t");
	Serial.println("");
}
