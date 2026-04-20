// Raspberry Pi Pico Fan Controller by aban0917
// GPIO pins connected to L298N motor driver
#define FAN1_PIN1 2  // IN1 - Fan 1 control
#define FAN1_PIN2 3  // IN2 - Fan 1 reverse (unused)
#define FAN2_PIN1 4  // IN3 - Fan 2 control
#define FAN2_PIN2 5  // IN4 - Fan 2 reverse (unused)
#define FAN1_PIN1 6  // IN5 - Fan 3 control
#define FAN1_PIN2 7  // IN6 - Fan 3 reverse (unused)
#define FAN2_PIN1 8  // IN7 - Fan 4 control
#define FAN2_PIN2 9  // IN8 - Fan 4 control (unused)

// Temperature thresholds in Celsius
#define TEMP_OFF 40
#define TEMP_LOW 50
#define TEMP_MEDIUM 60
#define TEMP_HIGH 70
#define TEMP_MAX 75

float currentTemp = 0;
int fanSpeed = 0;
unsigned long lastReceived = 0;
#define TIMEOUT 5000

void setup() {
  Serial.begin(115200);
  
  pinMode(FAN1_PIN1, OUTPUT);
  pinMode(FAN1_PIN2, OUTPUT);
  pinMode(FAN2_PIN1, OUTPUT);
  pinMode(FAN2_PIN2, OUTPUT);
  
  analogWrite(FAN1_PIN1, 0);
  digitalWrite(FAN1_PIN2, LOW);
  analogWrite(FAN2_PIN1, 0);
  digitalWrite(FAN2_PIN2, LOW);
  
  Serial.println("PICO_READY");
  
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);
}

void loop() {
  if (Serial.available() > 0) {
    String data = Serial.readStringUntil('\n');
    data.trim();
    
    if (data.startsWith("TEMP:")) {
      currentTemp = data.substring(5).toFloat();
      lastReceived = millis();
      
      Serial.print("ACK:");
      Serial.println(currentTemp);
      
      updateFanSpeed(currentTemp);
      
      digitalWrite(LED_BUILTIN, HIGH);
      delay(50);
      digitalWrite(LED_BUILTIN, LOW);
    }
    else if (data.startsWith("PING")) {
      Serial.println("PONG");
    }
    else if (data.startsWith("SPEED:")) {
      int manualSpeed = data.substring(6).toInt();
      setFanSpeed(constrain(manualSpeed, 0, 255));
      Serial.print("SPEED_SET:");
      Serial.println(fanSpeed);
    }
  }
  
  if (millis() - lastReceived > TIMEOUT && lastReceived > 0) {
    setFanSpeed(0);
    currentTemp = 0;
  }
  
  delay(10);
}

void updateFanSpeed(float temp) {
  int speed;
  
  if (temp < TEMP_OFF) {
    speed = 0;
  }
  else if (temp < TEMP_LOW) {
    speed = map(temp, TEMP_OFF, TEMP_LOW, 0, 64);
  }
  else if (temp < TEMP_MEDIUM) {
    speed = map(temp, TEMP_LOW, TEMP_MEDIUM, 64, 128);
  }
  else if (temp < TEMP_HIGH) {
    speed = map(temp, TEMP_MEDIUM, TEMP_HIGH, 128, 192);
  }
  else if (temp < TEMP_MAX) {
    speed = map(temp, TEMP_HIGH, TEMP_MAX, 192, 255);
  }
  else {
    speed = 255;
  }
  
  setFanSpeed(speed);
}

void setFanSpeed(int speed) {
  fanSpeed = constrain(speed, 0, 255);
  
  // Control both fans with PWM
  analogWrite(FAN1_PIN1, fanSpeed);
  digitalWrite(FAN1_PIN2, LOW);
  analogWrite(FAN2_PIN1, fanSpeed);
  digitalWrite(FAN2_PIN2, LOW);
  
  // Send status back to PC
  Serial.print("STATUS:");
  Serial.print(currentTemp);
  Serial.print(",");
  Serial.print(fanSpeed);
  Serial.print(",");
  Serial.println((fanSpeed * 100) / 255);
}