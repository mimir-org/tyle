import { Container, Error, Label } from "./FormField.styled";
import { PropsWithChildren } from "react";

interface FormFieldProps {
  label?: string;
  error?: { message?: string };
}

/**
 * A component for wrapping form inputs with a label and an error message
 * @param label describing the input
 * @param error message for the given input
 * @param children
 * @constructor
 */
export const FormField = ({ label, error, children }: PropsWithChildren<FormFieldProps>) => (
  <Container>
    <Label>{label}</Label>
    {children}
    {error && <Error>{error.message}</Error>}
  </Container>
);
