#PicoFlow ||||High performance universal Pc/Laptop cooling Fan hub||||

PicoFlow: Universal DIY 9-Channel PC Thermal Controller
The high-performance, budget-friendly alternative to premium fan hubs.

Most high quality and branded commercial fan controllers cost highly, limit you to specific proprietary software, and offer a restricted number of headers. Pico-Cooler Pro breaks those barriers. By leveraging the Raspberry Pi Pico or Pi Zero or Pi series and a custom C# Windows interface, this project provides a professional-grade, 9-channel cooling system that responds in real-time to your PC’s internal temperatures for a fraction of the cost.

🚀 The Value Proposition
Cost-Effective: Achieve "Commander Pro" levels of control for the price of a Pico and a motor driver.

Hardware Agnostic: Use any 12V fan—standard case fans, high-RPM server fans, or salvaged laptop blowers.

Extreme Scalability: Unlike motherboard headers that can fry under high current, this system uses external drivers, allowing you to daisy-chain multiple fans per channel (GPIO 1-9).

Total Control: This controls better than those 3rd party apps, this can probably cool your Hot-Oven PC or Cookable laptop bottom

🛠 Hardware Architecture
The Controller: Raspberry Pi Series Only
This project is purpose-built for the Raspberry Pi ecosystem.

Compatible Boards: Raspberry Pi Pico, Pico W, and the Raspberry Pi Zero series.

❌ NOT Compatible with Arduino: This system utilizes specific logic and serial handling optimized for Raspberry Pi hardware. Arduino boards are not supported.

9 Dedicated Channels: Utilizes GPIO pins 1 through 9 for independent, high-precision control.

The Driver Compatibility
This system is designed to work with any motor driver or stepper motor driver that includes an IN1 (or similar logic input) functionality.

Supported Drivers: L298N, ULN2003, MOSFET modules, or dedicated Stepper drivers.

Versatility: Because it uses standard logic pins, you can even use this to control moving case parts (like motorized vents) or water-cooling pumps.

💻 Software Stack
The PC-side control center is a lightweight, high-performance Windows Forms application written in C#.

Core Technologies:
LibreHardwareMonitorLib / OpenHardwareMonitor: Used to tap into the kernel-level thermal sensors of your CPU and GPU. This ensures the fans react the millisecond a temperature spike is detected.

System.IO.Ports: Manages a dedicated, low-latency Serial communication line over USB to the Raspberry Pi.

Custom Fan Mapping: The app allows you to map specific hardware sensors to specific GPIO pins on the Pi.

⚠️ Critical Notice: Anti-Cheat Compatibility
This tool interacts with low-level system drivers to read hardware registers.

[!CAUTION]
Kernel-Level Anti-Cheat: Software like Easy Anti-Cheat (EAC) (used in Crossout, Apex Legends, War Thunder) and BattlEye may flag hardware monitoring drivers as a security risk because the Libraries used in making this application can take control control fans by looking onto temperature sensor with no latency.

Usage Rule: To ensure your game accounts remain safe, close the C# Monitoring App before launching any game that uses kernel-level anti-cheat. Your fans will remain at their last commanded speed or stay in a "safe" default state defined in your Pi's firmware.

📋 Installation & Setup
Hardware Wiring:

Connect Raspberry Pi GPIO 1-9 to your motor driver's Input pins. (depend on user)

Connect your 12V or 24v or how much ever user's module support to the motor driver.

Mandatory: Connect a Common Ground (GND) between the Pi and your 12V power supply.

Pi Firmware:

Copy the arduino ide file to your Pico or Pi Zero or whichever you have with the provided script to listen for Serial commands and output PWM/Logic signals on GPIO Pins 1-9.

Windows App:

Launch the Pico-Cooler C# App.

Select your COM Port and and this app automatically maintains fan speed depend upon your CPU/GPU sensors.
--------------Dont try to cool a damn bomb running minecraft insane graphics on potatos------------------------

Why This Project Matters?
This isn't just a fan hub; it’s a modder’s tool. Whether you are building a custom open-air test bench, a water-cooled beast, or just want to save money on a budget build, Pico-Cooler Pro gives you the professional features of high-end hardware with the freedom of open-source DIY.
