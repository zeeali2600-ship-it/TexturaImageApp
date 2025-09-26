# Textura AI - Text to Image Generator

A Windows desktop application that generates AI-powered images from text descriptions. Features a free trial with 3 generations and unlimited access through Microsoft Store subscription.

## Features

- **AI Text-to-Image Generation**: Convert text prompts into unique images
- **Free Trial**: 3 free image generations for new users
- **Subscription Model**: Unlimited generations through Microsoft Store
- **Local Storage**: Saves user trial status and preferences
- **Modern UI**: Built with WinUI 3 for native Windows experience

## Technical Stack

- **Framework**: .NET 9.0 with WinUI 3
- **Platform**: Windows Desktop (Windows 10/11)
- **Store Integration**: Microsoft Store subscription support
- **AI API**: Placeholder implementation (integrate with your preferred AI service)

## Setup Instructions

### Prerequisites

- Windows 10 version 1903 (build 18362) or higher
- Visual Studio 2022 with Windows App SDK workload
- .NET 9.0 SDK

### Building the App

1. Clone the repository
2. Open `TexturaAI.csproj` in Visual Studio
3. Restore NuGet packages
4. Build and run the application

```bash
cd src/TexturaAI
dotnet restore
dotnet build
dotnet run
```

## Configuration

### AI API Integration

To integrate with a real AI image generation service:

1. Edit `Services/ImageGenerationService.cs`
2. Replace the `API_ENDPOINT` with your AI service URL
3. Add authentication headers if required
4. Update the request/response format according to your API

Example APIs you can integrate:
- OpenAI DALL-E
- Stability AI
- Midjourney API
- Custom AI services

### Microsoft Store Subscription

For production deployment:

1. Uncomment the real store integration code in `Services/SubscriptionService.cs`
2. Create a Microsoft Store listing
3. Configure subscription products in Partner Center
4. Update the `SUBSCRIPTION_PRODUCT_ID` with your actual product ID

## Deployment

### Microsoft Store Packaging

1. Create an MSIX package using Visual Studio
2. Associate the app with your Microsoft Store listing
3. Configure app manifest with proper identity and capabilities
4. Submit to Microsoft Store for certification

### Local Testing

The app includes debug features for testing:
- "Reset Trial for Testing" button to reset free generation counter
- Mock subscription system for testing premium features

## Project Structure

```
src/TexturaAI/
├── App.xaml/App.xaml.cs          # Application entry point
├── MainWindow.xaml/.cs           # Main UI and logic
├── Services/
│   ├── ImageGenerationService.cs # AI API integration
│   └── SubscriptionService.cs    # Store subscription handling
├── Package.appxmanifest          # App manifest for store
└── TexturaAI.csproj             # Project configuration
```

## Usage

1. **Launch the app** - See your remaining free generations
2. **Enter a prompt** - Describe the image you want to create
3. **Generate image** - Click to create your AI-generated image
4. **Subscribe for unlimited** - Purchase through Microsoft Store for unlimited access

## Development Notes

- Current implementation uses placeholder image generation (colored rectangles)
- Replace `GenerateImageAsync` method with real AI API calls
- Subscription system is mocked for testing purposes
- Add proper error handling for production use
- Consider adding image saving/sharing features

## License

This project is provided as-is for educational and development purposes.