/*
 Name:		SimpleVest.ino
 Created:	2020/10/19 13:48:47
 Author:	C427
*/
#include <Wire.h>

// MPU-6050のアドレス、レジスタ設定値
#define MPU6050_WHO_AM_I     0x75  // Read Only
#define MPU6050_PWR_MGMT_1   0x6B  // Read and Write
#define MPU_ADDRESS  0x68

float gyo_y_Max = 250.13;
float gyo_y_min = -250.14;
// デバイス初期化時に実行される
// the setup function runs once when you press reset or power the board
void setup() {
  PinSetUp();
  WireSetUp();
  Serial.begin(115200);	
}

// the loop function runs over and over again until power down or reset
void loop() {
  GetSerial();
  SendSerial();
}

void PinSetUp(){
  for (int i = 13; i > 5; i--)
  {
    pinMode(i, OUTPUT);
  }
}

void WireSetUp(){
  Wire.begin();
  // 初回の読み出し
  Wire.beginTransmission(MPU_ADDRESS);
  Wire.write(MPU6050_WHO_AM_I);  //MPU6050_PWR_MGMT_1
  Wire.write(0x00);
  Wire.endTransmission();

  // 動作モードの読み出し
  Wire.beginTransmission(MPU_ADDRESS);
  Wire.write(MPU6050_PWR_MGMT_1);  //MPU6050_PWR_MGMT_1レジスタの設定
  Wire.write(0x00);
  Wire.endTransmission();
  
}

void SendSerial(){
  Wire.beginTransmission(0x68);
  Wire.write(0x3B);
  Wire.endTransmission(false);
  Wire.requestFrom(0x68, 14, true);
  while (Wire.available() < 14);
  int16_t axRaw, ayRaw, azRaw, gxRaw, gyRaw, gzRaw, Temperature;

  axRaw = Wire.read() << 8 | Wire.read();
  ayRaw = Wire.read() << 8 | Wire.read();
  azRaw = Wire.read() << 8 | Wire.read();
  Temperature = Wire.read() << 8 | Wire.read();
  gxRaw = Wire.read() << 8 | Wire.read();
  gyRaw = Wire.read() << 8 | Wire.read();
  gzRaw = Wire.read() << 8 | Wire.read();

  // 加速度値を分解能で割って加速度(G)に変換する
  float acc_x = axRaw / 16384.0;  //FS_SEL_0 16,384 LSB / g
  float acc_y = ayRaw / 16384.0;
  float acc_z = azRaw / 16384.0;

  // 角速度値を分解能で割って角速度(degrees per sec)に変換する
  float gyro_x = gxRaw / 131.0;//FS_SEL_0 131 LSB / (°/s)
  float gyro_y = gyRaw / 131.0;
  float gyro_z = gzRaw / 131.0;

 Serial.println(gyro_y);
 Serial.flush();
}

void GetSerial() {
	if (Serial.available() > 0) {
		char cmd = Serial.read();
		switch (cmd) {

		case'a':  
			ON(13); //Back 
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
		case'e': //Left
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
		case'i': //Right
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
		case'm': //Front
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
    case'z':
      Serial.end();
      break;
      case 't':
      Serial.begin(115200);
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
