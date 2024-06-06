export interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

export enum ForecastMode {
    Location,
    City,
    ZipCode,
    Coordinates
}

export interface ForecastData {
    mode: ForecastMode;
    latitude?: number;
    longitude?: number;
    zipCode?: string;
    countryCode?: string;
    cityName?: string;
    stateCode?: string;
}

export interface TableColumn<T> {
    key: keyof T;
    label: string;
    type?: "dateTime" | "string";
}

interface Option {
    value: number;
    label: string;
}

export interface FormInput<T> {
    key: keyof T;
    label: string;
    type?: "select" | "text" | "number";
    options?: Option[];
    visible?: boolean;
}