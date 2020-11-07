void setup()
{
     // シリアルモニターの初期化をする
     Serial.begin(115200) ;
}
void loop()
{
int i , a ;
     long x , y , z ;

     // 各データを100回読込んで平均化する
     x = y = z = 0 ;
     for (i=0 ; i < 100 ; i++) {
          x = x + analogRead(3) ;  // Ｘ軸を読込む
          y = y + analogRead(4) ;  // Ｙ軸を読込む
          z = z + analogRead(5) ;  // Ｚ軸を読込む
     }
     x = x / 100 ;
     y = y / 100 ;
     z = z / 100 ;
     // 角度を計算して表示を行う
     a=atan2(x-323,z-317) / 3.14159 * 180.0 ;
     Serial.print("X=") ;
     Serial.print(a) ;             // X軸方向角度表示
     Serial.print(" Y=") ;
     a=atan2(y-326,z-317) / 3.14159 * 180.0 ;
     Serial.println(a) ;           // Y軸方向角度表示

     delay(500) ;
}
