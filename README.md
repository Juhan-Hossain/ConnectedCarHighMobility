

# Connected Car Event Trigger using High Mobility API

This repository integrates the [High Mobility API](https://www.high-mobility.com/) to trigger custom events for connected cars. The project communicates with vehicles in real time and manages custom event notifications using Firebase.

## Features

- Integration with the High Mobility API.
- Trigger custom events based on real-time telematics data.
- Push notifications using **Firebase Cloud Messaging (FCM)**.
- **Firebase Admin SDK** with service account permissions.
- **Serilog** for logging.
- **Unity** for Dependency Injection.

## Technologies Used

- **.NET Core 8.0**
- **High Mobility API**
- **Firebase Cloud Messaging (FCM)**
- **Firebase Admin SDK** for permission management.
- **Serilog** for logging.

## Prerequisites

- **.NET Core SDK** - Ensure you have .NET Core SDK 7.0 installed.
- **High Mobility API Key** - Register with High Mobility and obtain an API key.
- **Firebase Admin SDK** - Set up Firebase and create a service account for admin permissions.
  
  To create the Firebase admin permission file:
  1. Go to your Firebase console.
  2. Navigate to **Project Settings** > **Service Accounts**.
  3. Click on **Generate New Private Key** to download the JSON file.
  4. Save the file in a secure location.

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/high-mobility-connected-car.git
   ```

2. Navigate to the project directory:
   ```bash
   cd high-mobility-connected-car
   ```

3. Add the Firebase admin permission file (service account JSON) to your project directory.

4. Set up your environment variables for the High Mobility API and Firebase:
   ```bash
   export HIGH_MOBILITY_API_KEY=your_api_key
   export FIREBASE_SERVER_KEY=your_firebase_server_key
   export FIREBASE_ADMIN_SDK_PATH=/path/to/your/firebase-admin-sdk.json
   ```

5. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```

6. Run the project:
   ```bash
   dotnet run
   ```

## Configuration

The project uses **dependency injection** for service configuration. Update your `appsettings.json` with the necessary API keys and settings:
```json
{
  "HighMobility": {
    "ApiKey": "your_api_key",
    "BaseUrl": "https://sandbox.api.high-mobility.com/v1/"
  },
  "Firebase": {
    "ServerKey": "your_firebase_server_key",
    "AdminSdkPath": "/path/to/your/firebase-admin-sdk.json",
    "NotificationUrl": "https://fcm.googleapis.com/fcm/send"
  }
}
```

## How It Works

1. The project listens for real-time data from connected cars using the High Mobility API.
2. Custom events are triggered based on conditions (e.g., vehicle malfunctions or threshold breaches).
3. Push notifications are sent to users via **Firebase Cloud Messaging (FCM)**, using admin permissions from the Firebase Admin SDK.

## Logging

**Serilog** handles logging in the project. Update the `appsettings.json` file for custom logging settings:
```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      { "Name": "Console" },
      { "Name": "File", "Args": { "path": "Logs/log.txt" } }
    ]
  }
}
```

## Contributing

Contributions are welcome! Fork the repository and submit pull requests with new features, bug fixes, or enhancements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

This version includes the necessary steps for setting up the Firebase Admin SDK. Let me know if you'd like to add or modify anything further!
