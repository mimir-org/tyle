import { Heading } from "../text";
import { THEME } from "../core";
import { MotionFlexbox } from "../layouts";

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
  <MotionFlexbox as={"header"} layout flexDirection={"column"}>
    {title && <Heading>{title}</Heading>}
    {subtitle && (
      <Heading as={"h2"} font={THEME.FONT.TYPES.H3}>
        {subtitle}
      </Heading>
    )}
  </MotionFlexbox>
);
