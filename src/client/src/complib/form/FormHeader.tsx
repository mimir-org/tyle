import { SubTitle } from "./FormHeader.styled";

interface Props {
  title?: string;
  subtitle?: string;
}

/**
 * A component for describing a given form
 * @param title
 * @param subTitle
 * @constructor
 */
export const FormHeader = ({ title, subtitle }: Props) => (
  <header>
    {title && <h1>{title}</h1>}
    {subtitle && <SubTitle>{subtitle}</SubTitle>}
  </header>
);
