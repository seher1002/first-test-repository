#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN D4  
#define RST_PIN D3 

const char* ssid = "OMiLAB";  
const char* password = "digifofulbs"; 
const char* serverUrl = "http://10.14.10.113:3000/api/data";

MFRC522 mfrc522(SS_PIN, RST_PIN);
WiFiClient client;

void setup() {
    Serial.begin(115200);
    WiFi.begin(ssid, password);
    Serial.print("Conectare la WiFi");
    
    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }
    Serial.println("\nConectat la WiFi");
    
    SPI.begin();
    mfrc522.PCD_Init();
    Serial.println("Apropie un tag RFID...");
}

void loop() {
    if (!mfrc522.PICC_IsNewCardPresent()) {
        delay(1000);
        return;
    }
    
    if (!mfrc522.PICC_ReadCardSerial()) {
        delay(1000);
        return;
    }
    
    String tagID = "";
    for (byte i = 0; i < mfrc522.uid.size; i++) {
        tagID += String(mfrc522.uid.uidByte[i], HEX);
    }
    Serial.print("Tag UID: ");
    Serial.println(tagID);
    
    sendToServer(tagID);
    
    mfrc522.PICC_HaltA();
    delay(2000);
}

void sendToServer(String tagID) {
    if (WiFi.status() != WL_CONNECTED) {
        Serial.println("Conexiune pierdută, nu pot trimite date...");
        return;
    }
    
    HTTPClient http;
    http.begin(client, serverUrl);
    http.addHeader("Content-Type", "application/json");
    
    String jsonPayload = "{";
    jsonPayload += "\"id\": \"" + tagID + "\",";
    jsonPayload += "\"name\": \"Seher, Diana, Andrei\"}";
    
    int httpResponseCode = http.POST(jsonPayload);
    
    if (httpResponseCode == 200) {
        Serial.println("Date trimise cu succes!");
    } else {
        Serial.print("Eroare la trimitere. Cod: ");
        Serial.println(httpResponseCode);
    }
    
    http.end();
}