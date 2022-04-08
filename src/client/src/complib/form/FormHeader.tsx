import { Heading } from "../text";
import { THEME } from "../core";

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
    {title && <Heading>{title}</Heading>}
    {subtitle && (
      <Heading as={"h2"} font={THEME.FONT.TYPES.H3}>
        {subtitle}
      </Heading>
    )}
  </header>
);
