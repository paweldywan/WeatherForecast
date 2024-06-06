import {
    ForecastData,
    ForecastMode
} from "./interfaces";

export const getWeatherForecast = async (data: ForecastData) => {
    const dataToSend = { ...data };

    if (dataToSend.mode === ForecastMode.Location) {
        const currentCoordinates = await new Promise<GeolocationPosition>((resolve, reject) => navigator.geolocation.getCurrentPosition(resolve, reject));

        dataToSend.latitude = currentCoordinates.coords.latitude;

        dataToSend.longitude = currentCoordinates.coords.longitude;
    }

    const entries = Object.entries(dataToSend).filter(([, value]) => value !== undefined).map(([key, value]) => [key, value.toString()]);

    const requestObject = Object.fromEntries(entries);

    const response = await fetch(`api/weatherforecast?${new URLSearchParams(requestObject)}`);

    return await response.json();
};