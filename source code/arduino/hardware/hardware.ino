#include "HX711.h"
float Weight = 0;
String comdata ="";
int motor = 9;
void setup()
{
	Init_Hx711();				//初始化HX711模块连接的IO设置
	Serial.begin(9600);
	pinMode(motor, OUTPUT);  
	delay(3000);
	Get_Maopi();		//获取毛皮
}

void loop()
{
   comdata = "";  
    while (Serial.available() > 0)    
        {  
            int inChar=Serial.read();
            comdata += char(inChar); 
            delay(100);  
        }  
     if (comdata.length() > 0){ 
            int speed=comdata.toInt();
            if(speed==111){  
                Weight = Get_Weight();  
                float pull=(2.352-float(Weight/1000))*9.8;
                Serial.println(pull,3);
                delay(200);
              }
          else{
            if(speed==500||speed==1000||speed==2000){
             int speed=comdata.toInt();
             digitalWrite(motor, HIGH);  
             delay(speed);               
             digitalWrite(motor, LOW);
             delay(speed);
             speed=0;    
            }
          }
     }     
}
