import {
    Button,
    Form,
    FormGroup,
    Input,
    Label,
    Row,
    RowProps
} from "reactstrap";

import { FormInput } from "../interfaces";

interface Props<T> {
    rowProps: RowProps,
    inputs: FormInput<T>[],
    data: T,
    setData: (data: T) => void,
    onSubmit?: () => void,
    buttonText?: string,
    className?: string
}

const AppForm = <T,>({
    rowProps,
    inputs,
    data,
    setData,
    onSubmit,
    buttonText,
    className
}: Props<T>) => {
    return (
        <Form
            onSubmit={e => {
                e.preventDefault();

                onSubmit?.()
            }}
            className={className}
        >
            <Row {...rowProps}>
                {inputs
                    .filter(i => i.visible !== false)
                    .map(input =>
                        <FormGroup key={input.key as string}>
                            <Label for={input.key as string}>{input.label}</Label>

                            <Input
                                id={input.key as string}
                                name={input.key as string}
                                type={input.type}
                                value={data[input.key] as string}
                                onChange={e => setData({ ...data, [input.key]: input.type === 'select' ? parseInt(e.target.value) : e.target.value })}
                            >
                                {input.options?.map(option =>
                                    <option key={option.value} value={option.value}>{option.label}</option>
                                )}
                            </Input>
                        </FormGroup>
                    )}
            </Row>

            <Button type="submit">{buttonText || 'Submit'}</Button>
        </Form>
    );
}

export default AppForm;