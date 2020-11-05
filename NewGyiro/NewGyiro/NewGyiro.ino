const int X_PIN = A3;
const int Y_PIN = A4;
const int Z_PIN = A5;

struct Coordinate {
    long cx;
    long cy;
    long cz;
};

struct Angle {
    int ax;
    int ay;
    int az;
};

void setup() {
    pinMode(X_PIN, INPUT);
    pinMode(Y_PIN, INPUT);
    pinMode(Z_PIN, INPUT);
    Serial.begin(115200);
}

void loop() {
    Coordinate c = getCoordinate();
    showCoordinate(c); 
    //Angle a = angleCalculation(c);
    //showAngle(a);

    delay(500);
}

Coordinate getCoordinate() {
    Coordinate c;
    long x = 0, y = 0, z = 0;

    for (int i = 0; i < 100; i++) {
        x = x + analogRead(X_PIN);  
        y = y + analogRead(Y_PIN);  
        z = z + analogRead(Z_PIN);  
    }

    c.cx = x / 100;
    c.cy = y / 100;
    c.cz = z / 100;
    return c;
}


Angle angleCalculation(Coordinate c) {
    int MAX_X = 450, MAX_Y = 460, MAX_Z = 460;
    int MIN_X = 210, MIN_Y = 200, MIN_Z = 220;


    float oneAngleX = (MAX_X - MIN_X) / 180.000;
    float oneAngleY = (MAX_Y - MIN_Y) / 180.000;
    float oneAngleZ = (MAX_Z - MIN_Z) / 180.000;

    Angle a;
    a.ax = (c.cx - MIN_X) / oneAngleX - 90;
    a.ay = (c.cy - MIN_Y) / oneAngleY - 90;
    a.az = (c.cz - MIN_Z) / oneAngleZ - 90;
    return a;
}


void showAngle(Angle a) {
    Serial.print(a.ax);
    Serial.print(",");
    Serial.print(a.ay);
    Serial.print(",");
    Serial.print(a.az);
    Serial.println(",");
}


void showCoordinate(Coordinate c) {
Serial.print("x:");
  Serial.print(c.cx);
  Serial.print(" y:");
  Serial.print(c.cy);
  Serial.print(" z:");
  Serial.println(c.cz);
}
