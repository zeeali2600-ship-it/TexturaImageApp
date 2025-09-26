# Textura AI - Deployment Guide

## Overview
Textura AI is a Microsoft Store ready application for AI-powered text-to-image generation with subscription model.

## Pre-deployment Checklist

### 1. Real AI API Integration
Before deploying, replace the placeholder image generation with a real AI service:

**Recommended AI APIs:**
- OpenAI DALL-E API
- Stability AI API  
- Midjourney API
- Hugging Face Inference API

**Integration Steps:**
1. Edit `src/TexturaAI/Services/ImageGenerationService.cs`
2. Replace `API_ENDPOINT` with your chosen service
3. Add API authentication (API key, tokens)
4. Update request/response format
5. Handle error responses appropriately

### 2. Microsoft Store Setup
1. Create Microsoft Partner Center account
2. Reserve app name "Textura AI"
3. Configure subscription products
4. Update `SUBSCRIPTION_PRODUCT_ID` in `SubscriptionService.cs`

### 3. App Identity & Certificates
1. Associate app with Microsoft Store listing in Visual Studio
2. Update `Package.appxmanifest` with your publisher info
3. Generate proper app certificates for signing

## Build & Package Instructions

### Development Build
```bash
cd src/TexturaAI
dotnet restore
dotnet build -c Release
```

### Microsoft Store Package (MSIX)
1. Open project in Visual Studio 2022
2. Right-click project → Publish → Create App Packages
3. Choose "Microsoft Store under new app name"
4. Follow packaging wizard
5. Test package locally before submission

### Manual MSIX Creation
```bash
# Create package manifest
msbuild TexturaAI.csproj /p:Configuration=Release /p:Platform=x64

# Generate MSIX package  
makeappx pack /d "bin\Release\net9.0-windows10.0.19041.0\win10-x64" /p TexturaAI.msix

# Sign package (with certificate)
signtool sign /a /fd SHA256 /tr http://timestamp.digicert.com TexturaAI.msix
```

## Microsoft Store Submission

### Store Listing Requirements
- **App Name**: Textura AI
- **Category**: Productivity / Multimedia
- **Age Rating**: Everyone
- **Screenshots**: 4-10 screenshots of the app in action
- **App Description**: Focus on AI image generation, free trial, subscription benefits

### Required Assets
Create these images before submission:
- Store Logo: 300x300px
- Wide Logo: 620x300px  
- Screenshots: 1366x768px or 1920x1080px
- App Tile Icons: 44x44, 150x150, 310x150px

### Subscription Configuration
1. In Partner Center → Monetization → Add-ons
2. Create subscription add-on
3. Set pricing and billing period
4. Update app code with actual product ID

## Testing Strategy

### Local Testing
1. Use the console test app to verify core logic:
```bash
cd src/ConsoleTest
dotnet run
```

2. Test Windows app in development:
```bash
cd src/TexturaAI
dotnet run
```

### Store Testing
1. Submit beta version to Microsoft Store
2. Use closed beta testing with limited users
3. Verify subscription flow works end-to-end
4. Test on different Windows versions

## Production Configuration

### Environment Variables
Set these for production deployment:
- `AI_API_KEY`: Your AI service API key
- `AI_API_ENDPOINT`: Production AI service URL
- `STORE_SUBSCRIPTION_ID`: Microsoft Store product ID

### Security Considerations
- Never hardcode API keys
- Use Windows Credential Manager for sensitive data
- Implement proper error handling for API failures
- Add rate limiting for API calls

## Monitoring & Analytics

### Recommended Metrics
- Daily/Monthly Active Users
- Free trial conversion rate
- Subscription retention rate
- Image generation success rate
- API error rates

### Integration Options
- Application Insights for telemetry
- Microsoft Store Analytics for downloads/revenue
- Custom analytics for user behavior

## Support & Maintenance

### Customer Support
- Implement in-app feedback system
- Create FAQ/Help documentation
- Set up email support channel
- Monitor Store reviews and respond

### Regular Updates
- Monitor AI API updates and adapt
- Update UI based on user feedback
- Add new features based on usage patterns
- Maintain compatibility with Windows updates

## Cost Considerations

### Development Costs
- Microsoft Developer Account: $19/year
- Code signing certificate: ~$200/year
- AI API usage: Variable based on users

### Revenue Model
- Freemium: 3 free generations → paid subscription
- Subscription pricing: $4.99-9.99/month recommended
- Revenue split: Microsoft takes 30% of subscription revenue

## Launch Strategy

### Phase 1: Beta Launch
- Limited beta release to gather feedback
- Test subscription flow with real users
- Refine UI based on usage patterns

### Phase 2: Public Launch
- Full Microsoft Store release
- Marketing campaign (social media, AI communities)
- App Store Optimization (ASO) for discoverability

### Phase 3: Growth
- Add premium features (batch generation, higher resolution)
- Social sharing capabilities
- Multi-language support
- Desktop wallpaper integration

## Troubleshooting Common Issues

### Build Errors
- Ensure Windows App SDK is installed
- Check target framework compatibility
- Verify all NuGet packages are restored

### Store Submission Errors
- Validate package manifest thoroughly
- Ensure all required assets are included
- Test app on clean Windows installation

### Runtime Issues
- Check network connectivity for API calls
- Verify API keys are configured correctly
- Monitor memory usage for large images