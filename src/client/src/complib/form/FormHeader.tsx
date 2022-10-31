import { MotionFlexbox } from "complib/layouts";
import { Text } from "complib/text";

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
    {title && <Text variant={"display-medium"}>{title}</Text>}
    {subtitle && <Text variant={"headline-small"}>{subtitle}</Text>}
  </MotionFlexbox>
);
