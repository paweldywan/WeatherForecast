import {
    Table
} from "reactstrap";

import {
    TableColumn
} from "../interfaces";

interface Props<T> {
    data: T[];
    columns: TableColumn<T>[];
    keyField: keyof T;
}

const AppTable = <T,>({
    data,
    columns,
    keyField
}: Props<T>) => {
    return (
        <Table
            bordered
            dark
            hover
            responsive
            striped
        >
            <thead>
                <tr>
                    {columns.map(column =>
                        <th key={column.key as string}>{column.label}</th>
                    )}
                </tr>
            </thead>
            <tbody>
                {data.map(element =>
                    <tr key={element[keyField] as string}>
                        {columns.map(column =>
                            <td key={column.key as string}>
                                {column.type === "dateTime"
                                    ? new Date(element[column.key] as string).toLocaleString()
                                    : element[column.key] as string}
                            </td>
                        )}
                    </tr>
                )}
            </tbody>
        </Table>
    );
};

export default AppTable;
