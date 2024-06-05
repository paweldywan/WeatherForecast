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

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [data, setData] = useState<Forecast[]>();

    useEffect(() => {
        const populateData = async () => {
            const dataToSet = await getWeatherForecast();

            setData(dataToSet);
        }

        populateData();
    }, []);

    const contents = data === undefined
        ? <p><em>Loading....</em></p>
        :
        <AppTable
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

            {contents}
        </Container>
    );
}

export default App;