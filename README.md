# WeatherForecast Solution

Live demo: [https://weatherforecast.paweldywan.com/](https://weatherforecast.paweldywan.com/)

This repository contains a full-stack Weather Forecast application, including backend services, frontend client, and supporting libraries. The solution is organized into several projects:

- **WeatherForecast.BLL**: Business logic layer for weather forecasting and geocoding.
- **WeatherForecast.Server**: ASP.NET Core Web API backend server.
- **WeatherForecast.BLL.Tests**: Unit tests for the business logic layer.
- **WeatherForecast.Server.Tests**: Unit tests for the backend server.
- **weatherforecast.client**: Frontend client built with Vite and TypeScript.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js (v18+)](https://nodejs.org/)
- [npm](https://www.npmjs.com/)
- [Docker](https://www.docker.com/) (optional, for containerization)

---

## Getting Started

### 1. Clone the Repository

```sh
git clone https://github.com/yourusername/weatherforecast.git
cd weatherforecast
```

### 2. Backend Setup

#### a. WeatherForecast.BLL & WeatherForecast.Server

```sh
cd WeatherForecast.sln
dotnet restore
dotnet build
```

#### b. WeatherForecast.BLL.Tests & WeatherForecast.Server.Tests

```sh
cd WeatherForecast.BLL.Tests
dotnet test

cd ../WeatherForecast.Server.Tests
dotnet test
```

### 3. Frontend Setup

```sh
cd weatherforecast.client
npm install
npm run dev
```

---

## Usage

1. Start the backend server:

```sh
cd WeatherForecast.Server
dotnet run
```

2. Access the API at `https://localhost:5001` (or the URL shown in the terminal).

3. Use the frontend client to interact with the application, or test the API using tools like Postman or curl.

---

## Docker

To build and run the application using Docker, follow these steps:

1. Build the Docker images:

```sh
docker-compose build
```

2. Run the containers:

```sh
docker-compose up
```

3. Access the application at `http://localhost:80` (or the port specified in the `docker-compose.yml` file).

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/YourFeature`.
3. Make your changes.
4. Commit your changes: `git commit -m 'Add some feature'`.
5. Push to the branch: `git push origin feature/YourFeature`.
6. Submit a pull request.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
