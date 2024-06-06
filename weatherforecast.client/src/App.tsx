import {
    useEffect,
    useState
} from 'react';

import {
    Container
} from 'reactstrap';

import "bootstrap/dist/css/bootstrap.min.css";

import AppTable from './components/AppTable';

import {
    getWeatherForecast
} from './requests';

import {
    Forecast,
    ForecastMode,
    ForecastData
} from './interfaces';

import AppForm from './components/AppForm';

function App() {
    const [data, setData] = useState<Forecast[]>();

    const [forecastData, setForecastData] = useState<ForecastData>({
        mode: ForecastMode.Location
    });

    useEffect(() => {
        const populateData = async () => {
            const dataToSet = await getWeatherForecast(forecastData);

            setData(dataToSet);
        }

        populateData();
    }, [forecastData]);

    const contents = data === undefined
        ? <p><em>Loading....</em></p>
        : <AppTable
            columns={[
                { key: "date", label: "Date", type: "dateTime" },
                { key: "temperatureC", label: "Temp. (C)" },
                { key: "temperatureF", label: "Temp. (F)" },
                { key: "summary", label: "Summary" }
            ]}
            data={data}
            keyField="date"
        />;

    return (
        <Container fluid>
            <h1>Weather forecast</h1>

            <AppForm
                rowProps={{ xs: 1, sm: 2, md: 3 }}
                inputs={[
                    {
                        key: "mode",
                        label: "Mode",
                        type: "select",
                        options: [
                            { value: ForecastMode.Location, label: "Location" },
                            { value: ForecastMode.City, label: "City" },
                            { value: ForecastMode.ZipCode, label: "Zip code" },
                            { value: ForecastMode.Coordinates, label: "Coordinates" }
                        ]
                    },
                    {
                        key: "countryCode",
                        label: "Country code",
                        visible: forecastData.mode === ForecastMode.City || forecastData.mode === ForecastMode.ZipCode
                    },
                    {
                        key: "stateCode",
                        label: "State code",
                        visible: forecastData.mode === ForecastMode.City
                    },
                    {
                        key: "cityName",
                        label: "City name",
                        visible: forecastData.mode === ForecastMode.City
                    },
                    {
                        key: "zipCode",
                        label: "Zip code",
                        visible: forecastData.mode === ForecastMode.ZipCode
                    },
                    {
                        key: "latitude",
                        label: "Latitude",
                        visible: forecastData.mode === ForecastMode.Coordinates
                    },
                    {
                        key: "longitude",
                        label: "Longitude",
                        visible: forecastData.mode === ForecastMode.Coordinates
                    }
                ]}
                data={forecastData}
                setData={setForecastData}
            />

            {contents}
        </Container>
    );
}

export default App;