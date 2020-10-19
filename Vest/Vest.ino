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
#include"Parts.h"

const int SerialSpeed = 115200;
const int Size = 4;
Parts parts[8];

// The setup() function runs once each time the micro-controller starts
void setup()
{
	Serial.begin(SerialSpeed);
	parts[8] = {
  Parts(13,'a','b'),
  Parts(12,'c','d'),
  Parts(11,'e','f'),
  Parts(10,'g','h'),
  Parts(9,'i','j'),
  Parts(8,'k','l'),
  Parts(7,'m','n'),
  Parts(6,'o','p')
	};
}

// Add the main program code into the continuous loop() function
void loop()
{
	GetSerial();

}

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		for (int i = 0; i < 8; i++) {
			bool odd = false;
			if ((i + 1) % 2 == 0) odd = true;
			switch (odd)
			{
			case true:
				if (cmd == parts[i].On) {
					parts[i].ON();
				}
				else if (cmd == parts[i].Off)
				{
					parts[i].OFF();
				}
				break;
			}
		}
		/*
		switch (cmd) {

		case'a':  //�q�[�^�[�@�I��
			parts[0].ON();
			break;
		case'b':  //�q�[�^�[�@�I�t
			parts[0].OFF();
			break;
		case'c':
			parts[1].ON();
			break;
		case'd':
			parts[1].OFF();
			break;
		case'e':
			parts[2].ON();
			break;
		case'f':
			parts[2].OFF();
			break;
		case'g':
			parts[3].ON();
			break;
		case'h':
			parts[3].OFF();
			break;
		case'i':
			parts[4].ON();
			break;
		case'j':
			parts[4].OFF();
			break;
		case'k':
			parts[5].ON();
			break;
		case'l':
			parts[5].OFF();
			break;
		case'm':
			parts[6].ON();
			break;
		case'n':
			parts[6].OFF();
			break;
		case'o':
			parts[7].ON();
			break;
		case'p':
			parts[7].OFF();
			break;
		}
		*/
	}
}
