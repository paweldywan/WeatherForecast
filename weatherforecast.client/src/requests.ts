export const getWeatherForecast = async () => {
    const currentCoordinates = await new Promise<GeolocationPosition>((resolve, reject) => navigator.geolocation.getCurrentPosition(resolve, reject));

    const { latitude, longitude } = currentCoordinates.coords;

    const response = await fetch(`api/weatherforecast?${new URLSearchParams({
        latitude: latitude.toString(),
        longitude: longitude.toString()
    })}`);

    return await response.json();
};