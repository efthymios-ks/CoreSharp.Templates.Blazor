# Auth0 dashboard setup

## Client
1. Enter Auth0 dashboard.
2. Applications > Create Application > Single Page Web Applications > Create.
3. Take note of `Domain` and `ClientId`.
4. Allowed Callback URLs > `https://localhost:<YOUR_PORT_NUMBER>/authentication/login-callback`.
5. Allowed Logout URLs > `https://localhost:<YOUR_PORT_NUMBER>/authentication/logout-callback`.
6. Setup your code.
- These are the default authetication paths. For different callbacks, you have to set them here and also by code.

### Fix logout issues
1. Enter Auth0 dashboard.
2. Settings > Advanced > RP-Initiated Logout End Session Endpoint Discovery.

## Server
1. Enter Auth0 dashboard.
2. Create API.
3. Provide a friendly name for your API (AppName API) and a unique identifier (also known as audience) in the URL format (for example, https://my-app-name-api.com).
4. Leave the signing algorithm to RS256 and click the Create button.
5. Setup your code.

## Server - Swagger
1. Enter Auth0 dashboard.
2. Applications > Find existing application > Advanced settings > Grant Types > Implicit, Authorization Code.
3. Take note of `ClientId` and `ClientSecret`.
4. Allowed Callback URLs > https://localhost:<YOUR_PORT_NUMBER>/swagger/oauth2-redirect.html
5. Setup your code.
6. 